using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;
using E_shift.Models;
namespace E_shift.Forms
{
    public partial class ContainerManagementControl : UserControl
    {
        public ContainerManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvContainers);
            DataGridViewHelper.AttachCellClickEvent(dgvContainers, dgvContainers_CellClick);

            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            LoadSearchComboItems();
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

            List<Container> containers = new List<Container>();

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                foreach (var p in prm)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        containers.Add(new Container
                        {
                            ContainerID = reader.GetInt32(reader.GetOrdinal("ContainerID")),
                            ContainerNumber = reader.GetString(reader.GetOrdinal("ContainerNumber")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                            IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        });
                    }
                }
            }

            dgvContainers.DataSource = containers;

            // Set headers and widths
            dgvContainers.Columns["ContainerID"].HeaderText = "ID";
            dgvContainers.Columns["ContainerNumber"].HeaderText = "Number";
            dgvContainers.Columns["Description"].HeaderText = "Description";
            dgvContainers.Columns["Capacity"].HeaderText = "Capacity";
            dgvContainers.Columns["AvailableDisplay"].HeaderText = "Available";

            // Hidign unwnted columns
            dgvContainers.Columns["IsAvailable"].Visible = false;
            dgvContainers.Columns["CreatedAt"].Visible = false;

            // Remove existing buttons first
            for (int i = dgvContainers.Columns.Count - 1; i >= 0; i--)
            {
                if (dgvContainers.Columns[i].Name == "Edit" || dgvContainers.Columns[i].Name == "Delete")
                    dgvContainers.Columns.RemoveAt(i);
            }

            // Add Edit and Delete buttons
            dgvContainers.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 50
            });

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
            string kw = txtSearch.Text.Trim();
            if (kw == "" || kw == "Search...")
            {
                LoadContainerData();
                return;
            }

            if (cmbSearchBy.SelectedItem == null) return;

            string uiCol = cmbSearchBy.SelectedItem.ToString();

            var map = new Dictionary<string, string>
            {
                { "ContainerID", "ContainerID" },
                { "ContainerNumber", "ContainerNumber" },
                { "Description", "Description" },
                { "Capacity", "Capacity" },
                { "IsAvailable", "IsAvailable" }
            };

            if (!map.ContainsKey(uiCol)) return;

            LoadContainerData(map[uiCol], kw);
        }

        private void dgvContainers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = Convert.ToInt32(dgvContainers.Rows[e.RowIndex].Cells["ContainerID"].Value);
            string col = dgvContainers.Columns[e.ColumnIndex].Name;

            if (col == "Edit")
            {

                EditContainerForm f = new EditContainerForm(id);
                if (f.ShowDialog() == DialogResult.OK) LoadContainerData();
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
    }
}
