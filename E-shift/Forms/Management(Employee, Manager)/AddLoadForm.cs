using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using E_shift.DataAccess;

namespace E_shift.Forms
{
    public partial class AddLoadForm : Form
    {
        public AddLoadForm()
        {
            InitializeComponent();
        }

        private void AddLoadForm_Load(object sender, EventArgs e)
        {
            LoadJobIDs();

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddDays(1);

            dtpStartDate.MinDate = DateTime.Today;
            dtpEndDate.MinDate = dtpStartDate.Value.AddDays(1);

            dtpStartDate.ValueChanged += DateRangeChanged;
            dtpEndDate.ValueChanged += DateRangeChanged;

            LoadAvailableTransportUnits(dtpStartDate.Value, dtpEndDate.Value);
        }

        public bool IsJobCancelled(int jobId)
        {
            string query = "SELECT Status FROM Job WHERE JobID = @JobID";
            var parameters = new Dictionary<string, object> { { "@JobID", jobId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                string status = dt.Rows[0]["Status"].ToString();
                return status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private void LoadJobIDs()
        {
            string query = "SELECT JobID FROM Job WHERE Status <> 'Cancelled' ORDER BY JobID DESC";

            DataTable dt = Database.GetData(query);

            cmbJobID.DisplayMember = "JobID";
            cmbJobID.ValueMember = "JobID";
            cmbJobID.DataSource = dt;
            cmbJobID.SelectedIndex = -1;
            txtCustomerName.Text = "";

            cmbJobID.SelectedIndexChanged += CmbJobID_SelectedIndexChanged;
        }

        private void CmbJobID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbJobID.SelectedValue == null)
            {
                txtCustomerName.Text = "";
                return;
            }

            int selectedJobId = 0;

            if (cmbJobID.SelectedValue is int val)
            {
                selectedJobId = val;
            }
            else if (cmbJobID.SelectedValue is DataRowView drv)
            {
                selectedJobId = Convert.ToInt32(drv["JobID"]);
            }

            string query = @"
                SELECT c.FullName 
                FROM Job j
                INNER JOIN Customer c ON j.CustomerID = c.CustomerID
                WHERE j.JobID = @JobID";

            var parameters = new Dictionary<string, object>
            {
                { "@JobID", selectedJobId }
            };

            try
            {
                DataTable dt = Database.GetData(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    txtCustomerName.Text = dt.Rows[0]["FullName"].ToString();
                }
                else
                {
                    txtCustomerName.Text = "(No Customer Found)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Text = "(Error)";
            }
        }

        private void LoadAvailableTransportUnits(DateTime startDate, DateTime endDate)
        {
            try
            {
                string query = @"
                    SELECT TransportUnitID, UnitName 
                    FROM TransportUnit
                    WHERE IsAvailable = 1 AND TransportUnitID NOT IN (
                        SELECT TransportUnitID
                        FROM LoadSchedule
                        WHERE (
                            (@StartDate <= EndDate AND @EndDate >= StartDate)
                        )
                    )
                    ORDER BY UnitName";

                var parameters = new Dictionary<string, object>
                {
                    { "@StartDate", startDate },
                    { "@EndDate", endDate }
                };

                DataTable dt = Database.GetData(query, parameters);

                cmbTransportUnit.DisplayMember = "UnitName";
                cmbTransportUnit.ValueMember = "TransportUnitID";
                cmbTransportUnit.DataSource = dt;
                cmbTransportUnit.SelectedIndex = -1;

                lblTransportUnitAvailability.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transport units: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateRangeChanged(object sender, EventArgs e)
        {
            DateTime start = dtpStartDate.Value.Date;
            DateTime minEnd = start.AddDays(1);

            dtpEndDate.MinDate = minEnd;

            if (dtpEndDate.Value < minEnd)
            {
                dtpEndDate.Value = minEnd;
            }

            LoadAvailableTransportUnits(start, dtpEndDate.Value.Date);
        }

        private void BtnAddLoad_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int jobId = Convert.ToInt32(cmbJobID.SelectedValue);
            decimal weight = Convert.ToDecimal(txtWeight.Text);
            int transportUnitId = Convert.ToInt32(cmbTransportUnit.SelectedValue);
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            string startLocation = txtStartLocation.Text.Trim();
            string endLocation = txtEndLocation.Text.Trim();

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string insertQuery = @"
                                INSERT INTO Load (JobID, Weight, StartDate, EndDate, CreatedAt, TransportUnitID, StartLocation, EndLocation)
                                VALUES (@JobID, @Weight, @StartDate, @EndDate, @CreatedAt, @TransportUnitID, @StartLocation, @EndLocation);
                                SELECT SCOPE_IDENTITY();";

                            int loadId;

                            using (var cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@JobID", jobId);
                                cmd.Parameters.AddWithValue("@Weight", weight);
                                cmd.Parameters.AddWithValue("@StartDate", startDate);
                                cmd.Parameters.AddWithValue("@EndDate", endDate);
                                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                                cmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                cmd.Parameters.AddWithValue("@StartLocation", startLocation);
                                cmd.Parameters.AddWithValue("@EndLocation", endLocation);

                                loadId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            string insertSchedule = @"
                                INSERT INTO LoadSchedule (TransportUnitID, LoadID, StartDate, EndDate)
                                VALUES (@TransportUnitID, @LoadID, @StartDate, @EndDate)";

                            using (var cmd = new SqlCommand(insertSchedule, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TransportUnitID", transportUnitId);
                                cmd.Parameters.AddWithValue("@LoadID", loadId);
                                cmd.Parameters.AddWithValue("@StartDate", startDate);
                                cmd.Parameters.AddWithValue("@EndDate", endDate);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Load added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error adding load: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbJobID.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Job ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtWeight.Text, out decimal weight) || weight <= 0)
            {
                MessageBox.Show("Please enter a valid positive weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbTransportUnit.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a transport unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStartLocation.Text))
            {
                MessageBox.Show("Please enter a start location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEndLocation.Text))
            {
                MessageBox.Show("Please enter an end location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpStartDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Start Date cannot be in the past.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpEndDate.Value.Date <= dtpStartDate.Value.Date)
            {
                MessageBox.Show("End Date must be at least one day after Start Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
