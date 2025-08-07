using E_shift.DataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class EditCustomer : Form
    {
        private readonly  int customerId;

        public EditCustomer(int id)
        {
            InitializeComponent();
            customerId = id;
            LoadCustomerDetails();
        }

        private void LoadCustomerDetails()
        {
            string query = $"SELECT * FROM Customer WHERE CustomerID = {customerId}";
            DataTable dt = Database.GetData(query);
            if (dt.Rows.Count > 0)
            {
                txtFullName.Text = dt.Rows[0]["FullName"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtFullName.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string address = txtAddress.Text;

            string updateQuery = $"UPDATE Customer SET FullName = '{name}', Phone = '{phone}', Email = '{email}', Address = '{address}' WHERE CustomerID = {customerId}";

            try
            {
                Database.ExecuteNonQuery(updateQuery);
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void EditCustomer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
