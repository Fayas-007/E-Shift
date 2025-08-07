using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

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
                // Optional handling
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
            string query = @"SELECT TransportUnitID, UnitName FROM TransportUnit WHERE IsAvailable = 1 ORDER BY UnitName";
            var dt = Database.GetData(query);
            cmbTransportUnit.DataSource = dt;
            cmbTransportUnit.DisplayMember = "UnitName";
            cmbTransportUnit.ValueMember = "TransportUnitID";
        }

        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
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
                       L.TransportUnitID,
                       L.StartLocation,
                       L.EndLocation
                FROM Load L
                WHERE L.LoadID = @LoadID";

            var parameters = new Dictionary<string, object> { { "@LoadID", loadId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                cmbJob.SelectedValue = row["JobID"];
                txtWeight.Text = row["Weight"].ToString();
                txtStartLocation.Text = row["StartLocation"]?.ToString();
                txtEndLocation.Text = row["EndLocation"]?.ToString();

                string status = row["Status"]?.ToString();
                if (cmbStatus.Items.Contains(status)) cmbStatus.SelectedItem = status;

                DateTime start = row["StartDate"] == DBNull.Value ? DateTime.Today : (DateTime)row["StartDate"];
                if (start < DateTime.Today) start = DateTime.Today;
                dtpStartDate.Value = start;

                DateTime end = row["EndDate"] == DBNull.Value ? start.AddDays(1) : (DateTime)row["EndDate"];
                if (end < start.AddDays(1)) end = start.AddDays(1);
                dtpEndDate.Value = end;

                if (row["TransportUnitID"] != DBNull.Value)
                    cmbTransportUnit.SelectedValue = (int)row["TransportUnitID"];

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
            string startLocation = txtStartLocation.Text.Trim();
            string endLocation = txtEndLocation.Text.Trim();

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
                                StartLocation   = @StartLocation,
                                EndLocation     = @EndLocation,
                                TransportUnitID = @TransportUnitID
                            WHERE LoadID = @LoadID;";

                        using (var cmd = new SqlCommand(updateLoadSql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@JobID", jobId);
                            cmd.Parameters.AddWithValue("@Weight", weight);
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            cmd.Parameters.AddWithValue("@StartLocation", startLocation);
                            cmd.Parameters.AddWithValue("@EndLocation", endLocation);
                            cmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                            cmd.Parameters.AddWithValue("@LoadID", loadId);
                            cmd.ExecuteNonQuery();
                        }
                        string updateScheduleSql = @"
                        UPDATE LoadSchedule
                           SET TransportUnitID = @TransportUnitID,
                               StartDate       = @StartDate,
                               EndDate         = @EndDate
                         WHERE LoadID = @LoadID;";

                        using (var cmd = new SqlCommand(updateScheduleSql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            cmd.Parameters.AddWithValue("@LoadID", loadId);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Load updated successfully.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating load: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrWhiteSpace(txtStartLocation.Text))
            {
                MessageBox.Show("Please enter Start Location.");
                txtStartLocation.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEndLocation.Text))
            {
                MessageBox.Show("Please enter End Location.");
                txtEndLocation.Focus();
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            // No action needed
        }

        private void cmbTransportUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional
        }
    }
}
