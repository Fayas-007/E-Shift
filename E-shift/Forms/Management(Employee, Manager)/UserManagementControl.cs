using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Forms.ReportViewers;
using E_shift.Helpers;
using E_shift.Models;

namespace E_shift.Forms
{
    public partial class UserManagementControl : UserControl
    {
        public UserManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvUsers);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;
            dgvUsers.CellContentClick += DgvUsers_CellContentClick;

            LoadSearchComboItems();

            // Attach search helper - wires GotFocus, LostFocus, and TextChanged for you
            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadUserData();
                    return;
                }

                // Map columns to db columns if needed
                string dbCol = col switch
                {
                    "UserID" => "UserID",
                    "Username" => "Username",
                    "Role" => "Role",
                    "Availability" => "IsAvailable", 
                    _ => null
                };
                if (dbCol != null)
                    LoadUserData(dbCol, keyword);
            });

            // Initialize search box text and color manually if you want default placeholder
            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
        }


        private void UserManagementControl_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadSearchComboItems()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.Add("UserID");
            cmbSearchBy.Items.Add("Username");
            cmbSearchBy.Items.Add("Role");
            cmbSearchBy.Items.Add("Availability");
            cmbSearchBy.SelectedIndex = 0;
        }

        private void LoadUserData(string columnFilter = null, string keyword = null)
        {
            try
            {
                string sql = "SELECT UserID, Username, Password, Role, IsAvailable FROM [User]";
                var parameters = new Dictionary<string, object>();

                if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                {
                    if (columnFilter == "IsAvailable")
                    {
                        if (keyword.Equals("yes", StringComparison.OrdinalIgnoreCase))
                            parameters.Add("@keyword", 1);
                        else if (keyword.Equals("no", StringComparison.OrdinalIgnoreCase))
                            parameters.Add("@keyword", 0);
                        else
                        {
                            dgvUsers.DataSource = new DataTable(); // empty grid
                            return;
                        }

                        sql += " WHERE IsAvailable = @keyword";
                    }
                    else
                    {
                        sql += $" WHERE {columnFilter} LIKE @keyword";
                        parameters.Add("@keyword", $"%{keyword}%");
                    }
                }

                sql += " ORDER BY UserID DESC";

                // Get data as DataTable
                DataTable dt = Database.GetData(sql, parameters);

                // Add a friendly Availability column (Yes/No)
                if (!dt.Columns.Contains("AvailabilityDisplay"))
                    dt.Columns.Add("AvailabilityDisplay", typeof(string));

                foreach (DataRow row in dt.Rows)
                    row["AvailabilityDisplay"] = (bool)row["IsAvailable"] ? "Yes" : "No";

                // Bind directly to DataTable (enables sorting arrows!)
                dgvUsers.DataSource = dt;

                // Set column headers
                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Password"].Visible = false;
                dgvUsers.Columns["Role"].HeaderText = "Role";
                dgvUsers.Columns["IsAvailable"].Visible = false;
                dgvUsers.Columns["UserID"].Width = 90;   
                dgvUsers.Columns["Username"].Width = 160; 
                dgvUsers.Columns["Role"].Width = 120; 
                dgvUsers.Columns["AvailabilityDisplay"].Width = 150;   
                if (dgvUsers.Columns.Contains("AvailabilityDisplay"))
                {
                    dgvUsers.Columns["AvailabilityDisplay"].HeaderText = "Available";
                    dgvUsers.Columns["AvailabilityDisplay"].SortMode = DataGridViewColumnSortMode.Automatic;
                }

                // Remove existing Edit/Delete button columns if already added
                for (int i = dgvUsers.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvUsers.Columns[i].Name == "Edit" || dgvUsers.Columns[i].Name == "Delete")
                        dgvUsers.Columns.RemoveAt(i);
                }

                // Add Edit button column
                dgvUsers.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });

                // Add Delete button column
                dgvUsers.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 60,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });

                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dgvUsers.Columns[e.ColumnIndex].Name;
            int userId = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value);

            if (columnName == "Delete")
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        string deleteQuery = "DELETE FROM [User] WHERE UserID = @UserID";
                        Database.ExecuteNonQuery(deleteQuery, new Dictionary<string, object> { { "@UserID", userId } });

                        LoadUserData();

                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("REFERENCE constraint") || ex.Message.Contains("FOREIGN KEY constraint"))
                        {
                            MessageBox.Show("This user cannot be deleted because it is referenced in other records.[Transport Unit]",
                                "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (columnName == "Edit")
            {
                EditUserForm editForm = new EditUserForm(userId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUserData();
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

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            AddUserForm form = new AddUserForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUserData();
            }
        }

        private void dgvUsers_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvUsers.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                UsersReportViewer viewer = new UsersReportViewer(dt);
                viewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("No data to display in report.");
            }
        }
    }
}
