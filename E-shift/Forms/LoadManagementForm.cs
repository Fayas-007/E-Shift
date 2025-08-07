using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
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

            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;

            LoadSearchComboItems();
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
            cmbSearchBy.Items.Add("Description");
            cmbSearchBy.SelectedIndex = 0;
        }

        private void LoadLoadData(string columnFilter = null, string keyword = null)
        {
            try
            {
                string baseQuery = @"
            SELECT 
                l.LoadID,
                l.JobID,
                c.FullName AS CustomerName,
                l.Description,
                l.Weight,
                l.CreatedAt
            FROM Load l
            INNER JOIN Job j ON l.JobID = j.JobID
            INNER JOIN Customer c ON j.CustomerID = c.CustomerID";

                if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                {
                    baseQuery += $" WHERE {columnFilter} LIKE @keyword";
                }

                baseQuery += " ORDER BY l.LoadID DESC";

                var parameters = new Dictionary<string, object>();
                if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                {
                    parameters.Add("@keyword", $"%{keyword}%");
                }

                DataTable dt = Database.GetData(baseQuery, parameters);
                dgvLoads.DataSource = dt;

                // Set column headers and widths as before
                dgvLoads.Columns["LoadID"].HeaderText = "Load ID";
                dgvLoads.Columns["JobID"].HeaderText = "Job ID";
                dgvLoads.Columns["CustomerName"].HeaderText = "Customer Name";
                dgvLoads.Columns["Description"].HeaderText = "Description";
                dgvLoads.Columns["Weight"].HeaderText = "Weight (kg)";
                dgvLoads.Columns["CreatedAt"].HeaderText = "Created On";

                dgvLoads.Columns["LoadID"].Width = 60;
                dgvLoads.Columns["JobID"].Width = 65;
                dgvLoads.Columns["CustomerName"].Width = 120;
                dgvLoads.Columns["Description"].Width = 240;
                dgvLoads.Columns["Weight"].Width = 100;
                dgvLoads.Columns["CreatedAt"].Width = 150;

                // Remove old Edit/Delete buttons if present
                for (int i = dgvLoads.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvLoads.Columns[i].Name == "Edit" || dgvLoads.Columns[i].Name == "Delete")
                        dgvLoads.Columns.RemoveAt(i);
                }

                // Add Edit/Delete buttons
                var editBtn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50
                };
                dgvLoads.Columns.Add(editBtn);

                var deleteBtn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                };
                dgvLoads.Columns.Add(deleteBtn);

                dgvLoads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading load data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    LoadLoadData();
                    return;
                }

                if (cmbSearchBy.SelectedItem == null) return;

                string selectedColumn = cmbSearchBy.SelectedItem.ToString();

                // Map search dropdown items to actual DB columns
                Dictionary<string, string> columnMap = new Dictionary<string, string>()
                    {
                        { "LoadID", "l.LoadID" },
                        { "JobID", "l.JobID" },
                        { "CustomerName", "c.FullName" },
                        { "Description", "l.Description" }
                    };

                if (!columnMap.ContainsKey(selectedColumn))
                    return;

                string dbColumn = columnMap[selectedColumn];

                try
                {
                    string query = $@"
                        SELECT 
                            l.LoadID,
                            l.JobID,
                            c.FullName AS CustomerName,
                            l.Description,
                            l.Weight,
                            l.CreatedAt
                        FROM Load l
                        INNER JOIN Job j ON l.JobID = j.JobID
                        INNER JOIN Customer c ON j.CustomerID = c.CustomerID
                        WHERE {dbColumn} LIKE @searchText
                        ORDER BY l.LoadID DESC";

                    var parameters = new Dictionary<string, object>
            {
                { "@searchText", $"%{searchText}%" }
            };

                DataTable dt = Database.GetData(query, parameters);
                dgvLoads.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
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
                        string deleteQuery = "DELETE FROM Load WHERE LoadID = @LoadID";
                        Database.ExecuteNonQuery(deleteQuery, new Dictionary<string, object> { { "@LoadID", loadId } });
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

        private void dgvLoads_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
