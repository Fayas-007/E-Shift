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
        private DataTable dtOriginalUsers;

        public UserManagementControl()
        {
            InitializeComponent();

            DataGridViewHelper.ApplyTheme(dgvUsers);

            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;
            dgvUsers.CellContentClick += DgvUsers_CellContentClick;

            LoadSearchComboItems();

            // Using search helper for linear (in-memory) search
            SearchHelper.AttachSearch(txtSearch, cmbSearchBy, (col, keyword) =>
            {
                if (string.IsNullOrWhiteSpace(keyword) || string.IsNullOrWhiteSpace(col))
                {
                    dgvUsers.DataSource = dtOriginalUsers;
                    return;
                }

                if (dtOriginalUsers == null) return;

                dgvUsers.DataSource = SearchHelper.LinearSearch(dtOriginalUsers, col, keyword);
            });


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

        private void LoadUserData()
        {
            try
            {
                string sql = "SELECT UserID, Username, Password, Role, IsAvailable FROM [User] ORDER BY UserID DESC";
                dtOriginalUsers = Database.GetData(sql);

                if (!dtOriginalUsers.Columns.Contains("AvailabilityDisplay"))
                    dtOriginalUsers.Columns.Add("AvailabilityDisplay", typeof(string));

                foreach (DataRow row in dtOriginalUsers.Rows)
                    row["AvailabilityDisplay"] = (bool)row["IsAvailable"] ? "Yes" : "No";

                dgvUsers.DataSource = dtOriginalUsers;

                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Password"].Visible = false;
                dgvUsers.Columns["Role"].HeaderText = "Role";
                dgvUsers.Columns["IsAvailable"].Visible = false;

                if (dgvUsers.Columns.Contains("AvailabilityDisplay"))
                {
                    dgvUsers.Columns["AvailabilityDisplay"].HeaderText = "Available";
                    dgvUsers.Columns["AvailabilityDisplay"].SortMode = DataGridViewColumnSortMode.Automatic;
                }

                dgvUsers.Columns["UserID"].Width = 90;
                dgvUsers.Columns["Username"].Width = 160;
                dgvUsers.Columns["Role"].Width = 120;
                dgvUsers.Columns["AvailabilityDisplay"].Width = 150;

                // Remove old Edit/Delete buttons
                for (int i = dgvUsers.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvUsers.Columns[i].Name == "Edit" || dgvUsers.Columns[i].Name == "Delete")
                        dgvUsers.Columns.RemoveAt(i);
                }

                dgvUsers.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 50,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });

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
                var confirmResult = MessageBox.Show("Are you sure to delete this user? [All related records will be deleted]",
                                                    "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        // Fetch role and availability
                        string userInfoQuery = "SELECT Role, IsAvailable FROM [User] WHERE UserID = @UserID";
                        var userInfo = Database.GetData(userInfoQuery, new Dictionary<string, object> { { "@UserID", userId } });

                        if (userInfo.Rows.Count == 0)
                        {
                            MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string role = userInfo.Rows[0]["Role"].ToString();
                        bool isAvailable = Convert.ToBoolean(userInfo.Rows[0]["IsAvailable"]);

                        if (role.Equals("customer", StringComparison.OrdinalIgnoreCase))
                        {
                            // Delete from Job and Load if applicable, then Customer, then User
                            string deleteJobs = "DELETE FROM Job WHERE CustomerID = (SELECT CustomerID FROM Customer WHERE UserID = @UserID)";
                            Database.ExecuteNonQuery(deleteJobs, new Dictionary<string, object> { { "@UserID", userId } });

                            string deleteCustomer = "DELETE FROM Customer WHERE UserID = @UserID";
                            Database.ExecuteNonQuery(deleteCustomer, new Dictionary<string, object> { { "@UserID", userId } });

                            string deleteUser = "DELETE FROM [User] WHERE UserID = @UserID";
                            Database.ExecuteNonQuery(deleteUser, new Dictionary<string, object> { { "@UserID", userId } });

                            MessageBox.Show("Customer and associated data deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (!isAvailable)
                            {
                                MessageBox.Show("This user is assigned and cannot be deleted. Please unassign or make them available before deleting.",
                                                "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Delete user if not assigned
                            string deleteUser = "DELETE FROM [User] WHERE UserID = @UserID";
                            Database.ExecuteNonQuery(deleteUser, new Dictionary<string, object> { { "@UserID", userId } });

                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadUserData();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("REFERENCE constraint") || ex.Message.Contains("FOREIGN KEY constraint"))
                        {
                            MessageBox.Show("This user cannot be deleted because it is referenced in other records.",
                                "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }else if (columnName == "Edit")
            {
                try
                {

                    EditUserForm editForm = new EditUserForm(userId);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadUserData(); // Refresh grid after editing
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading edit form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        
        private void dgvUsers_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
