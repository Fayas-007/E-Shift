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
            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;
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
            cmbRole.Items.Add("admin");      
            cmbRole.Items.Add("driver");
            cmbRole.Items.Add("assistant");
            cmbRole.Items.Add("customer");
            cmbRole.SelectedIndex = 0;
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
                cmbRole.SelectedItem = role;

            bool isAvailable = Convert.ToBoolean(row["IsAvailable"]);
            cmbIsAvailable.Items.Clear();
            cmbIsAvailable.Items.Add("Yes");
            cmbIsAvailable.Items.Add("No");
            cmbIsAvailable.SelectedIndex = isAvailable ? 0 : 1;

            // Show/hide customer fields depending on role
            ToggleCustomerFields(role.Equals("customer", StringComparison.OrdinalIgnoreCase));

            // If customer, load customer details
            if (role.Equals("customer", StringComparison.OrdinalIgnoreCase))
            {
                LoadCustomerData();
            }
            else
            {
                ClearCustomerFields();
            }
        }
        private void LoadCustomerData()
        {
            string query = "SELECT FullName, Phone, Email, Address FROM Customer WHERE UserID = @UserID";
            var parameters = new Dictionary<string, object> { { "@UserID", userId } };
            var dt = Database.GetData(query, parameters);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtFullName.Text = row["FullName"].ToString();
                txtPhone.Text = row["Phone"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtAddress.Text = row["Address"].ToString();

                // Show customer fields if role is customer
                ToggleCustomerFields(true);
            }
            else
            {
                // No customer record found - hide fields
                ToggleCustomerFields(false);
            }
        }
        private void ClearCustomerFields()
        {
            txtFullName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }
        private void ToggleCustomerFields(bool visible)
        {
            lblFullName.Visible = txtFullName.Visible = visible;
            lblPhone.Visible = txtPhone.Visible = visible;
            lblEmail.Visible = txtEmail.Visible = visible;
            lblAddress.Visible = txtAddress.Visible = visible;
        }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem != null && cmbRole.SelectedItem.ToString().Equals("customer", StringComparison.OrdinalIgnoreCase))
                ToggleCustomerFields(true);
            else
                ToggleCustomerFields(false);
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

        private void EditUserForm_Load_1(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // existing validation for Username, Role, Availability...

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRole.SelectedItem.ToString().Equals("customer", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validate phone, email, address similarly
            }

            try
            {
                // Update User table
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
            { "@IsAvailable", cmbIsAvailable.Enabled ? (cmbIsAvailable.SelectedIndex == 0 ? 1 : 0) : GetCurrentAvailability() },
            { "@UserID", userId }
        };

                Database.ExecuteNonQuery(updateQuery, parameters);

                // Handle Customer table insert/update/delete depending on role

                if (cmbRole.SelectedItem.ToString().Equals("customer", StringComparison.OrdinalIgnoreCase))
                {
                    // Check if Customer record exists
                    string checkCustomerQuery = "SELECT COUNT(*) FROM Customer WHERE UserID = @UserID";
                    int count = Convert.ToInt32(Database.ExecuteSingleValue(checkCustomerQuery, new Dictionary<string, object> { { "@UserID", userId } }));

                    if (count > 0)
                    {
                        // Update existing customer record
                        string updateCustomerQuery = @"
                    UPDATE Customer
                    SET FullName = @FullName,
                        Phone = @Phone,
                        Email = @Email,
                        Address = @Address
                    WHERE UserID = @UserID";

                        var custParams = new Dictionary<string, object>
                {
                    { "@FullName", txtFullName.Text.Trim() },
                    { "@Phone", txtPhone.Text.Trim() },
                    { "@Email", txtEmail.Text.Trim() },
                    { "@Address", txtAddress.Text.Trim() },
                    { "@UserID", userId }
                };

                        Database.ExecuteNonQuery(updateCustomerQuery, custParams);
                    }
                    else
                    {
                        // Insert new customer record
                        string insertCustomerQuery = @"
                    INSERT INTO Customer (FullName, Phone, Email, Address, UserID)
                    VALUES (@FullName, @Phone, @Email, @Address, @UserID)";

                        var custParams = new Dictionary<string, object>
                {
                    { "@FullName", txtFullName.Text.Trim() },
                    { "@Phone", txtPhone.Text.Trim() },
                    { "@Email", txtEmail.Text.Trim() },
                    { "@Address", txtAddress.Text.Trim() },
                    { "@UserID", userId }
                };

                        Database.ExecuteNonQuery(insertCustomerQuery, custParams);
                    }
                }
                else
                {
                    // If role is NOT customer, optionally delete customer record
                    string deleteCustomerQuery = "DELETE FROM Customer WHERE UserID = @UserID";
                    Database.ExecuteNonQuery(deleteCustomerQuery, new Dictionary<string, object> { { "@UserID", userId } });
                }

                MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
