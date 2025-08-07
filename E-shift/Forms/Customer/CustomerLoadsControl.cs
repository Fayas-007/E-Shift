using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Helpers;

namespace E_shift.Forms.Customer
{
    public partial class CustomerLoadsControl : UserControl
    {
        private readonly int _customerId;
        public CustomerLoadsControl(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            DataGridViewHelper.ApplyTheme(dgvLoads);

            LoadCustomerLoads();

            // Attach CellClick event (better for button columns)
            dgvLoads.CellClick += dgvLoads_CellClick;
        }

        /// <summary>
        /// Loads all loads associated with the current customer.
        /// </summary>
        private void LoadCustomerLoads()
        {
            string query = @"
            SELECT 
                l.LoadID,
                l.Weight,
                l.Status,
                l.StartDate,
                l.EndDate,
                j.JobID
            FROM Load l
            INNER JOIN Job j ON l.JobID = j.JobID
            WHERE j.CustomerID = @cid";

            var parameters = new Dictionary<string, object>
            {
                { "@cid", _customerId }
            };

            DataTable dt = Database.GetData(query, parameters);
            dgvLoads.DataSource = dt;

            FormatGrid();

            AddEditDeleteButtons();  // <== Add this here
        }

        /// <summary>
        /// Applies column formatting and visibility to the DataGridView.
        /// </summary>
        private void FormatGrid()
        {
            if (dgvLoads.Columns.Contains("LoadID"))
                dgvLoads.Columns["LoadID"].Visible = false;

            dgvLoads.Columns["Weight"].HeaderText = "Weight (kg)";
            dgvLoads.Columns["Status"].HeaderText = "Status";
            dgvLoads.Columns["StartDate"].HeaderText = "Start Date";
            dgvLoads.Columns["EndDate"].HeaderText = "End Date";
            dgvLoads.Columns["JobID"].HeaderText = "Job ID";

            dgvLoads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoads.ReadOnly = true;
        }

        /// <summary>
        /// Handles cell click events for future features like view, edit, etc.
        /// </summary>
        private void dgvLoads_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string columnName = dgvLoads.Columns[e.ColumnIndex].Name;
            int loadId = Convert.ToInt32(dgvLoads.Rows[e.RowIndex].Cells["LoadID"].Value);
            string loadStatus = dgvLoads.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            // Only allow Edit/Delete if Status is Pending or Submitted
            bool canEditOrDelete = (loadStatus == "Pending" || loadStatus == "Submitted");

            if (columnName == "Edit")
            {
                if (!canEditOrDelete)
                {
                    MessageBox.Show("This load cannot be edited because it is already in progress", "Cannot Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var editForm = new EditLoadFormCus(loadId))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadCustomerLoads(); // refresh grid after editing
                    }
                }

            }
            else if (columnName == "Delete")
            {
                if (!canEditOrDelete)
                {
                    MessageBox.Show("This load cannot be deleted because it is already in progress .", "Deletion Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("Are you sure you want to delete this load and all its schedules?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    DeleteLoadAndSchedules(loadId);
                    LoadCustomerLoads(); // refresh the grid
                }
            }
        }

        private void DeleteLoadAndSchedules(int loadId)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Delete LoadSchedule rows first
                        string deleteSchedulesSql = "DELETE FROM LoadSchedule WHERE LoadID = @LoadID";
                        using (var cmd = new System.Data.SqlClient.SqlCommand(deleteSchedulesSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@LoadID", loadId);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete Load row
                        string deleteLoadSql = "DELETE FROM Load WHERE LoadID = @LoadID";
                        using (var cmd = new System.Data.SqlClient.SqlCommand(deleteLoadSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@LoadID", loadId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                MessageBox.Show("Load and associated schedules deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting load: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddEditDeleteButtons()
        {
            if (!dgvLoads.Columns.Contains("Edit"))
            {
                var editButton = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                };
                dgvLoads.Columns.Add(editButton);
            }

            if (!dgvLoads.Columns.Contains("Delete"))
            {
                var deleteButton = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };
                dgvLoads.Columns.Add(deleteButton);
            }

            DataGridViewHelper.ApplyTheme(dgvLoads);
        }
        private void CustomerLoadsControl_Load(object sender, EventArgs e)
        {
            // Optional logic during control load (if needed)
        }
    }
}
