using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using E_shift.Helpers;
namespace E_shift.Forms
{
    public partial class MyJobsControl : UserControl
    {
        private readonly int _transportUnitId;

        

        public MyJobsControl(int transportUnitId)
        {
            InitializeComponent();
            _transportUnitId = transportUnitId;
            DataGridViewHelper.ApplyTheme(dgvJobs);
            LoadJobs();
        }

        /* ---------- data loading ---------- */

        private void LoadJobs()
        {
            const string sql = @"
                SELECT  j.JobID,
                        c.FullName AS CustomerName,
                        j.StartLocation,
                        j.Destination,
                        j.StartDate,
                        j.EndDate,
                        j.Status
                FROM    Job j
                        INNER JOIN Customer c ON j.CustomerID = c.CustomerID
                WHERE   j.TransportUnitID = @UnitId
                ORDER BY j.StartDate DESC";

            var dt = Database.GetData(sql, new Dictionary<string, object> { { "@UnitId", _transportUnitId } });
            dgvJobs.DataSource = dt;

            dgvJobs.Columns["JobID"].HeaderText = "Job ID";
            dgvJobs.Columns["CustomerName"].HeaderText = "Customer";
            dgvJobs.Columns["StartLocation"].HeaderText = "From";
            dgvJobs.Columns["Destination"].HeaderText = "To";
            dgvJobs.Columns["StartDate"].HeaderText = "Start Date";
            dgvJobs.Columns["EndDate"].HeaderText = "End Date";
            dgvJobs.Columns["Status"].HeaderText = "Job Status";

            dgvJobs.ReadOnly = true;
            dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJobs.ClearSelection();
        }

    
        private void MyJobsControl_Load(object sender, EventArgs e)
        {
            
        }

        private void lblTransportUnitInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
