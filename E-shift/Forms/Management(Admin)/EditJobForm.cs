using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
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
            LoadStatusOptions();
            LoadJobDetails();
        }

        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Submitted");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("InProgress");
            cmbStatus.Items.Add("Completed");
            cmbStatus.Items.Add("Cancelled");
        }

        private void LoadCustomers()
        {
            string query = "SELECT CustomerID, FullName FROM Customer ORDER BY FullName";
            cmbCustomer.DataSource = Database.GetData(query);
            cmbCustomer.DisplayMember = "FullName";
            cmbCustomer.ValueMember = "CustomerID";
        }

        private void LoadJobDetails()
        {
            string query = @"
                SELECT CustomerID, Status, Notes, StartLocation, EndLocation 
                FROM Job WHERE JobID = @JobID";
            var parameters = new Dictionary<string, object> { { "@JobID", jobId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                cmbCustomer.SelectedValue = row["CustomerID"];
                txtNotes.Text = row["Notes"] == DBNull.Value ? "" : row["Notes"].ToString();

                string status = row["Status"]?.ToString();
                if (!string.IsNullOrEmpty(status) && cmbStatus.Items.Contains(status))
                {
                    cmbStatus.SelectedItem = status;
                }
                else
                {
                    cmbStatus.SelectedIndex = 0;
                }

                // Load StartLocation and EndLocation
                txtStartLocation.Text = row["StartLocation"] == DBNull.Value ? "" : row["StartLocation"].ToString();
                txtEndLocation.Text = row["EndLocation"] == DBNull.Value ? "" : row["EndLocation"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string jobStatus = cmbStatus.SelectedItem.ToString();

                // Update Job table with StartLocation and EndLocation
                string updateJobQuery = @"
                    UPDATE Job SET 
                        CustomerID = @CustomerID,
                        Status = @Status,
                        Notes = @Notes,
                        StartLocation = @StartLocation,
                        EndLocation = @EndLocation
                    WHERE JobID = @JobID";

                var parameters = new Dictionary<string, object>
                {
                    { "@CustomerID", cmbCustomer.SelectedValue },
                    { "@Status", jobStatus },
                    { "@Notes", txtNotes.Text.Trim() },
                    { "@StartLocation", txtStartLocation.Text.Trim() },
                    { "@EndLocation", txtEndLocation.Text.Trim() },
                    { "@JobID", jobId }
                };

                Database.ExecuteNonQuery(updateJobQuery, parameters);

                // Use the same status value for Load update (no mapping needed)
                string loadStatus = jobStatus;

                // Update all loads related to this job
                string updateLoadsQuery = @"
                    UPDATE Load
                    SET Status = @Status
                    WHERE JobID = @JobID";

                var loadParams = new Dictionary<string, object>
                {
                    { "@Status", loadStatus },
                    { "@JobID", jobId }
                };

                Database.ExecuteNonQuery(updateLoadsQuery, loadParams);

                MessageBox.Show("Job and related Loads updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating job and loads: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a status.");
                cmbStatus.Focus();
                return false;
            }

            
            if (string.IsNullOrWhiteSpace(txtStartLocation.Text))
            {
                MessageBox.Show("Please enter a start location.");
                txtStartLocation.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEndLocation.Text))
            {
                MessageBox.Show("Please enter an end location.");
                txtEndLocation.Focus();
                return false;
            }
            

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
