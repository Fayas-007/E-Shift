using E_shift.DataAccess;
using System;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                string query = "INSERT INTO Customer (FullName, Phone, Email, Address) VALUES (@FullName, @Phone, @Email, @Address)";
                var parameters = new System.Collections.Generic.Dictionary<string, object>()
                {
                    {"@FullName", txtFullName.Text.Trim()},
                    {"@Phone", txtPhone.Text.Trim()},
                    {"@Email", txtEmail.Text.Trim()},
                    {"@Address", txtAddress.Text.Trim()}
                };

                Database.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter the full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }
            // Add any extra validation here if needed

            return true;
        }

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
