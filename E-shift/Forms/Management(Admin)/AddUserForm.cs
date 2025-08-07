using E_shift.DataAccess;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            LoadRolesFromUsersTable();

            cmbIsAvailable.Items.AddRange(new[] { "Yes", "No" });
            cmbIsAvailable.SelectedIndex = 0;

            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;

            // Hide customer info inputs initially
            ToggleCustomerFields(false);
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem != null && cmbRole.SelectedItem.ToString().Equals("customer", StringComparison.OrdinalIgnoreCase))
                ToggleCustomerFields(true);
            else
                ToggleCustomerFields(false);
        }

        private void ToggleCustomerFields(bool show)
        {
            lblFullName.Visible = txtFullName.Visible = show;
            lblPhone.Visible = txtPhone.Visible = show;
            lblEmail.Visible = txtEmail.Visible = show;
            lblAddress.Visible = txtAddress.Visible = show;
        }

        // This method is modified to load roles from a hardcoded list instead of DB
        private void LoadRolesFromUsersTable()
        {
            try
            {
                cmbRole.Items.Clear();

                // Hardcoded roles list — change this to whatever roles you want always available
                var roles = new List<string> { "admin", "customer", "driver", "assistant" };

                foreach (var role in roles)
                {
                    cmbRole.Items.Add(role);
                }

                cmbRole.SelectedIndex = -1; // No default selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            if (!IsUsernameUnique(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Username already exists. Please choose another.", "Duplicate Username",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var userParameters = new Dictionary<string, object>
                {
                    { "@Username", txtUsername.Text.Trim() },
                    { "@Password", HashPassword(txtPassword.Text.Trim()) },
                    { "@Role", cmbRole.SelectedItem.ToString() },
                    { "@IsAvailable", cmbIsAvailable.SelectedItem.ToString() == "Yes" ? 1 : 0 }
                };

                string insertUserQuery = "INSERT INTO [User] (Username, Password, Role, IsAvailable) OUTPUT INSERTED.UserID VALUES (@Username, @Password, @Role, @IsAvailable)";

                // Insert user and get generated UserID
                var result = Database.ExecuteSingleValue(insertUserQuery, userParameters);
                int newUserId = Convert.ToInt32(result);

                // If role is customer, insert into Customer table
                if (cmbRole.SelectedItem.ToString().Equals("customer", StringComparison.OrdinalIgnoreCase))
                {
                    var customerParameters = new Dictionary<string, object>
                    {
                        { "@FullName", txtFullName.Text.Trim() },
                        { "@Phone", txtPhone.Text.Trim() },
                        { "@Email", txtEmail.Text.Trim() },
                        { "@Address", txtAddress.Text.Trim() },
                        { "@UserID", newUserId }
                    };

                    string insertCustomerQuery = @"INSERT INTO Customer (FullName, Phone, Email, Address, UserID) 
                                                   VALUES (@FullName, @Phone, @Email, @Address, @UserID)";

                    Database.ExecuteNonQuery(insertCustomerQuery, customerParameters);
                }

                MessageBox.Show("User added successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.");
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.");
                txtPassword.Focus();
                return false;
            }

            if (cmbRole.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a role.");
                cmbRole.Focus();
                return false;
            }

            if (cmbIsAvailable.SelectedIndex < 0)
            {
                MessageBox.Show("Please select availability.");
                cmbIsAvailable.Focus();
                return false;
            }

            if (cmbRole.SelectedItem.ToString().Equals("customer", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Please enter full name.");
                    txtFullName.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Please enter phone.");
                    txtPhone.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Please enter email.");
                    txtEmail.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Please enter address.");
                    txtAddress.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsUsernameUnique(string username)
        {
            string q = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
            var result = Database.ExecuteSingleValue(q, new Dictionary<string, object> { { "@Username", username } });
            return Convert.ToInt32(result) == 0;
        }

        private string HashPassword(string plain)
        {
            using (var sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
