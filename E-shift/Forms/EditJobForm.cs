using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class EditJobForm : Form
    {
        private readonly int jobId;

        public EditJobForm(int jobId)
        {
            InitializeComponent();
            this.jobId = jobId;
        }

        private void EditJobForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadTransportUnits();
            LoadJobDetails();

            dtpStartDate.MinDate = DateTime.Today;
            dtpEndDate.MinDate = dtpStartDate.Value.AddDays(1);
            dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("In Progress");
            cmbStatus.Items.Add("Completed");
            cmbStatus.Items.Add("Cancelled");

            cmbStatus.SelectedIndex = 0;
        }

        private void LoadCustomers()
        {
            string query = "SELECT CustomerID, FullName FROM Customer ORDER BY FullName";
            cmbCustomer.DataSource = Database.GetData(query);
            cmbCustomer.DisplayMember = "FullName";
            cmbCustomer.ValueMember = "CustomerID";
        }

        private void LoadTransportUnits()
        {
            string query = "SELECT TransportUnitID, UnitName FROM TransportUnit ORDER BY UnitName";
            cmbTransportUnit.DataSource = Database.GetData(query);
            cmbTransportUnit.DisplayMember = "UnitName";
            cmbTransportUnit.ValueMember = "TransportUnitID";
        }

        private void LoadJobDetails()
        {
            string query = "SELECT * FROM Job WHERE JobID = @JobID";
            var parameters = new Dictionary<string, object> { { "@JobID", jobId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                cmbCustomer.SelectedValue = row["CustomerID"];
                txtStartLocation.Text = row["StartLocation"].ToString();
                txtDestination.Text = row["Destination"].ToString();

                // Set StartDate and EndDate instead of single JobDate
                dtpStartDate.Value = Convert.ToDateTime(row["StartDate"]);
                dtpEndDate.Value = Convert.ToDateTime(row["EndDate"]);

                cmbTransportUnit.SelectedValue = row["TransportUnitID"];
                cmbStatus.Text = row["Status"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int transportUnitId = Convert.ToInt32(cmbTransportUnit.SelectedValue);
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("End date must be after start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Check for conflicts with other jobs (exclude current job)
                            string checkConflictQuery = @"
                        SELECT COUNT(*) FROM TransportUnitSchedule
                        WHERE TransportUnitID = @TransportUnitID
                        AND JobID != @JobID
                        AND (
                            (StartDate <= @EndDate AND EndDate >= @StartDate)
                        )";

                            using (var checkCmd = new SqlCommand(checkConflictQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                checkCmd.Parameters.AddWithValue("@JobID", jobId);
                                checkCmd.Parameters.AddWithValue("@StartDate", startDate);
                                checkCmd.Parameters.AddWithValue("@EndDate", endDate);

                                int conflict = (int)checkCmd.ExecuteScalar();
                                if (conflict > 0)
                                {
                                    MessageBox.Show("This transport unit is already booked for the selected period.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // 2. Update Job
                            string updateJobQuery = @"
                        UPDATE Job SET 
                            CustomerID = @CustomerID,
                            StartLocation = @StartLocation,
                            Destination = @Destination,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            Status = @Status,
                            TransportUnitID = @TransportUnitID
                        WHERE JobID = @JobID";

                            using (var updateCmd = new SqlCommand(updateJobQuery, conn, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@CustomerID", cmbCustomer.SelectedValue);
                                updateCmd.Parameters.AddWithValue("@StartLocation", txtStartLocation.Text.Trim());
                                updateCmd.Parameters.AddWithValue("@Destination", txtDestination.Text.Trim());
                                updateCmd.Parameters.AddWithValue("@StartDate", startDate);
                                updateCmd.Parameters.AddWithValue("@EndDate", endDate);
                                updateCmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                                updateCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                updateCmd.Parameters.AddWithValue("@JobID", jobId);

                                updateCmd.ExecuteNonQuery();
                            }

                            // 3. Update TransportUnitSchedule
                            string updateScheduleQuery = @"
                        UPDATE TransportUnitSchedule 
                        SET StartDate = @StartDate, EndDate = @EndDate, TransportUnitID = @TransportUnitID
                        WHERE JobID = @JobID";

                            using (var updateScheduleCmd = new SqlCommand(updateScheduleQuery, conn, transaction))
                            {
                                updateScheduleCmd.Parameters.AddWithValue("@StartDate", startDate);
                                updateScheduleCmd.Parameters.AddWithValue("@EndDate", endDate);
                                updateScheduleCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                updateScheduleCmd.Parameters.AddWithValue("@JobID", jobId);
                                updateScheduleCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show("Job updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error updating job: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            // Set the minimum for EndDate as StartDate + 1 day
            DateTime minEndDate = dtpStartDate.Value.AddDays(1);
            dtpEndDate.MinDate = minEndDate;

            // If current EndDate is before the new minimum, update it
            if (dtpEndDate.Value < minEndDate)
            {
                dtpEndDate.Value = minEndDate;
            }
        }

        private bool ValidateInput()
        {
            if (cmbCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer.");
                cmbCustomer.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtStartLocation.Text))
            {
                MessageBox.Show("Please enter the start location.");
                txtStartLocation.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                MessageBox.Show("Please enter the destination.");
                txtDestination.Focus();
                return false;
            }
            if (cmbTransportUnit.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a transport unit.");
                cmbTransportUnit.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbStatus.Text))
            {
                MessageBox.Show("Please select a status.");
                cmbStatus.Focus();
                return false;
            }
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.");
                dtpEndDate.Focus();
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
