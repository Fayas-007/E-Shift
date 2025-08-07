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

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Pending", "In Progress", "Completed", "Cancelled" });

            LoadJobDetails();
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
            string query = "SELECT CustomerID, Status, Notes FROM Job WHERE JobID = @JobID";
            var parameters = new Dictionary<string, object> { { "@JobID", jobId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                cmbCustomer.SelectedValue = row["CustomerID"];

                string statusFromDb = row["Status"].ToString();
                int index = cmbStatus.Items.IndexOf(statusFromDb);
                cmbStatus.SelectedIndex = index >= 0 ? index : 0;

                // Set Notes textbox value
                txtNotes.Text = row["Notes"] == DBNull.Value ? "" : row["Notes"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string updateJobQuery = @"
                    UPDATE Job SET 
                        CustomerID = @CustomerID,
                        Status = @Status,
                        Notes = @Notes
                    WHERE JobID = @JobID";

                var parameters = new Dictionary<string, object>
                {
                    { "@CustomerID", cmbCustomer.SelectedValue },
                    { "@Status", cmbStatus.Text },
                    { "@Notes", txtNotes.Text.Trim() },
                    { "@JobID", jobId }
                };

                Database.ExecuteNonQuery(updateJobQuery, parameters);

                MessageBox.Show("Job updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating job: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrWhiteSpace(cmbStatus.Text))
            {
                MessageBox.Show("Please select a status.");
                cmbStatus.Focus();
                return false;
            }
            // Notes can be optional, so no validation for txtNotes needed here.
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
