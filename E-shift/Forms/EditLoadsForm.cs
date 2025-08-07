using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class EditLoadForm : Form
    {
        private readonly int loadId;

        public EditLoadForm(int loadId)
        {
            InitializeComponent();
            this.loadId = loadId;
        }

        private void EditLoadForm_Load(object sender, EventArgs e)
        {
            LoadJobs();
            LoadLoadDetails();
        }

        private void LoadJobs()
        {
            string query = @"SELECT JobID, CAST(JobID AS VARCHAR) AS DisplayText FROM Job ORDER BY JobID DESC";
            cmbJob.DataSource = Database.GetData(query);
            cmbJob.DisplayMember = "DisplayText";  // This avoids duplicate column name
            cmbJob.ValueMember = "JobID";
        }

        private void LoadLoadDetails()
        {
            string query = @"
                SELECT 
                    Load.LoadID,
                    Load.JobID,
                    Load.Description,
                    Load.Weight
                FROM Load
                WHERE Load.LoadID = @LoadID";

            var parameters = new Dictionary<string, object> { { "@LoadID", loadId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                cmbJob.SelectedValue = row["JobID"];
                txtDescription.Text = row["Description"].ToString();
                txtWeight.Text = row["Weight"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string query = @"UPDATE Load SET 
                JobID = @JobID,
                Description = @Description,
                Weight = @Weight
                WHERE LoadID = @LoadID";

            var parameters = new Dictionary<string, object>()
            {
                {"@JobID", cmbJob.SelectedValue},
                {"@Description", txtDescription.Text.Trim()},
                {"@Weight", txtWeight.Text.Trim()},
                {"@LoadID", loadId}
            };

            try
            {
                Database.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Load updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating load: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbJob.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Job.");
                cmbJob.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description.");
                txtDescription.Focus();
                return false;
            }
            if (!decimal.TryParse(txtWeight.Text.Trim(), out _))
            {
                MessageBox.Show("Please enter a valid weight.");
                txtWeight.Focus();
                return false;
            }
            return true;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
