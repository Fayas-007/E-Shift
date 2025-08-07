// [Same using statements as you had]
using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using E_shift.Helpers;
using System.Linq;

namespace E_shift.Forms
{
    public partial class EditLoadForm : Form
    {
        private readonly int loadId;

        public EditLoadForm(int loadId)
        {
            InitializeComponent();
            this.loadId = loadId;
        }

        private void EditLoadForm_Load(object sender, EventArgs e)
        {
            LoadJobs();
            LoadTransportUnits();
            LoadStatusOptions();
            LoadLoadDetails();

            dtpStartDate.MinDate = DateTime.Today;
            dtpStartDate.ValueChanged += DtpStartDate_ValueChanged;
            UpdateEndDateConstraints();

            cmbJob.SelectedIndexChanged += CmbJob_SelectedIndexChanged;
        }

        private void CmbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbJob.SelectedValue is int jobId)
            {
                // Optional
            }
        }

        private void LoadJobs()
        {
            string query = @"
                SELECT JobID, CAST(JobID AS VARCHAR) AS DisplayText
                FROM Job
                WHERE Status <> 'Cancelled'
                ORDER BY JobID DESC";

            cmbJob.DataSource = Database.GetData(query);
            cmbJob.DisplayMember = "DisplayText";
            cmbJob.ValueMember = "JobID";
        }

        private void LoadTransportUnits()
        {
            DateTime selectedStartDate = dtpStartDate.Value.Date;
            DateTime selectedEndDate = dtpEndDate.Value.Date;

            string query = @"
            SELECT TU.TransportUnitID, TU.UnitName
            FROM TransportUnit TU
            WHERE TU.IsAvailable = 1
              AND NOT EXISTS (
                  SELECT 1 FROM LoadSchedule LS
                  WHERE LS.TransportUnitID = TU.TransportUnitID
                    AND LS.LoadID <> @LoadID
                    AND NOT (
                        LS.EndDate < @StartDate OR
                        LS.StartDate > @EndDate
                    )
              )";

            var parameters = new Dictionary<string, object>
            {
                { "@StartDate", selectedStartDate },
                { "@EndDate", selectedEndDate },
                { "@LoadID", loadId }
            };

            DataTable dt = Database.GetData(query, parameters);

            cmbTransportUnit.DataSource = dt;
            cmbTransportUnit.DisplayMember = "UnitName";
            cmbTransportUnit.ValueMember = "TransportUnitID";

            // Reset selection to none
            cmbTransportUnit.SelectedIndex = -1;
        }


        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Submitted");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("InProgress");
            cmbStatus.Items.Add("Completed");
            cmbStatus.Items.Add("Cancelled");

            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;
        }

        private void LoadLoadDetails()
        {
            string query = @"
                SELECT L.LoadID,
                       L.JobID,
                       L.Weight,
                       L.Status,
                       L.StartDate,
                       L.EndDate,
                       L.TransportUnitID
                FROM Load L
                WHERE L.LoadID = @LoadID";

            var parameters = new Dictionary<string, object> { { "@LoadID", loadId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                cmbJob.SelectedValue = row["JobID"];
                txtWeight.Text = row["Weight"].ToString();

                string status = row["Status"]?.ToString();
                if (cmbStatus.Items.Contains(status)) cmbStatus.SelectedItem = status;

                DateTime start = row["StartDate"] == DBNull.Value ? DateTime.Today : (DateTime)row["StartDate"];
                if (start < DateTime.Today) start = DateTime.Today;
                dtpStartDate.Value = start;

                DateTime end = row["EndDate"] == DBNull.Value ? start.AddDays(1) : (DateTime)row["EndDate"];
                if (end < start.AddDays(1)) end = start.AddDays(1);
                dtpEndDate.Value = end;

                if (row["TransportUnitID"] != DBNull.Value)
                    cmbTransportUnit.SelectedValue = Convert.ToInt32(row["TransportUnitID"]);

                UpdateEndDateConstraints();
            }
        }

        private void DtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateEndDateConstraints();
        }

        private void UpdateEndDateConstraints()
        {
            dtpEndDate.MinDate = dtpStartDate.Value.AddDays(1);

            if (dtpEndDate.Value < dtpEndDate.MinDate)
                dtpEndDate.Value = dtpEndDate.MinDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int jobId = (int)cmbJob.SelectedValue;
            decimal weight = decimal.Parse(txtWeight.Text.Trim());
            string status = cmbStatus.SelectedItem.ToString();
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;
            int transportUnitId = (int)cmbTransportUnit.SelectedValue;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        if (IsScheduleOverlapping(conn, tx, transportUnitId, loadId, startDate, endDate))
                        {
                            MessageBox.Show("The selected transport unit already has a load scheduled during this period. Please select different dates or a different transport unit.",
                                            "Schedule Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tx.Rollback();
                            return;
                        }

                        string updateLoadSql = @"
                            UPDATE Load SET
                                JobID           = @JobID,
                                Weight          = @Weight,
                                Status          = @Status,
                                StartDate       = @StartDate,
                                EndDate         = @EndDate,
                                TransportUnitID = @TransportUnitID
                            WHERE LoadID = @LoadID;";

                        using (var cmd = new SqlCommand(updateLoadSql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@JobID", jobId);
                            cmd.Parameters.AddWithValue("@Weight", weight);
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            cmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                            cmd.Parameters.AddWithValue("@LoadID", loadId);
                            cmd.ExecuteNonQuery();
                        }

                        string checkScheduleSql = "SELECT COUNT(*) FROM LoadSchedule WHERE LoadID = @LoadID";
                        using (var cmd = new SqlCommand(checkScheduleSql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@LoadID", loadId);
                            int count = (int)cmd.ExecuteScalar();

                            if (count > 0)
                            {
                                string updateScheduleSql = @"
                                    UPDATE LoadSchedule
                                    SET TransportUnitID = @TransportUnitID,
                                        StartDate       = @StartDate,
                                        EndDate         = @EndDate
                                    WHERE LoadID = @LoadID;";

                                using (var updateCmd = new SqlCommand(updateScheduleSql, conn, tx))
                                {
                                    updateCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                    updateCmd.Parameters.AddWithValue("@StartDate", startDate);
                                    updateCmd.Parameters.AddWithValue("@EndDate", endDate);
                                    updateCmd.Parameters.AddWithValue("@LoadID", loadId);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertScheduleSql = @"
                                    INSERT INTO LoadSchedule (LoadID, TransportUnitID, StartDate, EndDate)
                                    VALUES (@LoadID, @TransportUnitID, @StartDate, @EndDate);";

                                using (var insertCmd = new SqlCommand(insertScheduleSql, conn, tx))
                                {
                                    insertCmd.Parameters.AddWithValue("@LoadID", loadId);
                                    insertCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                    insertCmd.Parameters.AddWithValue("@StartDate", startDate);
                                    insertCmd.Parameters.AddWithValue("@EndDate", endDate);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                    }
                }

                JobStatusHelper.SyncJobStatus();
                MessageBox.Show("Load updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating load: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsScheduleOverlapping(SqlConnection conn, SqlTransaction tx, int transportUnitId, int loadId, DateTime newStart, DateTime newEnd)
        {
            string sql = @"
                SELECT COUNT(*) FROM Load
                WHERE TransportUnitID = @TransportUnitID
                  AND LoadID <> @LoadID
                  AND NOT (
                      EndDate < @NewStartDate OR
                      StartDate > @NewEndDate
                  );";

            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                cmd.Parameters.AddWithValue("@LoadID", loadId);
                cmd.Parameters.AddWithValue("@NewStartDate", newStart);
                cmd.Parameters.AddWithValue("@NewEndDate", newEnd);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private bool ValidateInput()
        {
            if (cmbJob.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Job.");
                cmbJob.Focus();
                return false;
            }
            if (!decimal.TryParse(txtWeight.Text.Trim(), out _))
            {
                MessageBox.Show("Please enter a valid weight.");
                txtWeight.Focus();
                return false;
            }

            if (cmbTransportUnit.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Transport Unit.");
                cmbTransportUnit.Focus();
                return false;
            }
            if (dtpStartDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Start Date cannot be in the past.");
                dtpStartDate.Focus();
                return false;
            }
            if (dtpEndDate.Value.Date <= dtpStartDate.Value.Date)
            {
                MessageBox.Show("End Date must be at least one day after the Start Date.");
                dtpEndDate.Focus();
                return false;
            }

            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e) => Close();

        private void lblTitle_Click(object sender, EventArgs e) { }

        private void cmbTransportUnit_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
