using E_shift.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class MyJobsControl : UserControl
    {
        private readonly int _transportUnitId;
        private int? _selectedJobId;

        // -------- status transition map --------
        private readonly Dictionary<string, string[]> _allowed = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            { "Pending",     new[] { "In Progress", "Cancelled" } },
            { "In Progress", new[] { "Completed", "Cancelled" } },
            { "Completed",   Array.Empty<string>() },
            { "Cancelled",   Array.Empty<string>() }
        };

        public MyJobsControl(int transportUnitId)
        {
            InitializeComponent();
            _transportUnitId = transportUnitId;

            LoadJobs();
            InitEmptyStatusDropdown();

            dgvJobs.SelectionChanged += DgvJobs_SelectionChanged;
            btnUpdateStatus.Click += BtnUpdateStatus_Click;
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

        /* ---------- dropdown helpers ---------- */

        private void InitEmptyStatusDropdown()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Enabled = false;
            btnUpdateStatus.Enabled = false;
        }

        private void PopulateStatusDropdown(string currentStatus)
        {
            cmbStatus.Items.Clear();

            if (_allowed.TryGetValue(currentStatus, out var nextStates) && nextStates.Length > 0)
            {
                cmbStatus.Items.AddRange(nextStates);
                cmbStatus.SelectedIndex = 0;
                cmbStatus.Enabled = true;
                btnUpdateStatus.Enabled = true;
            }
            else
            {
                // terminal state – nothing selectable
                cmbStatus.Enabled = false;
                btnUpdateStatus.Enabled = false;
            }
        }

        /* ---------- grid selection ---------- */

        private void DgvJobs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvJobs.SelectedRows.Count == 0)
            {
                _selectedJobId = null;
                InitEmptyStatusDropdown();
                return;
            }

            var row = dgvJobs.SelectedRows[0];
            _selectedJobId = Convert.ToInt32(row.Cells["JobID"].Value);
            string currentStatus = row.Cells["Status"].Value.ToString();

            PopulateStatusDropdown(currentStatus);
        }

        /* ---------- update button ---------- */

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (_selectedJobId == null) return;

            string newStatus = cmbStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus)) return;

            // Verify transition again (defensive)
            string currentStatus = dgvJobs.SelectedRows[0].Cells["Status"].Value.ToString();
            if (!_allowed.TryGetValue(currentStatus, out var allowedNext) ||
                Array.IndexOf(allowedNext, newStatus) < 0)
            {
                MessageBox.Show($"Cannot change status from '{currentStatus}' to '{newStatus}'.",
                                "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string sql = "UPDATE Job SET Status = @status WHERE JobID = @jobId";
            try
            {
                Database.ExecuteNonQuery(sql, new Dictionary<string, object>
                {
                    { "@status", newStatus },
                    { "@jobId",  _selectedJobId.Value }
                });

                MessageBox.Show("Job status updated.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadJobs(); // refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MyJobsControl_Load(object sender, EventArgs e)
        {
            
        }

        private void lblTransportUnitInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
