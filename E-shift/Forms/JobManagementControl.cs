using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;
using E_shift.Models; 

namespace E_shift.Forms
{
    public partial class JobManagementControl : UserControl
    {
        public JobManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvJobs);
            DataGridViewHelper.AttachCellClickEvent(dgvJobs, dgvJobs_CellClick);

            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            LoadSearchComboItems();
        }

        private void JobManagementControl_Load(object sender, EventArgs e)
        {
            LoadJobData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();

            cmbSearchBy.Items.Add("JobID");
            cmbSearchBy.Items.Add("CustomerName"); // From join
            cmbSearchBy.Items.Add("StartLocation");
            cmbSearchBy.Items.Add("Destination");
            cmbSearchBy.Items.Add("Status");
            cmbSearchBy.Items.Add("UnitName"); // From TransportUnit

            cmbSearchBy.SelectedIndex = 0;
        }

        private List<Job> LoadJobsFromDatabase(string columnFilter = null, string keyword = null)
        {
            var jobs = new List<Job>();

            string query = @"
                SELECT
                    j.JobID,
                    j.CustomerID,
                    c.FullName AS CustomerName,
                    j.StartLocation,
                    j.Destination,
                    j.StartDate,
                    j.EndDate,
                    j.Status,
                    j.TransportUnitID,
                    tu.UnitName
                FROM Job j
                INNER JOIN Customer c ON j.CustomerID = c.CustomerID
                LEFT JOIN TransportUnit tu ON j.TransportUnitID = tu.TransportUnitID
            ";

            if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
            {
                query += $" WHERE {columnFilter} LIKE @keyword";
            }

            query += " ORDER BY j.JobID DESC";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
            {
                parameters.Add("@keyword", $"%{keyword}%");
            }

            DataTable dt = Database.GetData(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                jobs.Add(new Job
                {
                    JobID = Convert.ToInt32(row["JobID"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    CustomerName = row["CustomerName"].ToString(),
                    StartLocation = row["StartLocation"].ToString(),
                    Destination = row["Destination"].ToString(),
                    StartDate = Convert.ToDateTime(row["StartDate"]),
                    EndDate = Convert.ToDateTime(row["EndDate"]),
                    Status = row["Status"].ToString(),
                    TransportUnitID = row["TransportUnitID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TransportUnitID"]),
                    UnitName = row["UnitName"] == DBNull.Value ? string.Empty : row["UnitName"].ToString()
                });
            }

            return jobs;
        }

        private void LoadJobData(string columnFilter = null, string keyword = null)
        {
            try
            {
                var jobs = LoadJobsFromDatabase(columnFilter, keyword);
                dgvJobs.DataSource = new BindingList<Job>(jobs);

                // Set DataGridView column headers and widths
                dgvJobs.Columns["JobID"].HeaderText = "Job ID";
                dgvJobs.Columns["CustomerName"].HeaderText = "Customer";
                dgvJobs.Columns["StartLocation"].HeaderText = "Starting Location";
                dgvJobs.Columns["Destination"].HeaderText = "Destination";
                dgvJobs.Columns["StartDate"].HeaderText = "Start Date";
                dgvJobs.Columns["EndDate"].HeaderText = "End Date";
                dgvJobs.Columns["UnitName"].HeaderText = "Transport Unit";
                dgvJobs.Columns["Status"].HeaderText = "Status";

                dgvJobs.Columns["JobID"].Width = 60;
                dgvJobs.Columns["CustomerName"].Width = 65;
                dgvJobs.Columns["StartLocation"].Width = 140;
                dgvJobs.Columns["Destination"].Width = 140;
                dgvJobs.Columns["StartDate"].Width = 110;
                dgvJobs.Columns["EndDate"].Width = 110;
                dgvJobs.Columns["UnitName"].Width = 90;
                dgvJobs.Columns["Status"].Width = 110;

                // Remove existing Edit/Delete button columns if present
                for (int i = dgvJobs.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvJobs.Columns[i].Name == "Edit" || dgvJobs.Columns[i].Name == "Delete")
                        dgvJobs.Columns.RemoveAt(i);
                }

                var editBtn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50
                };
                dgvJobs.Columns.Add(editBtn);

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

        private void TxtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText) || searchText == "Search...")
            {
                LoadJobData();
                return;
            }

            if (cmbSearchBy.SelectedItem == null) return;

            string selectedColumn = cmbSearchBy.SelectedItem.ToString();

            Dictionary<string, string> columnMap = new Dictionary<string, string>()
            {
                { "JobID", "j.JobID" },
                { "CustomerName", "c.FullName" },
                { "StartLocation", "j.StartLocation" },
                { "Destination", "j.Destination" },
                { "UnitName", "tu.UnitName" },
                { "Status", "j.Status" },
            };

            if (!columnMap.ContainsKey(selectedColumn))
                return;

            string dbColumn = columnMap[selectedColumn];

            LoadJobData(dbColumn, searchText);
        }

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var columnName = dgvJobs.Columns[e.ColumnIndex].Name;
            int jobId = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["JobID"].Value);

            if (columnName == "Edit")
            {
                // Open your Edit Job form - pass jobId
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
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            conn.Open();

                            using (SqlTransaction transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    // 1. Delete all loads related to this job
                                    string deleteLoadsQuery = "DELETE FROM Load WHERE JobID = @JobID";
                                    using (SqlCommand cmd = new SqlCommand(deleteLoadsQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@JobID", jobId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // 2. Delete from TransportUnitSchedule
                                    string deleteScheduleQuery = "DELETE FROM TransportUnitSchedule WHERE JobID = @JobID";
                                    using (SqlCommand cmd = new SqlCommand(deleteScheduleQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@JobID", jobId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // 3. Delete the job itself
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

        private void BtnAddJob_Click(object sender, EventArgs e)
        {
            AddJobForm form = new AddJobForm();
            if (form.ShowDialog() == DialogResult.OK)
                LoadJobData();
        }
    }
}
