using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class AddTransportUnit : Form
    {
        public AddTransportUnit()
        {
            InitializeComponent();
        }

        private void AddTransportUnitForm_Load(object sender, EventArgs e)
        {
            LoadLorries();
            LoadDrivers();
            LoadAssistants();
            LoadContainers();

            // Fill availability ComboBox with Yes/No options
            cmbIsAvailable.Items.Clear();
            cmbIsAvailable.Items.Add("Yes");
            cmbIsAvailable.Items.Add("No");
            cmbIsAvailable.SelectedIndex = 0; // default to Yes
        }

        private void LoadLorries()
        {
            try
            {
                string query = "SELECT LorryID, RegistrationNumber FROM Lorry WHERE IsAvailable = 1 ORDER BY RegistrationNumber";
                var dt = Database.GetData(query);
                cmbLorry.DisplayMember = "RegistrationNumber";
                cmbLorry.ValueMember = "LorryID";
                cmbLorry.DataSource = dt;
                cmbLorry.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lorries: " + ex.Message);
            }
        }

        private void LoadDrivers()
        {
            try
            {
                string query = "SELECT UserID, Username FROM [User] WHERE LOWER(Role) = 'driver' AND IsAvailable = 1 ORDER BY Username";
                var dt = Database.GetData(query);
                cmbDriver.DisplayMember = "Username";
                cmbDriver.ValueMember = "UserID";
                cmbDriver.DataSource = dt;
                cmbDriver.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading drivers: " + ex.Message);
            }
        }

        private void LoadAssistants()
        {
            try
            {
                string query = "SELECT UserID, Username FROM [User] WHERE LOWER(Role) = 'assistant' AND IsAvailable = 1 ORDER BY Username";
                var dt = Database.GetData(query);
                cmbAssistant.DisplayMember = "Username";
                cmbAssistant.ValueMember = "UserID";
                cmbAssistant.DataSource = dt;
                cmbAssistant.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assistants: " + ex.Message);
            }
        }

        private void LoadContainers()
        {
            try
            {
                string query = "SELECT ContainerID, ContainerNumber FROM Container WHERE IsAvailable = 1 ORDER BY ContainerNumber";
                var dt = Database.GetData(query);
                cmbContainer.DisplayMember = "ContainerNumber";
                cmbContainer.ValueMember = "ContainerID";
                cmbContainer.DataSource = dt;
                cmbContainer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading containers: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string insertQuery = @"
            INSERT INTO TransportUnit 
            (LorryID, DriverID, AssistantID, ContainerID, CreatedAt, UnitName, IsAvailable) 
            VALUES 
            (@LorryID, @DriverID, @AssistantID, @ContainerID, @CreatedAt, @UnitName, @IsAvailable)";

                int isAvailableValue = cmbIsAvailable.SelectedItem.ToString() == "Yes" ? 1 : 0;

                var parameters = new Dictionary<string, object>()
                {
                    {"@LorryID", cmbLorry.SelectedValue},
                    {"@DriverID", cmbDriver.SelectedValue},
                    {"@AssistantID", cmbAssistant.SelectedValue},
                    {"@ContainerID", cmbContainer.SelectedValue},
                    {"@CreatedAt", DateTime.Now},
                    {"@UnitName", txtUnitName.Text.Trim()},
                    {"@IsAvailable", isAvailableValue}
                };

                Database.ExecuteNonQuery(insertQuery, parameters);

                // Update availability statuses
                UpdateUserAvailability(Convert.ToInt32(cmbDriver.SelectedValue), false);
                UpdateUserAvailability(Convert.ToInt32(cmbAssistant.SelectedValue), false);
                UpdateLorryAvailability(Convert.ToInt32(cmbLorry.SelectedValue), false);
                UpdateContainerAvailability(Convert.ToInt32(cmbContainer.SelectedValue), false);

                MessageBox.Show("Transport Unit added successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding transport unit: " + ex.Message);
            }
        }

        private void UpdateUserAvailability(int userId, bool isAvailable)
        {
            string updateQuery = "UPDATE [User] SET IsAvailable = @IsAvailable WHERE UserID = @UserID AND Role IN ('Driver', 'Assistant')";
            var parameters = new Dictionary<string, object>()
            {
                {"@IsAvailable", isAvailable ? 1 : 0},
                {"@UserID", userId}
            };
            Database.ExecuteNonQuery(updateQuery, parameters);
        }

        private void UpdateLorryAvailability(int lorryId, bool isAvailable)
        {
            string query = "UPDATE Lorry SET IsAvailable = @IsAvailable WHERE LorryID = @LorryID";
            var parameters = new Dictionary<string, object>
            {
                { "@IsAvailable", isAvailable ? 1 : 0 },
                { "@LorryID", lorryId }
            };
            Database.ExecuteNonQuery(query, parameters);
        }

        private void UpdateContainerAvailability(int containerId, bool isAvailable)
        {
            string query = "UPDATE Container SET IsAvailable = @IsAvailable WHERE ContainerID = @ContainerID";
            var parameters = new Dictionary<string, object>
            {
                { "@IsAvailable", isAvailable ? 1 : 0 },
                { "@ContainerID", containerId }
            };
            Database.ExecuteNonQuery(query, parameters);
        }

        private bool ValidateInput()
        {
            if (cmbLorry.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a lorry.");
                cmbLorry.Focus();
                return false;
            }
            if (cmbDriver.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a driver.");
                cmbDriver.Focus();
                return false;
            }
            if (cmbAssistant.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an assistant.");
                cmbAssistant.Focus();
                return false;
            }
            if (cmbContainer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a container.");
                cmbContainer.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUnitName.Text))
            {
                MessageBox.Show("Please enter a unit name.");
                txtUnitName.Focus();
                return false;
            }
            if (cmbIsAvailable.SelectedIndex < 0)
            {
                MessageBox.Show("Please select availability (Yes or No).");
                cmbIsAvailable.Focus();
                return false;
            }
            return true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
