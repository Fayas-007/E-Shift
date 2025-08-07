using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.DataSets;
using E_shift.Forms.ReportViewers;
using E_shift.Helpers;

namespace E_shift.Forms
{
    public partial class LoadManagementControl : UserControl
    {
        public LoadManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvLoads);
            DataGridViewHelper.AttachCellClickEvent(dgvLoads, DgvLoads_CellClick);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            LoadSearchComboItems();

            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                if (string.IsNullOrWhiteSpace(keyword))     
                {
                    LoadLoadData();
                    return;
                }

                // Map the visible combo items to SQL columns / expressions
                var map = new Dictionary<string, string>
                {
                    { "LoadID",        "l.LoadID" },
                    { "JobID",         "l.JobID" },
                    { "CustomerName",  "c.FullName" },
                    { "Status",        "l.Status" },
                    { "StartLocation", "l.StartLocation" },
                    { "EndLocation",   "l.EndLocation" },
                    { "StartDate",     "CONVERT(varchar, l.StartDate, 23)" },
                    { "EndDate",       "CONVERT(varchar, l.EndDate, 23)" }
                };

                if (!map.TryGetValue(col ?? "", out string dbCol))
                    return;

                if (col == "Status")
                {

                    keyword = keyword.Replace(" ", "");
                }

                LoadLoadData(dbCol, keyword);
            });

            // optional placeholder:
            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
        }


        private void LoadManagementControl_Load(object sender, EventArgs e)
        {
            LoadLoadData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.Add("LoadID");
            cmbSearchBy.Items.Add("JobID");
            cmbSearchBy.Items.Add("CustomerName");
            cmbSearchBy.Items.Add("Status");
            cmbSearchBy.Items.Add("StartLocation"); 
            cmbSearchBy.Items.Add("EndLocation");   
            cmbSearchBy.Items.Add("Status");
            cmbSearchBy.Items.Add("StartDate");
            cmbSearchBy.Items.Add("EndDate");
            cmbSearchBy.SelectedIndex = 0;
        }

        private DataTable GetLoadDataTable(string filterColumn = null, string keyword = null)
        {
            string sql = @"
    SELECT 
        l.LoadID,
        l.JobID,
        c.FullName AS CustomerName,
        l.Weight,
        l.Status, 
        l.CreatedAt,
        l.StartDate,
        l.EndDate,
        ISNULL(l.StartLocation, '') AS StartLocation,
        ISNULL(l.EndLocation, '') AS EndLocation,
        tu.UnitName AS TransportUnitName
    FROM Load l
    INNER JOIN Job j ON l.JobID = j.JobID
    INNER JOIN Customer c ON j.CustomerID = c.CustomerID
    LEFT JOIN TransportUnit tu ON l.TransportUnitID = tu.TransportUnitID";

            if (!string.IsNullOrWhiteSpace(filterColumn) && !string.IsNullOrWhiteSpace(keyword))
            {
                sql += $" WHERE {filterColumn} LIKE @keyword";
            }

            sql += " ORDER BY l.LoadID DESC";

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

        private void LoadLoadData(string columnFilter = null, string keyword = null)
        {
            try
            {
                var dt = GetLoadDataTable(columnFilter, keyword);
                int scrollPosition = dgvLoads.FirstDisplayedScrollingRowIndex >= 0 ? dgvLoads.FirstDisplayedScrollingRowIndex : 0;
                DataGridViewCell currentCell = dgvLoads.CurrentCell;

                dgvLoads.DataSource = null;
                dgvLoads.Columns.Clear(); 
                dgvLoads.DataSource = dt;

                // Hide CreatedAt column if present
                if (dgvLoads.Columns.Contains("CreatedAt"))
                {
                    dgvLoads.Columns["CreatedAt"].Visible = false;
                }

                // Set headers and widths for main columns
                dgvLoads.Columns["LoadID"].HeaderText = "Load ID";
                dgvLoads.Columns["JobID"].HeaderText = "Job ID";
                dgvLoads.Columns["TransportUnitName"].HeaderText = "TP Unit Name";
                dgvLoads.Columns["CustomerName"].HeaderText = "Customer Name";
                dgvLoads.Columns["Weight"].HeaderText = "Weight (kg)";
                dgvLoads.Columns["Status"].HeaderText = "Status";
                dgvLoads.Columns["StartDate"].HeaderText = "Start Date";
                dgvLoads.Columns["EndDate"].HeaderText = "End Date";
                dgvLoads.Columns["StartLocation"].HeaderText = "Start Location";  // new
                dgvLoads.Columns["EndLocation"].HeaderText = "End Location";      // new

                dgvLoads.Columns["LoadID"].Width = 60;
                dgvLoads.Columns["JobID"].Width = 70;
                dgvLoads.Columns["CustomerName"].Width = 130;
                dgvLoads.Columns["Weight"].Width = 90;
                dgvLoads.Columns["Status"].Width = 80;
                dgvLoads.Columns["TransportUnitName"].Width = 140;
                dgvLoads.Columns["StartLocation"].Width = 120;  // adjust as needed
                dgvLoads.Columns["EndLocation"].Width = 120;    // adjust as needed


                // Add Edit button column
                var editBtn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };
                dgvLoads.Columns.Add(editBtn);

                // Add Delete button column
                var deleteBtn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                };
                dgvLoads.Columns.Add(deleteBtn);

                // Set AutoSize to fill remaining space nicely
                dgvLoads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading load data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DgvLoads_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var columnName = dgvLoads.Columns[e.ColumnIndex].Name;
            int loadId = Convert.ToInt32(dgvLoads.Rows[e.RowIndex].Cells["LoadID"].Value);

            if (columnName == "Edit")
            {
                EditLoadForm editForm = new EditLoadForm(loadId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadLoadData();
                }
            }
            else if (columnName == "Delete")
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this load?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = Database.GetConnection())
                        {
                            conn.Open();
                            using (var tx = conn.BeginTransaction())
                            {
                                // Delete related LoadSchedule entries first
                                string deleteScheduleSql = "DELETE FROM LoadSchedule WHERE LoadID = @LoadID";
                                using (var cmd = new SqlCommand(deleteScheduleSql, conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@LoadID", loadId);
                                    cmd.ExecuteNonQuery();
                                }

                                // Delete the Load entry
                                string deleteLoadSql = "DELETE FROM Load WHERE LoadID = @LoadID";
                                using (var cmd = new SqlCommand(deleteLoadSql, conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@LoadID", loadId);
                                    cmd.ExecuteNonQuery();
                                }

                                tx.Commit();
                            }
                        }
                        LoadLoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting load: " + ex.Message);
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
     
        private void BtnAddLoad_Click(object sender, EventArgs e)
        {
            AddLoadForm form = new AddLoadForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadLoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dt = GetLoadDataTable();  // get all data, no filter
            LoadReportViewer viewer = new LoadReportViewer(dt); // pass typed DataTable
            viewer.ShowDialog();
        }
    }
}
