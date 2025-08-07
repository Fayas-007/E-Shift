using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms
{
    public partial class AddLoadForm : Form
    {
        public AddLoadForm()
        {
            InitializeComponent();
            cmbJobID.SelectedIndexChanged += CmbJobID_SelectedIndexChanged;
        }

        private void AddLoadForm_Load(object sender, EventArgs e)
        {
            LoadJobIDs();
        }

        private void LoadJobIDs()
        {
            string query = "SELECT JobID FROM Job ORDER BY JobID DESC";
            DataTable dt = Database.GetData(query);

            cmbJobID.DisplayMember = "JobID";
            cmbJobID.ValueMember = "JobID";
            cmbJobID.DataSource = dt;
            cmbJobID.SelectedIndex = -1; // No selection initially
            txtCustomerName.Text = "";
        }

        private void CmbJobID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbJobID.SelectedValue == null)
            {
                txtCustomerName.Text = "";
                return;
            }

            int selectedJobId = 0;

            // Defensive cast because SelectedValue may be DataRowView
            if (cmbJobID.SelectedValue is int val)
            {
                selectedJobId = val;
            }
            else if (cmbJobID.SelectedValue is DataRowView drv)
            {
                selectedJobId = Convert.ToInt32(drv["JobID"]);
            }
            else
            {
                txtCustomerName.Text = "";
                return;
            }

            string query = @"
                SELECT c.FullName 
                FROM Job j
                INNER JOIN Customer c ON j.CustomerID = c.CustomerID
                WHERE j.JobID = @JobID";

            var parameters = new Dictionary<string, object>
            {
                { "@JobID", selectedJobId }
            };

            try
            {
                DataTable dt = Database.GetData(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    txtCustomerName.Text = dt.Rows[0]["FullName"].ToString();
                }
                else
                {
                    txtCustomerName.Text = "(No Customer Found)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Text = "(Error)";
            }
        }

        private void BtnAddLoad_Click(object sender, EventArgs e)
        {
            if (cmbJobID.SelectedItem == null || cmbJobID.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Job ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtWeight.Text, out decimal weight) || weight <= 0)
            {
                MessageBox.Show("Please enter a valid positive weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int jobId = Convert.ToInt32(cmbJobID.SelectedValue);
            string description = txtDescription.Text.Trim();

            string insertQuery = @"
                INSERT INTO Load (JobID, Description, Weight, CreatedAt)
                VALUES (@JobID, @Description, @Weight, @CreatedAt)";

            var parameters = new Dictionary<string, object>
            {
                { "@JobID", jobId },
                { "@Description", description },
                { "@Weight", weight },
                { "@CreatedAt", DateTime.Now }
            };

            try
            {
                Database.ExecuteNonQuery(insertQuery, parameters);
                MessageBox.Show("Load added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding load: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
