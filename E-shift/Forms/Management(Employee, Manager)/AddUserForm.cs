using E_shift.DataAccess;
using System;
using System.Collections.Generic;
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
            cmbRole.Items.AddRange(new[] { "manager", "employee", "driver", "assistant" });
            cmbRole.SelectedIndex = -1;

            cmbIsAvailable.Items.AddRange(new[] { "Yes", "No" });
            cmbIsAvailable.SelectedIndex = 0;
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
                var parameters = new Dictionary<string, object>
                {
                    { "@Username", txtUsername.Text.Trim() },
                    { "@Password", HashPassword(txtPassword.Text.Trim()) },
                    { "@Role", cmbRole.SelectedItem.ToString() },
                    { "@IsAvailable", cmbIsAvailable.SelectedItem.ToString() == "Yes" ? 1 : 0 }
                };

                string query = "INSERT INTO [User] (Username, Password, Role, IsAvailable) VALUES (@Username, @Password, @Role, @IsAvailable)";
                Database.ExecuteNonQuery(query, parameters);

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
