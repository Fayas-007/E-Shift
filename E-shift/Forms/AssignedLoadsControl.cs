using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;  // for Color
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class AssignedLoadsControl : UserControl
    {
        private readonly int _transportUnitId;
        private DataTable _loadsData;  // store loaded data here

        public AssignedLoadsControl(int transportUnitId)
        {
            InitializeComponent();
            _transportUnitId = transportUnitId;
            LoadAssignedLoads();
        }

        private void LoadAssignedLoads()
        {
            string query = @"
                SELECT
                    l.LoadID,
                    j.JobID,
                    l.Description,
                    l.Weight,
                    l.CreatedAt,
                    j.StartLocation,
                    j.Destination,
                    j.StartDate,
                    j.EndDate,
                    j.Status
                FROM Load l
                INNER JOIN Job j ON l.JobID = j.JobID
                WHERE j.TransportUnitID = @TransportUnitID
                ORDER BY l.CreatedAt DESC";

            var parameters = new Dictionary<string, object>
            {
                { "@TransportUnitID", _transportUnitId }
            };

            try
            {
                _loadsData = Database.GetData(query, parameters);
                dgvLoads.DataSource = _loadsData;

                // Configure columns
                dgvLoads.Columns["JobID"].Visible = true;
                dgvLoads.Columns["JobID"].HeaderText = "Job ID";
                dgvLoads.Columns["Description"].HeaderText = "Description";
                dgvLoads.Columns["Weight"].HeaderText = "Weight (kg)";
                dgvLoads.Columns["StartLocation"].HeaderText = "From";
                dgvLoads.Columns["Destination"].HeaderText = "To";
                dgvLoads.Columns["StartDate"].HeaderText = "Start Date";
                dgvLoads.Columns["EndDate"].HeaderText = "End Date";

                dgvLoads.Columns["JobID"].Width = 70;
                dgvLoads.Columns["Description"].Width = 160;
                dgvLoads.Columns["Weight"].Width = 120;
                dgvLoads.Columns["StartLocation"].Width = 130;
                dgvLoads.Columns["Destination"].Width = 130;
                dgvLoads.Columns["StartDate"].Width = 130;
                dgvLoads.Columns["EndDate"].Width = 130;

                dgvLoads.Columns["LoadID"].Visible = false;
                dgvLoads.Columns["CreatedAt"].Visible = false;
                dgvLoads.Columns["Status"].Visible = false;

                dgvLoads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLoads.ReadOnly = true;

  
                UpdateSummaryLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assigned loads: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummaryLabel()
        {
            if (_loadsData == null) return;

            int totalLoads = _loadsData.Rows.Count;
            double totalWeight = 0;

            foreach (DataRow row in _loadsData.Rows)
            {
                if (row["Weight"] != DBNull.Value)
                    totalWeight += Convert.ToDouble(row["Weight"]);
            }

            lblSummary.Text = $"Total Loads: {totalLoads}   |   Total Weight: {totalWeight} kg";
        }

        private void AssignedLoadsControl_Load(object sender, EventArgs e)
        {
  
        }
    }
}
