using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;
using E_shift.Models;

namespace E_shift.Forms
{
    public partial class TransportUnitManagementControl : UserControl
    {
        public TransportUnitManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvTransportUnits);
            DataGridViewHelper.AttachCellClickEvent(dgvTransportUnits, DgvTransportUnits_CellClick);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;

            LoadSearchComboItems();
        }

        private void TransportUnitManagementControl_Load(object sender, EventArgs e)
        {
            LoadTransportUnitData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.Add("TransportUnitID");
            cmbSearchBy.Items.Add("RegistrationNumber");
            cmbSearchBy.Items.Add("DriverName");
            cmbSearchBy.Items.Add("AssistantName");
            cmbSearchBy.Items.Add("ContainerNumber");
            cmbSearchBy.Items.Add("UnitName");
            cmbSearchBy.Items.Add("IsAvailable");
            cmbSearchBy.SelectedIndex = 0;
        }

        private void LoadTransportUnitData(string columnFilter = null, string keyword = null)
        {
            try
            {
                string sql = @"
                    SELECT 
                        t.TransportUnitID,
                        t.UnitName,
                        l.RegistrationNumber,
                        d.Username AS DriverName,
                        a.Username AS AssistantName,
                        c.ContainerNumber,
                        t.IsAvailable,
                        t.CreatedAt
                    FROM TransportUnit t
                    LEFT JOIN Lorry l ON t.LorryID = l.LorryID
                    LEFT JOIN [User] d ON t.DriverID = d.UserID AND d.Role = 'Driver'
                    LEFT JOIN [User] a ON t.AssistantID = a.UserID AND a.Role = 'Assistant'
                    LEFT JOIN Container c ON t.ContainerID = c.ContainerID
                ";

                var parameters = new Dictionary<string, object>();

                if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                {
                    sql += $" WHERE {columnFilter} LIKE @keyword";
                    parameters.Add("@keyword", $"%{keyword}%");
                }

                sql += " ORDER BY t.TransportUnitID DESC";

                var transportUnits = new List<TransportUnitDisplay>();

                using (var conn = Database.GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transportUnits.Add(new TransportUnitDisplay
                            {
                                TransportUnitID = reader.GetInt32(reader.GetOrdinal("TransportUnitID")),
                                UnitName = reader.GetString(reader.GetOrdinal("UnitName")),
                                RegistrationNumber = reader.IsDBNull(reader.GetOrdinal("RegistrationNumber")) ? null : reader.GetString(reader.GetOrdinal("RegistrationNumber")),
                                DriverName = reader.IsDBNull(reader.GetOrdinal("DriverName")) ? null : reader.GetString(reader.GetOrdinal("DriverName")),
                                AssistantName = reader.IsDBNull(reader.GetOrdinal("AssistantName")) ? null : reader.GetString(reader.GetOrdinal("AssistantName")),
                                ContainerNumber = reader.IsDBNull(reader.GetOrdinal("ContainerNumber")) ? null : reader.GetString(reader.GetOrdinal("ContainerNumber")),
                                AvailableDisplay = reader.GetBoolean(reader.GetOrdinal("IsAvailable")) ? "Yes" : "No",
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            });
                        }
                    }
                }

                dgvTransportUnits.DataSource = transportUnits;

                // Set headers and widths
                dgvTransportUnits.Columns["TransportUnitID"].HeaderText = "ID";
                dgvTransportUnits.Columns["UnitName"].HeaderText = "Unit Name";
                dgvTransportUnits.Columns["RegistrationNumber"].HeaderText = "Lorry Registration No";
                dgvTransportUnits.Columns["DriverName"].HeaderText = "Driver";
                dgvTransportUnits.Columns["AssistantName"].HeaderText = "Assistant";
                dgvTransportUnits.Columns["ContainerNumber"].HeaderText = "Container Number";
                dgvTransportUnits.Columns["AvailableDisplay"].HeaderText = "Available";
                dgvTransportUnits.Columns["CreatedAt"].HeaderText = "Created On";

                dgvTransportUnits.Columns["TransportUnitID"].Width = 50;
                dgvTransportUnits.Columns["UnitName"].Width = 120;
                dgvTransportUnits.Columns["RegistrationNumber"].Width = 120;
                dgvTransportUnits.Columns["DriverName"].Width = 120;
                dgvTransportUnits.Columns["AssistantName"].Width = 120;
                dgvTransportUnits.Columns["ContainerNumber"].Width = 120;
                dgvTransportUnits.Columns["AvailableDisplay"].Width = 80;
                dgvTransportUnits.Columns["CreatedAt"].Width = 150;

                // Remove old Edit/Delete buttons if any
                for (int i = dgvTransportUnits.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvTransportUnits.Columns[i].Name == "Edit" || dgvTransportUnits.Columns[i].Name == "Delete")
                        dgvTransportUnits.Columns.RemoveAt(i);
                }

                // Add Edit button
                dgvTransportUnits.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50
                });

                // Add Delete button
                dgvTransportUnits.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                });

                dgvTransportUnits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transport unit data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoadTransportUnitData();
                return;
            }

            if (cmbSearchBy.SelectedItem == null) return;

            string selectedColumn = cmbSearchBy.SelectedItem.ToString();

            Dictionary<string, string> columnMap = new Dictionary<string, string>()
            {
                { "TransportUnitID", "t.TransportUnitID" },
                { "RegistrationNumber", "l.RegistrationNumber" },
                { "DriverName", "d.Username" },
                { "AssistantName", "a.Username" },
                { "ContainerNumber", "c.ContainerNumber" },
                { "UnitName", "t.UnitName" },
                { "IsAvailable", "t.IsAvailable" }
            };

            if (!columnMap.ContainsKey(selectedColumn))
                return;

            string dbColumn = columnMap[selectedColumn];

            LoadTransportUnitData(dbColumn, searchText);
        }

        private void DgvTransportUnits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var columnName = dgvTransportUnits.Columns[e.ColumnIndex].Name;
            int transportUnitId = Convert.ToInt32(dgvTransportUnits.Rows[e.RowIndex].Cells["TransportUnitID"].Value);

            if (columnName == "Edit")
            {
                EditTransportUnit editForm = new EditTransportUnit(transportUnitId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTransportUnitData();
                }
            }
            else if (columnName == "Delete")
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this transport unit?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        string fetchSql = @"
                        SELECT DriverID, AssistantID, LorryID, ContainerID
                        FROM TransportUnit
                        WHERE TransportUnitID = @TransportUnitID";

                        var fetchParams = new Dictionary<string, object> { { "@TransportUnitID", transportUnitId } };
                        var dt = Database.GetData(fetchSql, fetchParams);

                        int? driverId = dt.Rows[0]["DriverID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DriverID"]) : (int?)null;
                        int? assistantId = dt.Rows[0]["AssistantID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["AssistantID"]) : (int?)null;
                        int? lorryId = dt.Rows[0]["LorryID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["LorryID"]) : (int?)null;
                        int? containerId = dt.Rows[0]["ContainerID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["ContainerID"]) : (int?)null;

                        string deleteQuery = "DELETE FROM TransportUnit WHERE TransportUnitID = @TransportUnitID";
                        Database.ExecuteNonQuery(deleteQuery, fetchParams);

                        if (driverId.HasValue)
                        {
                            Database.ExecuteNonQuery(
                                "UPDATE [User] SET IsAvailable = 1 WHERE UserID = @UserID AND Role = 'Driver'",
                                new Dictionary<string, object> { { "@UserID", driverId.Value } });
                        }

                        if (assistantId.HasValue)
                        {
                            Database.ExecuteNonQuery(
                                "UPDATE [User] SET IsAvailable = 1 WHERE UserID = @UserID AND Role = 'Assistant'",
                                new Dictionary<string, object> { { "@UserID", assistantId.Value } });
                        }

                        if (lorryId.HasValue)
                        {
                            Database.ExecuteNonQuery(
                                "UPDATE Lorry SET IsAvailable = 1 WHERE LorryID = @LorryID",
                                new Dictionary<string, object> { { "@LorryID", lorryId.Value } });
                        }

                        if (containerId.HasValue)
                        {
                            Database.ExecuteNonQuery(
                                "UPDATE Container SET IsAvailable = 1 WHERE ContainerID = @ContainerID",
                                new Dictionary<string, object> { { "@ContainerID", containerId.Value } });
                        }

                        LoadTransportUnitData();

                        MessageBox.Show("Transport unit deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("REFERENCE constraint") || ex.Message.Contains("FOREIGN KEY constraint"))
                        {
                            MessageBox.Show("This transport unit cannot be deleted because it is assigned to one or more jobs.",
                                "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Error deleting transport unit: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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

        private void BtnAddTransportUnit_Click(object sender, EventArgs e)
        {
            AddTransportUnit form = new AddTransportUnit();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTransportUnitData();
            }
        }

        private void dgvTransportUnits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optionally add logic here if needed
        }
    }
}
