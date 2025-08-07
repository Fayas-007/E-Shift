using System;
using System.Collections.Generic;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms
{
    public partial class AddVehicleForm : Form
    {
        public AddVehicleForm()
        {
            InitializeComponent();
        }

        private void AddVehicleForm_Load(object sender, EventArgs e)
        {
            cmbAvailability.Items.AddRange(new string[] { "Yes", "No" });
            cmbAvailability.SelectedIndex = 0; // Default to "Yes"
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string regNo = txtRegNo.Text.Trim();
            string model = txtModel.Text.Trim();
            bool isAvailable = cmbAvailability.SelectedItem.ToString() == "Yes";

         

            var parameters = new Dictionary<string, object>
            {
                { "@RegistrationNumber", regNo },
                { "@Model", string.IsNullOrWhiteSpace(model) ? DBNull.Value : (object)model },
                { "@IsAvailable", isAvailable ? 1 : 0 },
                { "@CreatedAt", DateTime.Now }
            };

            string insertQuery = @"
                INSERT INTO Lorry (RegistrationNumber, Model, IsAvailable, CreatedAt)
                VALUES (@RegistrationNumber, @Model, @IsAvailable, @CreatedAt)";

            try
            {
                Database.ExecuteNonQuery(insertQuery, parameters);
                MessageBox.Show("Vehicle added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding vehicle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
