using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class EditContainerForm : Form
    {
        private readonly int containerId;

        public EditContainerForm(int containerId)
        {
            InitializeComponent();
            this.containerId = containerId;
        }

        /* -------------------------------------------------------- */
        /*  LOAD                                                    */
        /* -------------------------------------------------------- */
        private void EditContainerForm_Load(object sender, EventArgs e)
        {
            LoadIsAvailableCombo();
            LoadContainerDetails();
        }

        private void LoadIsAvailableCombo()
        {
            cmbIsAvailable.Items.Clear();
            cmbIsAvailable.Items.Add("Yes");
            cmbIsAvailable.Items.Add("No");
            cmbIsAvailable.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadContainerDetails()
        {
            string query = "SELECT * FROM Container WHERE ContainerID = @ContainerID";
            var parameters = new Dictionary<string, object> { { "@ContainerID", containerId } };
            DataTable dt = Database.GetData(query, parameters);

            if (dt.Rows.Count != 1)
            {
                MessageBox.Show("Container not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            DataRow row = dt.Rows[0];
            txtContainerNo.Text = row["ContainerNumber"].ToString();
            txtDescription.Text = row["Description"].ToString();
            txtCapacity.Text = row["Capacity"] == DBNull.Value ? "" : row["Capacity"].ToString();

            bool currentIsAvailable = Convert.ToBoolean(row["IsAvailable"]);
            cmbIsAvailable.SelectedIndex = currentIsAvailable ? 0 : 1;   // 0 = Yes, 1 = No

            /* ---- check if this container is assigned to ANY job ---- */
            string checkAssignment = @"
                SELECT COUNT(*) 
                FROM Load L
                WHERE L.TransportUnitID IN (
                    SELECT TransportUnitID
                    FROM TransportUnit
                    WHERE ContainerID = @ContainerID
                )";
            int assignedCount = Convert.ToInt32(
                Database.GetData(checkAssignment, parameters).Rows[0][0]);

            bool assigned = assignedCount > 0;

            lblAssignmentWarning.Visible = assigned;
            cmbIsAvailable.Enabled = !assigned;        // lock if assigned
        }

        /* -------------------------------------------------------- */
        /*  SAVE                                                    */
        /* -------------------------------------------------------- */
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            /* gather values */
            string containerNo = txtContainerNo.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (!int.TryParse(txtCapacity.Text.Trim(), out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter a valid positive capacity.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapacity.Focus();
                return;
            }

            /* resolve IsAvailable based on whether the combo is enabled */
            bool newIsAvailable;
            if (cmbIsAvailable.Enabled)
            {
                newIsAvailable = cmbIsAvailable.SelectedItem.ToString() == "Yes";
            }
            else
            {
                // keep the existing DB value
                string q = "SELECT IsAvailable FROM Container WHERE ContainerID = @ContainerID";
                newIsAvailable = Convert.ToBoolean(
                    Database.GetData(q, new Dictionary<string, object> { { "@ContainerID", containerId } })
                            .Rows[0]["IsAvailable"]);
            }

            try
            {
                string updateQuery = @"
                    UPDATE Container
                    SET ContainerNumber = @ContainerNumber,
                        Description     = @Description,
                        Capacity        = @Capacity,
                        IsAvailable     = @IsAvailable
                    WHERE ContainerID = @ContainerID";

                var parms = new Dictionary<string, object>
                {
                    { "@ContainerNumber", containerNo },
                    { "@Description",     description },
                    { "@Capacity",        capacity },
                    { "@IsAvailable",     newIsAvailable ? 1 : 0 },
                    { "@ContainerID",     containerId }
                };

                Database.ExecuteNonQuery(updateQuery, parms);

                MessageBox.Show("Container updated successfully.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating container: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtContainerNo.Text))
            {
                MessageBox.Show("Please enter the Container No.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContainerNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCapacity.Text))
            {
                MessageBox.Show("Please enter the capacity.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapacity.Focus();
                return false;
            }
            return true;
        }

        /* -------------------------------------------------------- */
        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}
