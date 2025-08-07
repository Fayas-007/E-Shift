using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;
using E_shift.Models;
using E_shift.Forms.ReportViewers;

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

            LoadSearchComboItems();
            SetupDataGridViewColumns();

            // 🔄 1️⃣  USE SearchHelper instead of manual handlers
            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    RefreshGrid();
                    return;
                }

                var columnMap = new Dictionary<string, string>
                    {
                        { "TransportUnitID",  "t.TransportUnitID" },
                        { "RegistrationNumber","l.RegistrationNumber" },
                        { "DriverName",       "d.Username" },
                        { "AssistantName",    "a.Username" },
                        { "ContainerNumber",  "c.ContainerNumber" },
                        { "UnitName",         "t.UnitName" },
                        { "IsAvailable",      "t.IsAvailable" }
                    };

                if (columnMap.TryGetValue(col ?? "", out string dbCol))
                {
                    if (col == "IsAvailable")
                    {
                        if (keyword.Equals("yes", StringComparison.OrdinalIgnoreCase))
                            RefreshGrid(dbCol, "1");
                        else if (keyword.Equals("no", StringComparison.OrdinalIgnoreCase))
                            RefreshGrid(dbCol, "0");
                        else
                            RefreshGrid(); // reset for invalid keyword
                    }
                    else
                    {
                        RefreshGrid(dbCol, keyword);
                    }
                }
            });


            // Optional placeholder text (SearchHelper clears/restores it automatically):
            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
        }


        private void TransportUnitManagementControl_Load(object sender, EventArgs e)
        {
            RefreshGrid();  // Load data without filter initially
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.AddRange(new object[]
            {
                "TransportUnitID",
                "RegistrationNumber",
                "DriverName",
                "AssistantName",
                "ContainerNumber",
                "UnitName",
                "IsAvailable"
            });
            cmbSearchBy.SelectedIndex = 0;
        }

        private void SetupDataGridViewColumns()
        {
            dgvTransportUnits.Columns.Clear();

            dgvTransportUnits.AutoGenerateColumns = false;

            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TransportUnitID",
                HeaderText = "ID",
                DataPropertyName = "TransportUnitID",
                Width = 50
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitName",
                HeaderText = "Unit Name",
                DataPropertyName = "UnitName",
                Width = 120
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RegistrationNumber",
                HeaderText = "Lorry Registration No",
                DataPropertyName = "RegistrationNumber",
                Width = 150
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DriverName",
                HeaderText = "Driver",
                DataPropertyName = "DriverName",
                Width = 120
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AssistantName",
                HeaderText = "Assistant",
                DataPropertyName = "AssistantName",
                Width = 120
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContainerNumber",
                HeaderText = "Container Number",
                DataPropertyName = "ContainerNumber",
                Width = 120
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AvailableDisplay",
                HeaderText = "Available",
                DataPropertyName = "AvailableDisplay",
                Width = 80
            });
            dgvTransportUnits.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedAt",
                HeaderText = "Created On",
                DataPropertyName = "CreatedAt",
                Width = 150
            });

            dgvTransportUnits.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 50
            });
            dgvTransportUnits.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 60
            });

            dgvTransportUnits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private DataTable FetchTransportUnitData(string filterColumn = null, string keyword = null)
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
                LEFT JOIN Container c ON t.ContainerID = c.ContainerID";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(filterColumn) && !string.IsNullOrWhiteSpace(keyword))
            {
                sql += $" WHERE {filterColumn} LIKE @keyword";
                parameters["@keyword"] = $"%{keyword}%";
            }

            sql += " ORDER BY t.TransportUnitID DESC";

            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            // Add computed AvailableDisplay column
            if (!dt.Columns.Contains("AvailableDisplay"))
            {
                dt.Columns.Add("AvailableDisplay", typeof(string));
                foreach (DataRow r in dt.Rows)
                {
                    bool avail = r["IsAvailable"] != DBNull.Value && (bool)r["IsAvailable"];
                    r["AvailableDisplay"] = avail ? "Yes" : "No";
                }
            }

            return dt;
        }

        private void RefreshGrid(string filterColumn = null, string keyword = null)
        {
            var dt = FetchTransportUnitData(filterColumn, keyword);
            dgvTransportUnits.DataSource = dt;
        }

     

        private void DgvTransportUnits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dgvTransportUnits.Columns[e.ColumnIndex].Name;
            int transportUnitId = Convert.ToInt32(dgvTransportUnits.Rows[e.RowIndex].Cells["TransportUnitID"].Value);

            if (columnName == "Edit")
            {
                var editForm = new EditTransportUnit(transportUnitId);
                if (editForm.ShowDialog() == DialogResult.OK)
                    RefreshGrid();
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

                        RefreshGrid();

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
                RefreshGrid();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = FetchTransportUnitData();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No transport unit data to show in report.");
                return;
            }

            TransportUnitReportViewer viewer = new TransportUnitReportViewer(dt);
            viewer.ShowDialog();
        }
    }
}
