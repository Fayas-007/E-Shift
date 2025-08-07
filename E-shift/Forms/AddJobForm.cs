using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace E_shift.Forms
{
    public partial class AddJobForm : Form
    {
        public AddJobForm()
        {
            InitializeComponent();
        }

        private void AddJobForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadTransportUnits();

            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddDays(1);

            // Set minimum EndDate to StartDate + 1 day on load
            dtpEndDate.MinDate = dtpStartDate.Value.AddDays(1);

            // Attach event handler for StartDate change
            dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;

            cmbStatus.Items.AddRange(new string[] { "Pending", "In Progress", "Completed", "Cancelled" });
            cmbStatus.SelectedIndex = 0;
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            // Update minimum allowed EndDate to StartDate + 1 day
            DateTime minEndDate = dtpStartDate.Value.AddDays(1);
            dtpEndDate.MinDate = minEndDate;

            // If EndDate is less than new min, update it
            if (dtpEndDate.Value < minEndDate)
            {
                dtpEndDate.Value = minEndDate;
            }
        }

        private void LoadCustomers()
        {
            try
            {
                string query = "SELECT CustomerID, FullName FROM Customer ORDER BY FullName";
                DataTable dt = Database.GetData(query);

                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "CustomerID";
                cmbCustomer.DataSource = dt;
                cmbCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransportUnits()
        {
            try
            {
                string query = "SELECT TransportUnitID, UnitName, IsAvailable FROM TransportUnit ORDER BY UnitName";
                DataTable dt = Database.GetData(query);

                cmbTransportUnit.DisplayMember = "UnitName";
                cmbTransportUnit.ValueMember = "TransportUnitID";
                cmbTransportUnit.DataSource = dt;
                cmbTransportUnit.SelectedIndex = -1;

                // Attach SelectedIndexChanged event here
                cmbTransportUnit.SelectedIndexChanged += CmbTransportUnit_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transport units: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
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
                            // --- New Availability Check ---
                            string checkAvailabilityQuery = "SELECT IsAvailable FROM TransportUnit WHERE TransportUnitID = @TransportUnitID";
                            using (var availabilityCmd = new SqlCommand(checkAvailabilityQuery, conn, transaction))
                            {
                                availabilityCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                object result = availabilityCmd.ExecuteScalar();
                                if (result == null || Convert.ToInt32(result) != 1)
                                {
                                    MessageBox.Show("Selected transport unit is not available for booking.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // stop here
                                }
                            }

                            // Existing conflict check
                            string checkSchedule = @"
                        SELECT COUNT(*) FROM TransportUnitSchedule 
                        WHERE TransportUnitID = @TransportUnitID
                        AND (
                            (StartDate <= @EndDate AND EndDate >= @StartDate)
                        )";

                            using (var checkCmd = new SqlCommand(checkSchedule, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                checkCmd.Parameters.AddWithValue("@StartDate", startDate);
                                checkCmd.Parameters.AddWithValue("@EndDate", endDate);

                                int conflict = (int)checkCmd.ExecuteScalar();
                                if (conflict > 0)
                                {
                                    MessageBox.Show("This transport unit is already booked for the selected period.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Insert Job
                            string insertJobQuery = @"
                        INSERT INTO Job (CustomerID, StartLocation, Destination, StartDate, EndDate, Status, TransportUnitID)
                        VALUES (@CustomerID, @StartLocation, @Destination, @StartDate, @EndDate, @Status, @TransportUnitID);
                        SELECT SCOPE_IDENTITY();";

                            int jobId;
                            using (var insertJobCmd = new SqlCommand(insertJobQuery, conn, transaction))
                            {
                                insertJobCmd.Parameters.AddWithValue("@CustomerID", cmbCustomer.SelectedValue);
                                insertJobCmd.Parameters.AddWithValue("@StartLocation", txtStartLocation.Text.Trim());
                                insertJobCmd.Parameters.AddWithValue("@Destination", txtDestination.Text.Trim());
                                insertJobCmd.Parameters.AddWithValue("@StartDate", startDate);
                                insertJobCmd.Parameters.AddWithValue("@EndDate", endDate);
                                insertJobCmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                                insertJobCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);

                                jobId = Convert.ToInt32(insertJobCmd.ExecuteScalar());
                            }

                            // Insert Schedule
                            string insertScheduleQuery = @"
                        INSERT INTO TransportUnitSchedule (TransportUnitID, JobID, StartDate, EndDate)
                        VALUES (@TransportUnitID, @JobID, @StartDate, @EndDate)";

                            using (var scheduleCmd = new SqlCommand(insertScheduleQuery, conn, transaction))
                            {
                                scheduleCmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                scheduleCmd.Parameters.AddWithValue("@JobID", jobId);
                                scheduleCmd.Parameters.AddWithValue("@StartDate", startDate);
                                scheduleCmd.Parameters.AddWithValue("@EndDate", endDate);
                                scheduleCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Job added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error adding job: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbTransportUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTransportUnit.SelectedItem is DataRowView row)
            {
                bool isAvailable = Convert.ToBoolean(row["IsAvailable"]);
                lblTransportUnitAvailability.Text = isAvailable ? "Available " : "Not Available ";
                lblTransportUnitAvailability.ForeColor = isAvailable ? Color.Green : Color.Red;
            }
            else
            {
                lblTransportUnitAvailability.Text = "";
            }
        }

        private bool ValidateInput()
        {
            if (cmbCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCustomer.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStartLocation.Text))
            {
                MessageBox.Show("Please enter the start location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartLocation.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                MessageBox.Show("Please enter the destination.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDestination.Focus();
                return false;
            }

            if (cmbTransportUnit.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a transport unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTransportUnit.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Please select the status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e) { }

        private void lblTitle_Click(object sender, EventArgs e) { }

        private void lblTransportUnitAvailability_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
