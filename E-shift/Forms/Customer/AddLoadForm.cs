using System;
using System.Collections.Generic;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms.Customer
{
    public partial class AddLoadForm : Form
    {
        private readonly int _jobId;

        public AddLoadForm(int jobId)
        {
            InitializeComponent();
            _jobId = jobId;

            btnCancel.Click += (s, e) => this.Close();

            btnSave.Click += BtnSave_Click;
            btnAddAnother.Click += BtnAddAnother_Click;
        }

        private void AddLoadForm_Load(object sender, EventArgs e)
        {
            dtpStartDate.MinDate = DateTime.Today;
            dtpEndDate.MinDate = DateTime.Today;
        }

        private bool SaveLoad()
        {
            if (!decimal.TryParse(txtWeight.Text.Trim(), out decimal weight) || weight <= 0)
            {
                MessageBox.Show("Please enter a valid positive weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                MessageBox.Show("End date cannot be before start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                string sql = @"
                    INSERT INTO Load 
                    (JobID, Weight, Status, StartDate, EndDate, CreatedAt)
                    VALUES 
                    (@jobId, @weight, @status, @startDate, @endDate, @createdAt)";

                var parameters = new Dictionary<string, object>
                {
                    {"@jobId", _jobId},
                    {"@weight", weight},
                    {"@status", "Submitted"},
                    {"@startDate", dtpStartDate.Value.Date},
                    {"@endDate", dtpEndDate.Value.Date},
                    {"@createdAt", DateTime.Now}
                };

                Database.ExecuteNonQuery(sql, parameters);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding load: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveLoad())
            {
                MessageBox.Show("Load added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnAddAnother_Click(object sender, EventArgs e)
        {
            if (SaveLoad())
            {
                MessageBox.Show("Load added successfully! You can add another load.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtWeight.Clear();
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            txtWeight.Focus();
        }
    }
}
