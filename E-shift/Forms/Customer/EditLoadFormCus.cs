using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using E_shift.DataAccess;

namespace E_shift.Forms.Customer
{
    public partial class EditLoadFormCus : Form
    {
        private readonly int _loadId;
        private int _customerId;

        public EditLoadFormCus(int loadId)
        {
            InitializeComponent();
            _loadId = loadId;
        }

        private void EditLoadFormCus_Load(object sender, EventArgs e)
        {
            LoadLoadDetails();
        }

        private void LoadLoadDetails()
        {
            string query = @"
                SELECT Weight, StartDate, EndDate
                FROM Load
                WHERE LoadID = @LoadID";

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LoadID", _loadId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtWeight.Text = reader["Weight"].ToString();
                        dtpStartDate.Value = Convert.ToDateTime(reader["StartDate"]);
                        dtpEndDate.Value = Convert.ToDateTime(reader["EndDate"]);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("Weight field must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string updateQuery = @"
                UPDATE Load 
                SET Weight = @Weight, StartDate = @StartDate, EndDate = @EndDate
                WHERE LoadID = @LoadID";

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Weight", txtWeight.Text.Trim());
                    cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                    cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
                    cmd.Parameters.AddWithValue("@LoadID", _loadId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Load updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating load: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
           
        }
    }
}
