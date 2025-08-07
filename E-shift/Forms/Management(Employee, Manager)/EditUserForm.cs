using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms
{
    public partial class EditUserForm : Form
    {
        private int userId;
        private bool isAssigned;  // Flag if user is assigned to jobs/transport units

        public EditUserForm(int userId)
        {
            InitializeComponent();

            this.userId = userId;
            this.Load += EditUserForm_Load;  // Hook up Load event
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            LoadRoleCombo();     // Load role options first
            LoadUserData();      // Load current user data from DB and set controls
            CheckIfUserAssigned();  // Check if user is assigned somewhere
            SetupAvailabilityControl(); // Enable/disable availability based on assignment
        }

        private void LoadRoleCombo()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("manager");
            cmbRole.Items.Add("employee");
            cmbRole.Items.Add("driver");
            cmbRole.Items.Add("assistant");
            cmbRole.SelectedIndex = 0; // default to first role
        }

        private void LoadUserData()
        {
            string query = "SELECT Username, Role, IsAvailable FROM [User] WHERE UserID = @UserID";
            var parameters = new Dictionary<string, object> { { "@UserID", userId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            var row = dt.Rows[0];

            txtUsername.Text = row["Username"].ToString();

            string role = row["Role"].ToString();
            if (cmbRole.Items.Contains(role))
            {
                cmbRole.SelectedItem = role;
            }

            bool isAvailable = Convert.ToBoolean(row["IsAvailable"]);
            cmbIsAvailable.Items.Clear();
            cmbIsAvailable.Items.Add("Yes");
            cmbIsAvailable.Items.Add("No");
            cmbIsAvailable.SelectedIndex = isAvailable ? 0 : 1;
        }

        private void CheckIfUserAssigned()
        {
            string query = @"
            SELECT COUNT(*) AS AssignedCount
            FROM TransportUnit
            WHERE DriverID = @UserID OR AssistantID = @UserID";

            var parameters = new Dictionary<string, object> { { "@UserID", userId } };

            int assignedCount = Convert.ToInt32(Database.GetData(query, parameters).Rows[0]["AssignedCount"]);

            isAssigned = assignedCount > 0;

            lblAssignmentWarning.Visible = isAssigned;
        }

        private void SetupAvailabilityControl()
        {
            if (isAssigned)
            {
                cmbIsAvailable.Enabled = false;
                lblAssignmentWarning.Text = "This user is assigned to active jobs or transport units. Availability cannot be changed.";
                lblAssignmentWarning.Visible = true;
            }
            else
            {
                cmbIsAvailable.Enabled = true;
                lblAssignmentWarning.Visible = false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbIsAvailable.SelectedIndex < 0)
            {
                MessageBox.Show("Please select availability.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string updateQuery = @"
                    UPDATE [User]
                    SET Username = @Username,
                        Role = @Role,
                        IsAvailable = @IsAvailable
                    WHERE UserID = @UserID";

                var parameters = new Dictionary<string, object>
                {
                    { "@Username", txtUsername.Text.Trim() },
                    { "@Role", cmbRole.SelectedItem.ToString() },
                    // If availability combo is disabled, keep current value from DB
                    { "@IsAvailable", cmbIsAvailable.Enabled ? (cmbIsAvailable.SelectedIndex == 0 ? 1 : 0) : GetCurrentAvailability() },
                    { "@UserID", userId }
                };

                Database.ExecuteNonQuery(updateQuery, parameters);

                MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCurrentAvailability()
        {
            string query = "SELECT IsAvailable FROM [User] WHERE UserID = @UserID";
            var param = new Dictionary<string, object> { { "@UserID", userId } };
            return Convert.ToInt32(Database.GetData(query, param).Rows[0]["IsAvailable"]);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
