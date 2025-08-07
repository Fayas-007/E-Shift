using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;
using E_shift.Forms;
using E_shift.Forms.ReportViewers;

namespace E_shift.Forms
{
    public partial class VehicleManagementControl : UserControl
    {
        public VehicleManagementControl()
        {
            InitializeComponent();
            this.Load += VehicleManagementControl_Load;
            DataGridViewHelper.ApplyTheme(dgvVehicles);
            DataGridViewHelper.AttachCellClickEvent(dgvVehicles, dgvVehicles_CellClick);
            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;


            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                string column = col switch
                {
                    "LorryID" => "LorryID",
                    "RegistrationNumber" => "RegistrationNumber",
                    "Model" => "Model",
                    _ => null
                };

                if (column != null)
                    LoadVehicleData(column, keyword);
                else
                    LoadVehicleData();
            });
            LoadSearchComboItems();
        }

        private void VehicleManagementControl_Load(object sender, EventArgs e)
        {
            LoadVehicleData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.Add("LorryID");
            cmbSearchBy.Items.Add("RegistrationNumber");
            cmbSearchBy.Items.Add("Model");
            cmbSearchBy.SelectedIndex = 0;
        }

        private void LoadVehicleData(string columnFilter = null, string keyword = null)
        {
            try
            {
                string query = @"
            SELECT 
                LorryID, 
                RegistrationNumber, 
                Model, 
                CASE WHEN IsAvailable = 1 THEN 'Yes' ELSE 'No' END AS IsAvailable 
            FROM Lorry";

                var parameters = new Dictionary<string, object>();
                if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                {
                    query += $" WHERE {columnFilter} LIKE @keyword";
                    parameters.Add("@keyword", $"%{keyword}%");
                }

                query += " ORDER BY LorryID DESC";

                DataTable dt = Database.GetData(query, parameters);
                dgvVehicles.DataSource = dt;

                // Hide CreatedAt if it exists
                if (dgvVehicles.Columns.Contains("CreatedAt"))
                    dgvVehicles.Columns["CreatedAt"].Visible = false;

                dgvVehicles.Columns["LorryID"].HeaderText = "ID";
                dgvVehicles.Columns["RegistrationNumber"].HeaderText = "Registration No";
                dgvVehicles.Columns["Model"].HeaderText = "Model";
                dgvVehicles.Columns["IsAvailable"].HeaderText = "Is Available";

                // Remove old buttons
                for (int i = dgvVehicles.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvVehicles.Columns[i] is DataGridViewButtonColumn)
                    {
                        dgvVehicles.Columns.RemoveAt(i);
                    }
                }

                // Add Edit & Delete buttons
                dgvVehicles.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50
                });

                dgvVehicles.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                });

                dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vehicle data: " + ex.Message);
            }
        }


      
        private void dgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dgvVehicles.Columns[e.ColumnIndex].Name;
            int lorryId = Convert.ToInt32(dgvVehicles.Rows[e.RowIndex].Cells["LorryID"].Value);

            if (columnName == "Edit")
            {
                EditVechicleForm editForm = new EditVechicleForm(lorryId);
                if (editForm.ShowDialog() == DialogResult.OK)
                    LoadVehicleData();
            }
            else if (columnName == "Delete")
            {
                // 1️⃣  Check whether this lorry is used in any transport units
                string refCheck = "SELECT COUNT(*) FROM TransportUnit WHERE LorryID = @LorryID";
                var param = new Dictionary<string, object> { { "@LorryID", lorryId } };
                int referenceCount = Convert.ToInt32(Database.ExecuteSingleValue(refCheck, param));

                if (referenceCount > 0)
                {
                    MessageBox.Show(
                        "This vehicle is assigned to one or more transport units and cannot be deleted.\n" +
                        "Please unassign or delete the transport unit(s) first.",
                        "Delete Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;    // ✋ stop here
                }

                // 2️⃣  Ask for confirmation
                var confirm = MessageBox.Show("Are you sure you want to delete this vehicle?",
                                              "Confirm Delete",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                try
                {
                    string delQuery = "DELETE FROM Lorry WHERE LorryID = @LorryID";
                    Database.ExecuteNonQuery(delQuery, param);
                    MessageBox.Show("Vehicle deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVehicleData();
                }
                catch (Exception ex)
                {
                    // Fallback – in case another FK constraint stops the delete
                    if (ex.Message.Contains("REFERENCE") || ex.Message.Contains("FOREIGN KEY"))
                    {
                        MessageBox.Show(
                            "Cannot delete this vehicle because it is in use by other records.",
                            "Delete Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Error deleting vehicle: " + ex.Message,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void VehicleManagementControl_Load_1(object sender, EventArgs e)
        {

        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            AddVehicleForm form = new AddVehicleForm();
            if (form.ShowDialog() == DialogResult.OK)
                LoadVehicleData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvVehicles.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                VehicleReportViewer viewer = new VehicleReportViewer(dt);
                viewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("No data to display in report.");
            }
        }
    }
}
