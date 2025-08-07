using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;

namespace E_shift.Forms.Customer
{
    public partial class CustomerJobControl : UserControl
    {
        private readonly int _customerId;
        private DataTable _jobsTable;

        public CustomerJobControl(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            DataGridViewHelper.ApplyTheme(dgvJobs);
            LoadJobs();
            DataGridViewHelper.AttachCellClickEvent(dgvJobs, dgvJobs_CellClick);
        }

        private void LoadJobs()
        {
            string query = @"
                SELECT 
                    j.JobID,
                    c.FullName AS [Customer Name],
                    j.Status,
                    j.Notes,
                    j.StartLocation,
                    j.EndLocation
                FROM Job j
                INNER JOIN Customer c ON j.CustomerID = c.CustomerID
                WHERE j.CustomerID = @CustomerID
                ORDER BY j.JobID DESC";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                _jobsTable = new DataTable();
                adapter.Fill(_jobsTable);
                dgvJobs.DataSource = _jobsTable;
            }

            // Set all columns to ReadOnly
            foreach (DataGridViewColumn col in dgvJobs.Columns)
                col.ReadOnly = true;

            // Optionally customize column headers if you want
            dgvJobs.Columns["StartLocation"].HeaderText = "Start Location";
            dgvJobs.Columns["EndLocation"].HeaderText = "End Location";

            AddEditDeleteButtons();
        }

        private void AddEditDeleteButtons()
        {
            if (!dgvJobs.Columns.Contains("Edit"))
            {
                var editButton = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                };
                dgvJobs.Columns.Add(editButton);
            }

            if (!dgvJobs.Columns.Contains("Delete"))
            {
                var deleteButton = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };
                dgvJobs.Columns.Add(deleteButton);
            }

            DataGridViewHelper.ApplyTheme(dgvJobs);
        }

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string colName = dgvJobs.Columns[e.ColumnIndex].Name;
            int jobId = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["JobID"].Value);
            string status = dgvJobs.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            bool canDeleteEdit = (status == "Submitted" || status == "Pending");

            if (colName == "Edit")
            {
                if (!canDeleteEdit)
                {
                    MessageBox.Show("Only jobs with status 'Submitted' or 'Pending' can be edited.", "Cannot Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var editForm = new EditJobFormCus(jobId))
                {
                    var result = editForm.ShowDialog();

                    // If user saved successfully, refresh the job list
                    if (result == DialogResult.OK)
                    {
                        LoadJobs();
                    }
                }

            }
            else if (colName == "Delete")
            {
                if (!canDeleteEdit)
                {
                    MessageBox.Show("The job is already taken by E-Shift. It cannot be deleted now.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("Are you sure you want to delete this job and all its associated data?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    DeleteJobAndDependencies(jobId);
                    LoadJobs();
                }
            }
        }

        private void DeleteJobAndDependencies(int jobId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string deleteSchedulesSql = @"
                            DELETE FROM LoadSchedule 
                            WHERE LoadID IN (SELECT LoadID FROM Load WHERE JobID = @JobID)";
                        using (SqlCommand cmd1 = new SqlCommand(deleteSchedulesSql, conn, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@JobID", jobId);
                            cmd1.ExecuteNonQuery();
                        }

                        string deleteLoadsSql = "DELETE FROM Load WHERE JobID = @JobID";
                        using (SqlCommand cmd2 = new SqlCommand(deleteLoadsSql, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@JobID", jobId);
                            cmd2.ExecuteNonQuery();
                        }

                        string deleteJobSql = "DELETE FROM Job WHERE JobID = @JobID";
                        using (SqlCommand cmd3 = new SqlCommand(deleteJobSql, conn, transaction))
                        {
                            cmd3.Parameters.AddWithValue("@JobID", jobId);
                            cmd3.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                MessageBox.Show("Job deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting job: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerJobControl_Load(object sender, EventArgs e)
        {
            // If needed, handle control load here
        }
    }
}
