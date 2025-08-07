using E_shift.DataAccess;
using E_shift.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class SharedDashboard : Form
    {
        private readonly User _loggedInUser;

        public SharedDashboard(User user)
        {
            InitializeComponent();
            _loggedInUser = user;

            ApplyButtonStyles();

            this.Load += SharedDashboard_Load;
            this.FormClosing += SharedDashboard_FormClosing;
        }

        /* ---------- UI INITIALISATION ---------- */

        private void SharedDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_loggedInUser.Username}";
            ShowTransportUnitInfo();
            LoadDashboardInfo();
        }

        private void ApplyButtonStyles()
        {
            StyleButton(dashBoard, Color.FromArgb(60, 60, 60));
            StyleButton(btnLogout, Color.FromArgb(220, 53, 69));
        }

        private static void StyleButton(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        /* ---------- DASHBOARD DATA ---------- */

        private void LoadDashboardInfo()
        {
            int? transportUnitId = GetUserTransportUnitId(_loggedInUser.UserID);

            if (transportUnitId == null)
            {
                lblJobsToday.Text = "Assigned Loads Today: 0";
                lblJobsThisWeek.Text = "Assigned Loads This Week: 0";
                return;
            }

            DateTime today = DateTime.Today;
            DateTime weekEnd = today.AddDays(7);

            int loadsToday = GetLoadCountForTransportUnit(transportUnitId.Value, today, today);
            int loadsThisWeek = GetLoadCountForTransportUnit(transportUnitId.Value, today, weekEnd);

            lblJobsToday.Text = $"Total of [ {loadsToday} ]";
            lblJobsThisWeek.Text = $"Total of [ {loadsThisWeek} ]";
        }

        private int? GetUserTransportUnitId(int userId)
        {
            string query = @"
                SELECT TransportUnitID
                FROM TransportUnit
                WHERE DriverID = @UserId OR AssistantID = @UserId";

            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId }
            };

            try
            {
                object result = Database.ExecuteSingleValue(query, parameters);
                if (result != null && int.TryParse(result.ToString(), out int transportUnitId))
                {
                    return transportUnitId;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching transport unit: " + ex.Message);
                return null;
            }
        }

        private int GetLoadCountForTransportUnit(int transportUnitId, DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM LoadSchedule
                WHERE TransportUnitID = @UnitId
                  AND StartDate >= @FromDate
                  AND StartDate <= @ToDate";

            var parameters = new Dictionary<string, object>
            {
                { "@UnitId", transportUnitId },
                { "@FromDate", fromDate },
                { "@ToDate", toDate }
            };

            try
            {
                object result = Database.ExecuteSingleValue(sql, parameters);
                return (result != null && int.TryParse(result.ToString(), out int count)) ? count : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching load count: " + ex.Message);
                return 0;
            }
        }

        /* ---------- BUTTON HANDLERS ---------- */

        private void DashBoard_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            // Re-add the default dashboard controls
            pnlMainContent.Controls.Add(lblWelcome);
            pnlMainContent.Controls.Add(lblTransportUnitInfo);
            pnlMainContent.Controls.Add(pnlInfoBoxes);

            lblWelcome.Visible = true;
            lblTransportUnitInfo.Visible = true;
            pnlInfoBoxes.Visible = true;

            LoadDashboardInfo(); // Refresh values
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();

            this.FormClosing -= SharedDashboard_FormClosing;
            this.Close();
        }

        private void ShowTransportUnitInfo()
        {
            string query = @"
                SELECT UnitName 
                FROM TransportUnit 
                WHERE DriverID = @UserId OR AssistantID = @UserId";

            var parameters = new Dictionary<string, object>
            {
                { "@UserId", _loggedInUser.UserID }
            };

            try
            {
                object result = Database.ExecuteSingleValue(query, parameters);

                if (result != null && result != DBNull.Value)
                {
                    lblTransportUnitInfo.Text = $"You are assigned to transport unit: {result}";
                    lblTransportUnitInfo.ForeColor = Color.FromArgb(39, 174, 96);
                }
                else
                {
                    lblTransportUnitInfo.Text = "Transport Unit: Not Assigned";
                    lblTransportUnitInfo.ForeColor = Color.FromArgb(231, 76, 60);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching transport unit info: " + ex.Message);
                lblTransportUnitInfo.Text = "Transport Unit: Error";
                lblTransportUnitInfo.ForeColor = Color.Red;
            }
        }

        /* ---------- FORM LIFECYCLE ---------- */

        private void SharedDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pnlMainContent_Paint(object sender, PaintEventArgs e)
        {
            // Optional: remove or use if needed
        }

        private void btnAssignedLoads_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            int? transportUnitId = GetUserTransportUnitId(_loggedInUser.UserID);
            if (transportUnitId != null)
            {
                var loadsControl = new AssignedLoadsControl(transportUnitId.Value);
                loadsControl.Dock = DockStyle.Fill;
                pnlMainContent.Controls.Add(loadsControl);
            }
            else
            {
                MessageBox.Show("You are not assigned to any transport unit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblToday_Click(object sender, EventArgs e)
        {

        }
    }
}
