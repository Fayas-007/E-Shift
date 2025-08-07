using E_shift.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class AddJobForm : Form
    {
        public AddJobForm()
        {
            InitializeComponent();
        }

        private void AddJobForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();

            cmbStatus.Items.AddRange(new string[] { "Pending", "In Progress", "Completed", "Cancelled" });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadCustomers()
        {
            try
            {
                string query = "SELECT CustomerID, FullName FROM Customer ORDER BY FullName";
                DataTable dt = Database.GetData(query);

                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "CustomerID";
                cmbCustomer.DataSource = dt;
                cmbCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string insertJobQuery = @"
                        INSERT INTO Job (CustomerID, Status, Notes)
                        VALUES (@CustomerID, @Status, @Notes)";

                    using (var cmd = new SqlCommand(insertJobQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", cmbCustomer.SelectedValue);
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Job added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding job: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCustomer.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Please select the status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return false;
            }

            // Notes optional validation - keep commented or remove if you want
            /*
            if (string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                MessageBox.Show("Please enter notes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNotes.Focus();
                return false;
            }
            */

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
