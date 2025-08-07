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
        public JobManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvJobs);
            DataGridViewHelper.AttachCellClickEvent(dgvJobs, DgvJobs_CellClick);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            SyncJobStatusWithLoadStatus();
            LoadSearchComboItems();

            // Attach SearchHelper instead of separate txtSearch events
            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadJobData(); // no filter, load all
                    return;
                }

                var columnMap = new Dictionary<string, string>
        {
            { "JobID", "j.JobID" },
            { "CustomerName", "c.FullName" },
            { "Status", "j.Status" },
            { "Notes", "j.Notes" }
        };

                if (!columnMap.TryGetValue(col ?? "", out string dbCol))
                    return;

                // You could add any normalization of keyword here if needed

                LoadJobData(dbCol, keyword);
            });

            // Optional placeholder text
            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
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

        private DataTable GetJobDataTable(string filterColumn = null, string keyword = null)
        {
            string sql = @"
                SELECT 
                    j.JobID,
                    c.FullName AS CustomerName,
                    j.Status,
                    ISNULL(j.Notes, '') AS Notes
                FROM Job j
                INNER JOIN Customer c ON j.CustomerID = c.CustomerID";

            if (!string.IsNullOrWhiteSpace(filterColumn) && !string.IsNullOrWhiteSpace(keyword))
            {
                sql += $" WHERE {filterColumn} LIKE @keyword";
            }

            sql += " ORDER BY j.JobID DESC";

            var parameters = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(filterColumn) && !string.IsNullOrWhiteSpace(keyword))
            {
                parameters.Add("@keyword", $"%{keyword}%");
            }

            var dt = new DataTable();

            using (var conn = Database.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        private void LoadJobData(string columnFilter = null, string keyword = null)
        {
            try
            {
                var dt = GetJobDataTable(columnFilter, keyword);

                int scrollPosition = dgvJobs.FirstDisplayedScrollingRowIndex >= 0 ? dgvJobs.FirstDisplayedScrollingRowIndex : 0;
                DataGridViewCell currentCell = dgvJobs.CurrentCell;

                dgvJobs.DataSource = null;
                dgvJobs.Columns.Clear();
                dgvJobs.DataSource = dt;

                // Set headers
                dgvJobs.Columns["JobID"].HeaderText = "Job ID";
                dgvJobs.Columns["CustomerName"].HeaderText = "Customer Name";
                dgvJobs.Columns["Status"].HeaderText = "Status";
                dgvJobs.Columns["Notes"].HeaderText = "Notes";

                // Set widths
                dgvJobs.Columns["JobID"].Width = 60;
                dgvJobs.Columns["CustomerName"].Width = 140;
                dgvJobs.Columns["Status"].Width = 90;
                dgvJobs.Columns["Notes"].Width = 200;

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
            catch (Exception ex)
            {
                MessageBox.Show("Error loading job data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                                    // Delete LoadSchedule rows linked to loads in this job
                                    string deleteLoadScheduleQuery = @"DELETE FROM LoadSchedule WHERE LoadID IN (SELECT LoadID FROM Load WHERE JobID = @JobID)";
                                    using (SqlCommand cmd = new SqlCommand(deleteLoadScheduleQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@JobID", jobId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Delete Load rows linked to this job
                                    string deleteLoadsQuery = "DELETE FROM Load WHERE JobID = @JobID";
                                    using (SqlCommand cmd = new SqlCommand(deleteLoadsQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@JobID", jobId);
                                        cmd.ExecuteNonQuery();
                                    }

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
                        ) THEN 'In Progress'

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
                );";

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
