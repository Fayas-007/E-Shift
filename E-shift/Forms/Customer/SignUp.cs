using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms.Customer
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
            btnSignUp.Click += btnSignUp_Click_1;
        }


        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Phone number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            return true;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void lblFullName_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignUp_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Check if username already exists
                    string checkUserSql = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                    using (var checkCmd = new SqlCommand(checkUserSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert user (Role = customer, IsAvailable = 1)
                    string hashedPassword = HashPassword(password);

                    string insertUserSql = @"
                        INSERT INTO [User] (Username, Password, Role, IsAvailable)
                        OUTPUT INSERTED.UserID
                        VALUES (@Username, @Password, 'customer', 1)";

                    int newUserId;

                    using (var insertUserCmd = new SqlCommand(insertUserSql, conn))
                    {
                        insertUserCmd.Parameters.AddWithValue("@Username", username);
                        insertUserCmd.Parameters.AddWithValue("@Password", hashedPassword);
                        newUserId = (int)insertUserCmd.ExecuteScalar();
                    }

                    // Insert customer details
                    string insertCustomerSql = @"
                    INSERT INTO Customer (UserID, FullName, Phone, Email, Address)
                    VALUES (@UserID, @FullName, @Phone, @Email, @Address)";

                    using (var insertCustomerCmd = new SqlCommand(insertCustomerSql, conn))
                    {
                        insertCustomerCmd.Parameters.AddWithValue("@UserID", newUserId);
                        insertCustomerCmd.Parameters.AddWithValue("@FullName", fullName);
                        insertCustomerCmd.Parameters.AddWithValue("@Phone", phone);
                        insertCustomerCmd.Parameters.AddWithValue("@Email", email);
                        insertCustomerCmd.Parameters.AddWithValue("@Address", address);

                        insertCustomerCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Customer account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // or redirect to login
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during signup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
