using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class EditVechicleForm : Form
    {
        private readonly int lorryId;

        public EditVechicleForm(int lorryId)
        {
            InitializeComponent();
            this.lorryId = lorryId;
        }

        /* ------------------------------------------------------ */
        /*  LOAD                                                  */
        /* ------------------------------------------------------ */
        private void EditVechicleForm_Load(object sender, EventArgs e)
        {
            LoadAvailabilityCombo();
            LoadVehicleDetails();
        }

        private void LoadAvailabilityCombo()
        {
            cmbAvailable.Items.Clear();
            cmbAvailable.Items.Add("Yes");
            cmbAvailable.Items.Add("No");
            cmbAvailable.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadVehicleDetails()
        {
            string q = "SELECT * FROM Lorry WHERE LorryID = @LorryID";
            var parms = new Dictionary<string, object> { { "@LorryID", lorryId } };
            DataTable dt = Database.GetData(q, parms);

            if (dt.Rows.Count != 1)
            {
                MessageBox.Show("Lorry not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            DataRow row = dt.Rows[0];
            txtRegistrationNumber.Text = row["RegistrationNumber"].ToString();
            txtModel.Text = row["Model"].ToString();

            bool currentAvail = Convert.ToBoolean(row["IsAvailable"]);
            cmbAvailable.SelectedIndex = currentAvail ? 0 : 1;   // 0 = Yes, 1 = No

            /* --- check assignment (Job ↔ TransportUnit ↔ Lorry) --- */
            string check = @"
                SELECT COUNT(*) 
                FROM Job J
                INNER JOIN TransportUnit T ON J.TransportUnitID = T.TransportUnitID
                WHERE T.LorryID = @LorryID";

            int jobs = Convert.ToInt32(
                Database.GetData(check, parms).Rows[0][0]);

            bool assigned = jobs > 0;
            lblAssignmentWarning.Visible = assigned;
            cmbAvailable.Enabled = !assigned;   // lock if assigned
        }

        /* ------------------------------------------------------ */
        /*  SAVE                                                  */
        /* ------------------------------------------------------ */
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string regNo = txtRegistrationNumber.Text.Trim();
            string model = txtModel.Text.Trim();

            /* decide final availability */
            bool newIsAvailable;
            if (cmbAvailable.Enabled)
            {
                newIsAvailable = cmbAvailable.SelectedItem.ToString() == "Yes";
            }
            else
            {
                // keep DB value when combo is disabled
                string q = "SELECT IsAvailable FROM Lorry WHERE LorryID = @LorryID";
                newIsAvailable = Convert.ToBoolean(
                    Database.GetData(q, new Dictionary<string, object> { { "@LorryID", lorryId } })
                            .Rows[0]["IsAvailable"]);
            }

            string update = @"
                UPDATE Lorry
                SET RegistrationNumber = @Reg,
                    Model              = @Model,
                    IsAvailable        = @Avail
                WHERE LorryID         = @LorryID";

            var parms = new Dictionary<string, object>
            {
                { "@Reg",    regNo },
                { "@Model",  model },
                { "@Avail",  newIsAvailable ? 1 : 0 },
                { "@LorryID", lorryId }
            };

            try
            {
                Database.ExecuteNonQuery(update, parms);
                MessageBox.Show("Vehicle updated successfully.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating vehicle: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* ------------------------------------------------------ */
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtRegistrationNumber.Text))
            {
                MessageBox.Show("Please enter the registration number.");
                txtRegistrationNumber.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter the model.");
                txtModel.Focus();
                return false;
            }
            if (cmbAvailable.SelectedIndex < 0)
            {
                MessageBox.Show("Please select availability.");
                cmbAvailable.Focus();
                return false;
            }
            return true;
        }

        private void btnCance2_Click(object sender, EventArgs e) => Close();

    }
}
