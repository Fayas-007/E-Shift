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
    public partial class ContainerManagementControl : UserControl
    {
        public ContainerManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvContainers);
            DataGridViewHelper.AttachCellClickEvent(dgvContainers, dgvContainers_CellClick);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            LoadSearchComboItems();

            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadContainerData();
                    return;
                }

                var map = new Dictionary<string, string>
        {
            { "ContainerID", "ContainerID" },
            { "ContainerNumber", "ContainerNumber" },
            { "Description", "Description" },
            { "Capacity", "Capacity" },
            { "IsAvailable", "IsAvailable" }
        };

                if (!map.TryGetValue(col ?? "", out string dbCol))
                    return;

                if (dbCol == "ContainerID" && !int.TryParse(keyword, out _))
                    return;

                LoadContainerData(dbCol, keyword);
            });

            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
        }


        private void ContainerManagementControl_Load(object sender, EventArgs e)
        {
            LoadContainerData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.Add("ContainerID");
            cmbSearchBy.Items.Add("ContainerNumber");
            cmbSearchBy.Items.Add("Description");
            cmbSearchBy.Items.Add("Capacity");
            cmbSearchBy.Items.Add("IsAvailable");
            cmbSearchBy.SelectedIndex = 0;
        }

        private void LoadContainerData(string col = null, string kw = null)
        {
            string sql = @"
                SELECT
                    ContainerID,
                    ContainerNumber,
                    Description,
                    Capacity,
                    IsAvailable,
                    CreatedAt
                FROM Container";

            var prm = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(col) && !string.IsNullOrWhiteSpace(kw))
            {
                sql += $" WHERE {col} LIKE @kw";
                prm.Add("@kw", $"%{kw}%");
            }

            sql += " ORDER BY ContainerID DESC";

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (var p in prm)
                        cmd.Parameters.AddWithValue(p.Key, p.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                // Add computed column for display availability
                if (!dt.Columns.Contains("AvailableDisplay"))
                {
                    dt.Columns.Add("AvailableDisplay", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        bool isAvailable = false;
                        if (row["IsAvailable"] != DBNull.Value)
                            isAvailable = Convert.ToBoolean(row["IsAvailable"]);

                        row["AvailableDisplay"] = isAvailable ? "Yes" : "No";
                    }
                }

                dgvContainers.DataSource = dt;

                // Set column headers and visibility
                dgvContainers.Columns["ContainerID"].HeaderText = "ID";
                dgvContainers.Columns["ContainerNumber"].HeaderText = "Number";
                dgvContainers.Columns["Description"].HeaderText = "Description";
                dgvContainers.Columns["Capacity"].HeaderText = "Capacity";

                dgvContainers.Columns["IsAvailable"].Visible = false;
                dgvContainers.Columns["CreatedAt"].Visible = false;

                if (dgvContainers.Columns.Contains("AvailableDisplay"))
                {
                    dgvContainers.Columns["AvailableDisplay"].HeaderText = "Available";
                    dgvContainers.Columns["AvailableDisplay"].DisplayIndex = 4; // after Capacity
                    dgvContainers.Columns["AvailableDisplay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                // Remove existing Edit/Delete buttons
                for (int i = dgvContainers.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvContainers.Columns[i].Name == "Edit" || dgvContainers.Columns[i].Name == "Delete")
                        dgvContainers.Columns.RemoveAt(i);
                }

                // Add Edit button
                dgvContainers.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50
                });

                // Add Delete button
                dgvContainers.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                });

                dgvContainers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading container data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable CreateReportDataTable(DataTable dtOriginal)
        {
            // Clone original table structure and add new AvailableDisplay column
            DataTable dt = dtOriginal.Clone();
            dt.Columns.Add("AvailableDisplay", typeof(string));

            // Copy rows and set AvailableDisplay based on IsAvailable column
            foreach (DataRow row in dtOriginal.Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = row.ItemArray; // copy all existing columns

                bool isAvailable = false;
                if (row["IsAvailable"] != DBNull.Value)
                    isAvailable = (bool)row["IsAvailable"];

                newRow["AvailableDisplay"] = isAvailable ? "Yes" : "No";

                dt.Rows.Add(newRow);
            }

            return dt;
        }


        private void dgvContainers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = Convert.ToInt32(dgvContainers.Rows[e.RowIndex].Cells["ContainerID"].Value);
            string col = dgvContainers.Columns[e.ColumnIndex].Name;

            if (col == "Edit")
            {
                EditContainerForm f = new EditContainerForm(id);
                if (f.ShowDialog() == DialogResult.OK)
                    LoadContainerData();
            }
            else if (col == "Delete")
            {
                if (MessageBox.Show("Delete this container?", "Confirm",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        Database.ExecuteNonQuery(
                            "DELETE FROM Container WHERE ContainerID = @id",
                            new Dictionary<string, object> { { "@id", id } });

                        LoadContainerData();
                    }
                    catch (SqlException ex) when (ex.Number == 547) // FK violation
                    {
                        MessageBox.Show("Delete Blocked – Container is assigned to a job.",
                                        "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete failed: " + ex.Message, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnAddContainer_Click(object sender, EventArgs e)
        {
            AddContainerForm form = new AddContainerForm();
            if (form.ShowDialog() == DialogResult.OK) LoadContainerData();
        }

        private void cmbSearchBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Color c1 = Color.FromArgb(30, 144, 255);
            Color c2 = Color.FromArgb(25, 120, 230);

            using (var br = new System.Drawing.Drawing2D.LinearGradientBrush(
                       e.Bounds, c1, c2, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (var hl = new SolidBrush(Color.FromArgb(100, Color.LightSkyBlue)))
                {
                    e.Graphics.FillRectangle(hl, e.Bounds);
                }
            }

            string text = ((ComboBox)sender).Items[e.Index].ToString();
            TextRenderer.DrawText(e.Graphics, text, cmbSearchBy.Font,
                                  new Rectangle(e.Bounds.Left + 8, e.Bounds.Top, e.Bounds.Width - 8, e.Bounds.Height),
                                  Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            e.DrawFocusRectangle();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dgvContainers.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                ContainerReportViewer viewer = new ContainerReportViewer(dt);
                viewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("No data to display in report.");
            }
        }

        private void dgvContainers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
