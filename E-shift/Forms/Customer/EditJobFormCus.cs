using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms.Customer
{
    public partial class EditJobFormCus : Form
    {
        private readonly int _jobId;
        private int _customerId;

        public EditJobFormCus(int jobId)
        {
            InitializeComponent();
            _jobId = jobId;
        }

        private void EditJobFormCus_Load(object sender, EventArgs e)
        {
            LoadJobDetails();
        }

        private void LoadJobDetails()
        {
            string query = @"
                SELECT j.CustomerID, c.FullName, j.Notes, j.StartLocation, j.EndLocation
                FROM Job j 
                INNER JOIN Customer c ON j.CustomerID = c.CustomerID 
                WHERE j.JobID = @JobID";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobID", _jobId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _customerId = Convert.ToInt32(reader["CustomerID"]);
                        txtCustomerName.Text = reader["FullName"].ToString();
                        txtNotes.Text = reader["Notes"]?.ToString() ?? "";
                        txtStartLocation.Text = reader["StartLocation"]?.ToString() ?? "";
                        txtEndLocation.Text = reader["EndLocation"]?.ToString() ?? "";
                    }
                }
            }

            txtCustomerName.ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string updatedNotes = txtNotes.Text.Trim();
            string updatedStartLocation = txtStartLocation.Text.Trim();
            string updatedEndLocation = txtEndLocation.Text.Trim();

            if (string.IsNullOrEmpty(updatedNotes))
            {
                MessageBox.Show("Notes cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(updatedStartLocation))
            {
                MessageBox.Show("Start Location cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(updatedEndLocation))
            {
                MessageBox.Show("End Location cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string updateQuery = @"
                UPDATE Job 
                SET Notes = @Notes, StartLocation = @StartLocation, EndLocation = @EndLocation 
                WHERE JobID = @JobID";

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Notes", updatedNotes);
                    cmd.Parameters.AddWithValue("@StartLocation", updatedStartLocation);
                    cmd.Parameters.AddWithValue("@EndLocation", updatedEndLocation);
                    cmd.Parameters.AddWithValue("@JobID", _jobId);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Job updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating job: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblStartLocation_Click(object sender, EventArgs e)
        {

        }

        private void txtStartLocation_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
