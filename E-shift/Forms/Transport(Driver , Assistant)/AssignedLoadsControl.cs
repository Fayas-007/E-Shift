using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using E_shift.Helpers;

namespace E_shift.Forms
{
    public partial class AssignedLoadsControl : UserControl
    {
        private readonly int _transportUnitId;
        private DataTable _loadsData;

        public AssignedLoadsControl(int transportUnitId)
        {
            InitializeComponent();
            _transportUnitId = transportUnitId;

            dgvLoads.SelectionChanged += DgvLoads_SelectionChanged;
            DataGridViewHelper.ApplyTheme(dgvLoads);
            LoadAssignedLoads();

            
        }

        private void LoadAssignedLoads()
        {
            string query = @"
            SELECT
                l.LoadID,
                l.JobID,
                l.Weight,
                l.CreatedAt,
                l.StartDate,
                l.EndDate,
                l.Status
            FROM Load l
            WHERE l.TransportUnitID = @TransportUnitID
            ORDER BY l.CreatedAt DESC";

            var parameters = new Dictionary<string, object>
            {
                { "@TransportUnitID", _transportUnitId }
            };

            try
            {
                _loadsData = Database.GetData(query, parameters);
                dgvLoads.DataSource = _loadsData;

                if (dgvLoads.Columns.Contains("LoadID"))
                    dgvLoads.Columns["LoadID"].Visible = false;

                dgvLoads.Columns["JobID"].HeaderText = "Job ID";
                dgvLoads.Columns["Weight"].HeaderText = "Weight (kg)";
                dgvLoads.Columns["StartDate"].HeaderText = "Start Date";
                dgvLoads.Columns["EndDate"].HeaderText = "End Date";
                dgvLoads.Columns["Status"].HeaderText = "Load Status";

                dgvLoads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLoads.ReadOnly = true;

                UpdateSummaryLabel();
                ClearStatusControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assigned loads: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearStatusControls()
        {
            
        }

        private void DgvLoads_SelectionChanged(object sender, EventArgs e)
        {
            // Since no status controls, just clear or do nothing here
            ClearStatusControls();
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

        private void dgvLoads_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
