using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Forms.ReportViewers;
using E_shift.Helpers;

namespace E_shift.Forms
{
    public partial class JobManagementControl : UserControl
    {
        private DataTable _allJobsTable;

        public JobManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvJobs);
            DataGridViewHelper.AttachCellClickEvent(dgvJobs, DgvJobs_CellClick);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            SyncJobStatusWithLoadStatus();
            LoadSearchComboItems();

            // Initialize placeholder text
            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;

            // Attach SearchHelper with client-side filtering
            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                // Map combo box column to DataTable column name
                var columnMap = new Dictionary<string, string>
                {
                    { "JobID", "JobID" },
                    { "CustomerName", "CustomerName" },
                    { "Status", "Status" },
                    { "Notes", "Notes" }
                };

                if (string.IsNullOrWhiteSpace(keyword) || keyword == "Search...")
                {
                    FilterJobData(null, null);
                    return;
                }

                if (col == null || !columnMap.TryGetValue(col, out string dtCol))
                {
                    // fallback: no filter
                    FilterJobData(null, null);
                    return;
                }

                FilterJobData(dtCol, keyword);
            });
        }

        private void JobManagementControl_Load(object sender, EventArgs e)
        {
            LoadJobData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.Add("JobID");
            cmbSearchBy.Items.Add("CustomerName");
            cmbSearchBy.Items.Add("Status");
            cmbSearchBy.Items.Add("Notes");
            cmbSearchBy.SelectedIndex = 0;
        }

        private DataTable GetJobDataTable()
        {
            string sql = @"
            SELECT 
                j.JobID,
                c.FullName AS CustomerName,
                j.Status,
                ISNULL(j.Notes, '') AS Notes,
                ISNULL(j.StartLocation, '') AS StartLocation,
                ISNULL(j.EndLocation, '') AS EndLocation
            FROM Job j
            INNER JOIN Customer c ON j.CustomerID = c.CustomerID
            ORDER BY j.JobID DESC";

            var dt = new DataTable();

            using (var conn = Database.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        private void LoadJobData()
        {
            try
            {
                _allJobsTable = GetJobDataTable();
                dgvJobs.DataSource = _allJobsTable;
                ApplyGridSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading job data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyGridSettings()
        {
            if (dgvJobs.DataSource == null) return;

            // Remove Edit/Delete buttons first (if they exist)
            for (int i = dgvJobs.Columns.Count - 1; i >= 0; i--)
            {
                var col = dgvJobs.Columns[i];
                if (col.Name == "Edit" || col.Name == "Delete")
                    dgvJobs.Columns.RemoveAt(i);
            }

            dgvJobs.Columns["JobID"].HeaderText = "Job ID";
            dgvJobs.Columns["CustomerName"].HeaderText = "Customer Name";
            dgvJobs.Columns["Status"].HeaderText = "Status";
            dgvJobs.Columns["Notes"].HeaderText = "Notes";
            dgvJobs.Columns["StartLocation"].HeaderText = "From";
            dgvJobs.Columns["EndLocation"].HeaderText = "To";

            dgvJobs.Columns["JobID"].Width = 60;
            dgvJobs.Columns["CustomerName"].Width = 140;
            dgvJobs.Columns["Status"].Width = 90;
            dgvJobs.Columns["Notes"].Width = 200;
            dgvJobs.Columns["StartLocation"].Width = 100;
            dgvJobs.Columns["EndLocation"].Width = 100;

            // Add Edit button column
            var editBtn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvJobs.Columns.Add(editBtn);

            // Add Delete button column
            var deleteBtn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvJobs.Columns.Add(deleteBtn);

            dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FilterJobData(string searchColumn, string keyword)
        {
            if (_allJobsTable == null)
                return;

            if (string.IsNullOrWhiteSpace(searchColumn) || string.IsNullOrWhiteSpace(keyword))
            {
                dgvJobs.DataSource = _allJobsTable;
            }
            else
            {
                var filtered = SearchHelper.LinearSearch(_allJobsTable, searchColumn, keyword);
                dgvJobs.DataSource = filtered;
            }

            ApplyGridSettings();
        }

        private void DgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var columnName = dgvJobs.Columns[e.ColumnIndex].Name;
            int jobId = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["JobID"].Value);

            if (columnName == "Edit")
            {
                EditJobForm editForm = new EditJobForm(jobId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadJobData();
                }
            }
            else if (columnName == "Delete")
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this job and all related loads?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = Database.GetConnection())
                        {
                            conn.Open();

                            using (var transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    // When a job gets deleted accordign to the jobs loads should be dleeted as well
                                    //also when a load gets dleetd loadschedule table shoudl deletthe records aswell
                                    //this has been doen through CASCADE in SQL DATABASE


                                    // Delete Job
                                    string deleteJobQuery = "DELETE FROM Job WHERE JobID = @JobID";
                                    using (SqlCommand cmd = new SqlCommand(deleteJobQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@JobID", jobId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    transaction.Commit();

                                    LoadJobData();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    throw ex;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting job and related data: " + ex.Message);
                    }
                }
            }
        }

        private void cmbSearchBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = (ComboBox)sender;

            Color baseColor = Color.FromArgb(30, 144, 255);
            Color baseColorDark = Color.FromArgb(25, 120, 230);

            using (var backgroundBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                e.Bounds, baseColor, baseColorDark, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (var highlightBrush = new SolidBrush(Color.FromArgb(100, Color.LightSkyBlue)))
                {
                    e.Graphics.FillRectangle(highlightBrush, e.Bounds);
                }
            }

            string text = combo.Items[e.Index].ToString();
            using (var textBrush = new SolidBrush(Color.White))
            {
                var textRect = new Rectangle(e.Bounds.Left + 8, e.Bounds.Top, e.Bounds.Width - 8, e.Bounds.Height);

                TextRenderer.DrawText(e.Graphics, text, combo.Font, textRect, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }

            e.DrawFocusRectangle();
        }

        private void SyncJobStatusWithLoadStatus()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                   UPDATE Job
                    SET Status = 
                        CASE
                            WHEN EXISTS (
                                SELECT 1 FROM Load 
                                WHERE Load.JobID = Job.JobID AND Load.Status = 'InProgress'
                            ) THEN 'InProgress'

                            WHEN EXISTS (
                                SELECT 1 FROM Load 
                                WHERE Load.JobID = Job.JobID AND Load.Status = 'Pending'
                            ) THEN 'Pending'

                            WHEN NOT EXISTS (
                                SELECT 1 FROM Load 
                                WHERE Load.JobID = Job.JobID AND Load.Status NOT IN ('Completed', 'Cancelled')
                            ) AND EXISTS (
                                SELECT 1 FROM Load 
                                WHERE Load.JobID = Job.JobID AND Load.Status = 'Completed'
                            ) THEN 'Completed'

                            WHEN NOT EXISTS (
                                SELECT 1 FROM Load 
                                WHERE Load.JobID = Job.JobID AND Load.Status NOT IN ('Cancelled')
                            ) THEN 'Cancelled'

                            ELSE Job.Status
                        END
                    WHERE EXISTS (
                        SELECT 1 FROM Load WHERE Load.JobID = Job.JobID
                    ); ";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error syncing job status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddJob_Click_1(object sender, EventArgs e)
        {
            AddJobForm form = new AddJobForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadJobData();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var dt = GetJobDataTable(); // get all data, no filter
            JobReportViewer viewer = new JobReportViewer(dt);
            viewer.ShowDialog();
        }
    }
}
