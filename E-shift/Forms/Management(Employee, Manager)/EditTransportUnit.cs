using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms
{
    public partial class EditTransportUnit : Form
    {
        private int transportUnitId;
        private int oldDriverId;
        private int oldAssistantId;
        private int oldLorryId;
        private int oldContainerId;

        public EditTransportUnit(int id)
        {
            InitializeComponent();
            transportUnitId = id;
        }

        private void EditTransportUnit_Load(object sender, EventArgs e)
        {
            LoadTransportUnitData();
            CheckAssignmentAndSetAvailabilityControl();

            LoadLorries();
            LoadDriversAndAssistants();
            LoadContainers();

            SetComboSelectedValues();
        }

        private void LoadTransportUnitData()
        {
            string query = "SELECT * FROM TransportUnit WHERE TransportUnitID = @ID";
            var parameters = new Dictionary<string, object> { ["@ID"] = transportUnitId };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Transport Unit not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            var row = dt.Rows[0];
            oldDriverId = row["DriverID"] != DBNull.Value ? Convert.ToInt32(row["DriverID"]) : 0;
            oldAssistantId = row["AssistantID"] != DBNull.Value ? Convert.ToInt32(row["AssistantID"]) : 0;
            oldLorryId = row["LorryID"] != DBNull.Value ? Convert.ToInt32(row["LorryID"]) : 0;
            oldContainerId = row["ContainerID"] != DBNull.Value ? Convert.ToInt32(row["ContainerID"]) : 0;

            txtUnitName.Text = row["UnitName"].ToString();
            bool isAvailable = row["IsAvailable"] != DBNull.Value && Convert.ToBoolean(row["IsAvailable"]);
            cmbIsAvailable.SelectedIndex = isAvailable ? 0 : 1;
        }

        private void CheckAssignmentAndSetAvailabilityControl()
        {
            string checkAssignmentQuery = @"
                SELECT COUNT(*) FROM Load WHERE TransportUnitID = @TransportUnitID";

            var parameters = new Dictionary<string, object> { { "@TransportUnitID", transportUnitId } };

            int assignedCount = Convert.ToInt32(Database.GetData(checkAssignmentQuery, parameters).Rows[0][0]);
            bool isAssigned = assignedCount > 0;

            lblAssignmentWarning.Visible = isAssigned;
            cmbIsAvailable.Enabled = !isAssigned;
        }

        private void LoadLorries()
        {
            string query = @"
                SELECT LorryID, RegistrationNumber 
                FROM Lorry 
                WHERE IsAvailable = 1 OR LorryID = @CurrentLorryID
                ORDER BY RegistrationNumber";

            var parameters = new Dictionary<string, object>
            {
                { "@CurrentLorryID", oldLorryId }
            };

            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 0)
            {
                cmbLorry.DataSource = null;
                cmbLorry.Items.Clear();
                cmbLorry.Items.Add("No lorries available");
                cmbLorry.SelectedIndex = 0;
                cmbLorry.Enabled = false;
                return;
            }

            cmbLorry.Enabled = true;
            cmbLorry.DisplayMember = "RegistrationNumber";
            cmbLorry.ValueMember = "LorryID";
            cmbLorry.DataSource = dt;
        }

        private void LoadDriversAndAssistants()
        {
            // Drivers
            string queryDrivers = @"
                SELECT UserID, Username 
                FROM [User] 
                WHERE LOWER(Role) = 'driver' AND (IsAvailable = 1 OR UserID = @CurrentDriverID)
                ORDER BY Username";

            DataTable dtDrivers = Database.GetData(queryDrivers, new Dictionary<string, object>
            {
                { "@CurrentDriverID", oldDriverId }
            });

            if (dtDrivers.Rows.Count == 0)
            {
                cmbDriver.DataSource = null;
                cmbDriver.Items.Clear();
                cmbDriver.Items.Add("No drivers available");
                cmbDriver.SelectedIndex = 0;
                cmbDriver.Enabled = false;
            }
            else
            {
                cmbDriver.Enabled = true;
                cmbDriver.DisplayMember = "Username";
                cmbDriver.ValueMember = "UserID";
                cmbDriver.DataSource = dtDrivers;
            }

            // Assistants
            string queryAssistants = @"
                SELECT UserID, Username 
                FROM [User] 
                WHERE LOWER(Role) = 'assistant' AND (IsAvailable = 1 OR UserID = @CurrentAssistantID)
                ORDER BY Username";

            DataTable dtAssistants = Database.GetData(queryAssistants, new Dictionary<string, object>
            {
                { "@CurrentAssistantID", oldAssistantId }
            });

            if (dtAssistants.Rows.Count == 0)
            {
                cmbAssistant.DataSource = null;
                cmbAssistant.Items.Clear();
                cmbAssistant.Items.Add("No assistants available");
                cmbAssistant.SelectedIndex = 0;
                cmbAssistant.Enabled = false;
            }
            else
            {
                cmbAssistant.Enabled = true;
                cmbAssistant.DisplayMember = "Username";
                cmbAssistant.ValueMember = "UserID";
                cmbAssistant.DataSource = dtAssistants;
            }
        }

        private void LoadContainers()
        {
            string query = @"
                SELECT ContainerID, ContainerNumber 
                FROM Container 
                WHERE IsAvailable = 1 OR ContainerID = @CurrentContainerID
                ORDER BY ContainerNumber";

            var parameters = new Dictionary<string, object>
            {
                { "@CurrentContainerID", oldContainerId }
            };

            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count == 0)
            {
                cmbContainer.DataSource = null;
                cmbContainer.Items.Clear();
                cmbContainer.Items.Add("No containers available");
                cmbContainer.SelectedIndex = 0;
                cmbContainer.Enabled = false;
                return;
            }

            cmbContainer.Enabled = true;
            cmbContainer.DisplayMember = "ContainerNumber";
            cmbContainer.ValueMember = "ContainerID";
            cmbContainer.DataSource = dt;
        }

        private void SetComboSelectedValues()
        {
            if (cmbLorry.Enabled && cmbLorry.DataSource != null)
                cmbLorry.SelectedValue = oldLorryId;

            if (cmbDriver.Enabled && cmbDriver.DataSource != null)
                cmbDriver.SelectedValue = oldDriverId;

            if (cmbAssistant.Enabled && cmbAssistant.DataSource != null)
                cmbAssistant.SelectedValue = oldAssistantId;

            if (cmbContainer.Enabled && cmbContainer.DataSource != null)
                cmbContainer.SelectedValue = oldContainerId;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if ((cmbLorry.Enabled && cmbLorry.SelectedIndex < 0) ||
                (cmbDriver.Enabled && cmbDriver.SelectedIndex < 0) ||
                (cmbAssistant.Enabled && cmbAssistant.SelectedIndex < 0) ||
                (cmbContainer.Enabled && cmbContainer.SelectedIndex < 0) ||
                string.IsNullOrWhiteSpace(txtUnitName.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int newDriverId = cmbDriver.Enabled ? Convert.ToInt32(cmbDriver.SelectedValue) : oldDriverId;
                int newAssistantId = cmbAssistant.Enabled ? Convert.ToInt32(cmbAssistant.SelectedValue) : oldAssistantId;
                int newLorryId = cmbLorry.Enabled ? Convert.ToInt32(cmbLorry.SelectedValue) : oldLorryId;
                int newContainerId = cmbContainer.Enabled ? Convert.ToInt32(cmbContainer.SelectedValue) : oldContainerId;

                bool newIsAvailable = cmbIsAvailable.Enabled ? cmbIsAvailable.SelectedIndex == 0 : (oldDriverId == newDriverId && oldAssistantId == newAssistantId && oldLorryId == newLorryId && oldContainerId == newContainerId);

                string updateQuery = @"
                    UPDATE TransportUnit
                    SET LorryID = @LorryID,
                        DriverID = @DriverID,
                        AssistantID = @AssistantID,
                        ContainerID = @ContainerID,
                        UnitName = @UnitName,
                        IsAvailable = @IsAvailable
                    WHERE TransportUnitID = @TransportUnitID";

                var parameters = new Dictionary<string, object>
                {
                    ["@LorryID"] = newLorryId,
                    ["@DriverID"] = newDriverId,
                    ["@AssistantID"] = newAssistantId,
                    ["@ContainerID"] = newContainerId,
                    ["@UnitName"] = txtUnitName.Text.Trim(),
                    ["@IsAvailable"] = newIsAvailable ? 1 : 0,
                    ["@TransportUnitID"] = transportUnitId
                };

                Database.ExecuteNonQuery(updateQuery, parameters);

                if (oldDriverId != newDriverId)
                {
                    SetUserAvailability(oldDriverId, true);
                    SetUserAvailability(newDriverId, false);
                }

                if (oldAssistantId != newAssistantId)
                {
                    SetUserAvailability(oldAssistantId, true);
                    SetUserAvailability(newAssistantId, false);
                }

                if (oldLorryId != newLorryId)
                {
                    SetLorryAvailability(oldLorryId, true);
                    SetLorryAvailability(newLorryId, false);
                }

                if (oldContainerId != newContainerId)
                {
                    SetContainerAvailability(oldContainerId, true);
                    SetContainerAvailability(newContainerId, false);
                }

                MessageBox.Show("Transport Unit updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Transport Unit: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetUserAvailability(int userId, bool isAvailable)
        {
            if (userId == 0) return;

            string query = "UPDATE [User] SET IsAvailable = @Available WHERE UserID = @ID";
            Database.ExecuteNonQuery(query, new Dictionary<string, object> {
                { "@Available", isAvailable ? 1 : 0 },
                { "@ID", userId }
            });
        }

        private void SetLorryAvailability(int lorryId, bool isAvailable)
        {
            if (lorryId == 0) return;

            string query = "UPDATE Lorry SET IsAvailable = @Available WHERE LorryID = @ID";
            Database.ExecuteNonQuery(query, new Dictionary<string, object> {
                { "@Available", isAvailable ? 1 : 0 },
                { "@ID", lorryId }
            });
        }

        private void SetContainerAvailability(int containerId, bool isAvailable)
        {
            if (containerId == 0) return;

            string query = "UPDATE Container SET IsAvailable = @Available WHERE ContainerID = @ID";
            Database.ExecuteNonQuery(query, new Dictionary<string, object> {
                { "@Available", isAvailable ? 1 : 0 },
                { "@ID", containerId }
            });
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblAssignmentWarning_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click_1(object sender, EventArgs e)
        {

        }
    }
}
