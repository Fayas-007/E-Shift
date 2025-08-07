using E_shift.Authentication_Service;
using E_shift.Forms.Customer;
using E_shift.Models;
using System;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.\nPlease fill in both fields to continue.",
                                "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Authenticate user
            User user = AuthenticationService.Login(username, password);

            if (user != null)
            {
                MessageBox.Show($"Welcome back, {user.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                switch (user.Role.ToLower())
                {
                    case "admin":
                        new AdminDashboard(user).Show();
                        break;
                    case "driver":
                    case "assistant":
                        new SharedDashboard(user).Show();
                        break;
                    case "customer":
                        new CustomerDashboard(user).Show();  
                        break;
                    default:
                        new AdminDashboard(user).Show();
                        break; 
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblSignUp_Click(object sender, EventArgs e)
        {
            SignUp signUpForm = new SignUp();
            signUpForm.ShowDialog();
        }

        private void lblSignUp_MouseEnter(object sender, EventArgs e)
        {
            lblSignUp.ForeColor = System.Drawing.Color.Red; // Optional: change color on hover
            lblSignUp.Font = new System.Drawing.Font(lblSignUp.Font, System.Drawing.FontStyle.Underline);
        }

        private void lblSignUp_MouseLeave(object sender, EventArgs e)
        {
            lblSignUp.ForeColor = System.Drawing.Color.Blue; // Reset color
            lblSignUp.Font = new System.Drawing.Font(lblSignUp.Font, System.Drawing.FontStyle.Regular);
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        // Optional: keep these empty event handlers if wired in designer
        private void Login_Load(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void lblUsername_Click(object sender, EventArgs e) { }
    }
}
