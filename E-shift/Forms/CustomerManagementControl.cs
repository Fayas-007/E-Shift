using System;
using System.Windows.Forms;
using System.Data;
using E_shift.DataAccess;
using E_shift.Models;
using System.Drawing;
using System.Data.SqlClient;
using System.Collections.Generic;
using E_shift.Helpers;

namespace E_shift.Forms
{
    public partial class CustomerManagementControl : UserControl
    {
        public CustomerManagementControl()
        {
            InitializeComponent();
            DataGridViewHelper.ApplyTheme(dgvCustomers);
            DataGridViewHelper.AttachCellClickEvent(dgvCustomers, DgvCustomers_CellClick);


            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;
          
        }

        private void CustomerManagementControl_Load(object sender, EventArgs e)
        {
            LoadCustomerData(); // Load data into grid when control loads
            cmbSearchBy.SelectedIndex = 0;
            cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchBy.DrawItem += cmbSearchBy_DrawItem;

        }
        private void cmbSearchBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox combo = (ComboBox)sender;

            // Background - use gradient for modern feel
            Color baseColor = Color.FromArgb(30, 144, 255); // DodgerBlue
            Color baseColorDark = Color.FromArgb(25, 120, 230);

            using (var backgroundBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                e.Bounds,
                baseColor,
                baseColorDark,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Highlight selected or hovered item with lighter overlay
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (var highlightBrush = new SolidBrush(Color.FromArgb(100, Color.LightSkyBlue)))
                {
                    e.Graphics.FillRectangle(highlightBrush, e.Bounds);
                }
            }

            // Draw text with padding and vertical center alignment
            string text = combo.Items[e.Index].ToString();
            using (var textBrush = new SolidBrush(Color.White))
            {
                var textRect = new Rectangle(
                    e.Bounds.Left + 8,
                    e.Bounds.Top,
                    e.Bounds.Width - 8,
                    e.Bounds.Height);

                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    combo.Font,
                    textRect,
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }

            // Draw focus rectangle if needed
            e.DrawFocusRectangle();
        }


        private void TxtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void LoadCustomerData(string columnFilter = null, string keyword = null)
        {
            try
            {
                string query = "SELECT CustomerID, FullName, Phone, Email, Address FROM Customer";

                if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                {
                    query += $" WHERE {columnFilter} LIKE @keyword";
                }

                query += " ORDER BY CustomerID DESC";


                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(columnFilter) && !string.IsNullOrWhiteSpace(keyword))
                        {
                            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvCustomers.DataSource = dt;

                        dgvCustomers.Columns["FullName"].HeaderText = "Full Name";
                        dgvCustomers.Columns["Phone"].HeaderText = "Phone Number";
                        dgvCustomers.Columns["Email"].HeaderText = "Email Address";
                        dgvCustomers.Columns["Address"].HeaderText = "Address";

                        dgvCustomers.Columns["FullName"].Width = 130;
                        dgvCustomers.Columns["Phone"].Width = 130;
                        dgvCustomers.Columns["Email"].Width = 130;
                        dgvCustomers.Columns["Address"].Width = 130;

                        // Remove existing Edit/Delete buttons
                        for (int i = dgvCustomers.Columns.Count - 1; i >= 0; i--)
                        {
                            if (dgvCustomers.Columns[i].Name == "Edit" || dgvCustomers.Columns[i].Name == "Delete")
                                dgvCustomers.Columns.RemoveAt(i);
                        }

                        // Add Edit/Delete buttons again
                        var editButtonColumn = new DataGridViewButtonColumn();
                        editButtonColumn.Name = "Edit";
                        editButtonColumn.HeaderText = "";
                        editButtonColumn.Text = "Edit";
                        editButtonColumn.UseColumnTextForButtonValue = true;
                        dgvCustomers.Columns.Add(editButtonColumn);

                        var deleteButtonColumn = new DataGridViewButtonColumn();
                        deleteButtonColumn.Name = "Delete";
                        deleteButtonColumn.HeaderText = "";
                        deleteButtonColumn.Text = "Delete";
                        deleteButtonColumn.UseColumnTextForButtonValue = true;
                        dgvCustomers.Columns.Add(deleteButtonColumn);

                        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            // Reload all if empty or placeholder
            if (string.IsNullOrWhiteSpace(searchText) || searchText == "Search...")
            {
                LoadCustomerData();
                return;
            }

            if (cmbSearchBy.SelectedItem == null)
                return;

            string selectedColumn = cmbSearchBy.SelectedItem.ToString();
            string query;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (selectedColumn == "CustomerID")
            {
                // Exact match for ID, ensure it's a number
                if (!int.TryParse(searchText, out int customerId))
                {
                    // If not a valid number, do not proceed
                    return;
                }

                query = $"SELECT CustomerID, FullName, Phone, Email, Address FROM Customer WHERE CustomerID = @searchText ORDER BY CustomerID DESC";
                parameters["@searchText"] = customerId;
            }
            else
            {
                // Partial match for text fields
                query = $"SELECT CustomerID, FullName, Phone, Email, Address FROM Customer WHERE {selectedColumn} LIKE @searchText ORDER BY CustomerID DESC";
                parameters["@searchText"] = $"%{searchText}%";
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvCustomers.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            if (addCustomerForm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomerData(); // Refresh after adding
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void DgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header clicks or out of bounds
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgv = sender as DataGridView;
            var columnName = dgv.Columns[e.ColumnIndex].Name;

            if (columnName == "Edit")
            {
                // Get Customer ID or unique key if you have it in DataGridView
                // For example, assuming you have a hidden column "CustomerID"
                int customerId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["CustomerID"].Value);

                // Open edit form with that customer ID
                EditCustomer editForm = new EditCustomer(customerId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerData(); // refresh grid after edit
                }
            }
            else if (columnName == "Delete")
            {
                int customerId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["CustomerID"].Value);

                var confirmResult = MessageBox.Show("Are you sure to delete this customer and all related jobs?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            conn.Open();

                            using (SqlTransaction transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    // Delete related jobs first
                                    string deleteJobsQuery = "DELETE FROM Job WHERE CustomerID = @CustomerID";
                                    using (SqlCommand cmd = new SqlCommand(deleteJobsQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Then delete the customer
                                    string deleteCustomerQuery = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                                    using (SqlCommand cmd = new SqlCommand(deleteCustomerQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    throw ex;
                                }
                            }
                        }

                        LoadCustomerData(); // refresh grid after delete
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting customer and jobs: " + ex.Message);
                    }
                }
            }
        }

            private void cmbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
