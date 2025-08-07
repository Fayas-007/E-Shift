using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms
{
    public partial class AddContainerForm : Form
    {
        public AddContainerForm()
        {
            InitializeComponent();
        }

        private void AddContainerForm_Load(object sender, EventArgs e)
        {
            LoadIsAvailableCombo();
        }

        private void LoadIsAvailableCombo()
        {
            cmbIsAvailable.Items.Clear();
            cmbIsAvailable.Items.Add("Yes");
            cmbIsAvailable.Items.Add("No");
            cmbIsAvailable.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIsAvailable.SelectedIndex = 0; // default to "Yes"
        }

        private void BtnAddContainer_Click(object sender, EventArgs e)
        {
            string containerNumber = txtContainerNumber.Text.Trim();
            string description = txtDescription.Text.Trim();
            string capacityText = txtCapacity.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(containerNumber))
            {
                MessageBox.Show("Please enter a container number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContainerNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please enter a description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return;
            }

            if (!int.TryParse(capacityText, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter a valid positive capacity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapacity.Focus();
                return;
            }

            bool isAvailable = cmbIsAvailable.SelectedItem.ToString() == "Yes";

            try
            {
                // Check for duplicate container number
                string checkQuery = "SELECT COUNT(*) FROM Container WHERE ContainerNumber = @ContainerNumber";
                var checkParams = new Dictionary<string, object>
                {
                    { "@ContainerNumber", containerNumber }
                };

                object countObj = Database.ExecuteSingleValue(checkQuery, checkParams);
                int existingCount = 0;
                if (countObj != null && int.TryParse(countObj.ToString(), out int cnt))
                    existingCount = cnt;

                if (existingCount > 0)
                {
                    MessageBox.Show("A container with this number already exists. Please use a different container number.", "Duplicate Container Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContainerNumber.Focus();
                    return;
                }

                // Insert container
                string insertQuery = @"
                    INSERT INTO Container (ContainerNumber, Description, Capacity, IsAvailable, CreatedAt)
                    VALUES (@ContainerNumber, @Description, @Capacity, @IsAvailable, @CreatedAt)";

                var parameters = new Dictionary<string, object>
                {
                    { "@ContainerNumber", containerNumber },
                    { "@Description", description },
                    { "@Capacity", capacity },
                    { "@IsAvailable", isAvailable ? 1 : 0 },
                    { "@CreatedAt", DateTime.Now }
                };

                Database.ExecuteNonQuery(insertQuery, parameters);
                MessageBox.Show("Container added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding container: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
