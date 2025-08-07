using System;
using System.Collections.Generic;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms.Customer
{
    public partial class AddJobForm : Form
    {
        private readonly int _customerId;

        public AddJobForm(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            txtCustomerName.Text = GetCustomerName(_customerId);

            txtStatus.Text = "Submitted";
            txtStatus.ReadOnly = true;

            btnCancel.Click += (s, e) => this.Close();
        }

        private string GetCustomerName(int customerId)
        {
            string sql = "SELECT FullName FROM Customer WHERE CustomerID = @cid";
            var parameters = new Dictionary<string, object> { { "@cid", customerId } };

            var result = Database.ExecuteSingleValue(sql, parameters);
            return result?.ToString() ?? "Unknown Customer";
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string notes = txtNotes.Text.Trim();
            string startLocation = txtStartLocation.Text.Trim();
            string endLocation = txtEndLocation.Text.Trim();

            if (string.IsNullOrEmpty(notes))
            {
                MessageBox.Show("Please enter notes for the job.");
                return;
            }

            if (string.IsNullOrEmpty(startLocation))
            {
                MessageBox.Show("Please enter the start location.");
                return;
            }

            if (string.IsNullOrEmpty(endLocation))
            {
                MessageBox.Show("Please enter the end location.");
                return;
            }

            try
            {
                string sql = @"
                    INSERT INTO Job (CustomerID, Status, Notes, StartLocation, EndLocation) 
                    VALUES (@cid, @status, @notes, @startLoc, @endLoc);
                    SELECT CAST(SCOPE_IDENTITY() AS int);
                ";

                var parameters = new Dictionary<string, object>
                {
                    {"@cid", _customerId},
                    {"@status", "Submitted"},
                    {"@notes", notes},
                    {"@startLoc", startLocation},
                    {"@endLoc", endLocation}
                };

                object result = Database.ExecuteSingleValue(sql, parameters);
                int newJobId = Convert.ToInt32(result);

                MessageBox.Show("Job added successfully!");

                using (var addLoadForm = new AddLoadForm(newJobId))
                {
                    addLoadForm.ShowDialog();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while adding job: {ex.Message}");
            }
        }

        private void AddJobForm_Load(object sender, EventArgs e)
        {

        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblStartLocation_Click(object sender, EventArgs e)
        {

        }
    }
}
