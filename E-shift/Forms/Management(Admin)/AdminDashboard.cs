using E_shift.DataAccess;
using E_shift.Models;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class AdminDashboard : Form
    {
        private User _loggedInUser;

        public AdminDashboard(User user)
        {
            InitializeComponent();
            _loggedInUser = user;

            btnAddCustomer.Click += btnAddCustomer_Click;
            btnCreateJob.Click += btnCreateJob_Click;
            btnUsers.Visible = _loggedInUser.Role == "admin";


            this.Load += ClerkDashboard_Load;
            this.FormClosing += EmployeeDashboard_FormClosing;
        }

        private void ClerkDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_loggedInUser.Username}";
            LoadDashboardInfo();
        }





        private void LoadDashboardInfo()
        {
            

            lblLoadsToday.Text = $"Loads Today: {GetLoadsScheduledTodayCount()}";
            lblPendingLoads.Text = $"Pending Loads: {GetPendingLoadsCount()}";

            lblAvailableLorries.Text = $"Lorries Available: {GetAvailableUnitsCount("Lorry")}";
            lblAvailableDrivers.Text = $"Drivers Available: {GetAvailableUnitsCount("Driver")}";
            lblAvailableAssistants.Text = $"Assistants Available: {GetAvailableUnitsCount("Assistant")}";
            lblAvailableContainers.Text = $"Containers Available: {GetAvailableUnitsCount("Container")}";
        }

        private int GetLoadsScheduledTodayCount()
        {
            string query = @"SELECT COUNT(*) 
                     FROM LoadSchedule 
                     WHERE CAST(StartDate AS DATE) = CAST(GETDATE() AS DATE)";

            try
            {
                object result = Database.ExecuteSingleValue(query);
                return result != null && int.TryParse(result.ToString(), out int count) ? count : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching today's loads: " + ex.Message);
                return 0;
            }
        }


        private int GetPendingLoadsCount()
        {
            string query = @"SELECT COUNT(*) 
                     FROM Load 
                     WHERE Status = 'Pending'";

            try
            {
                object result = Database.ExecuteSingleValue(query);
                return result != null && int.TryParse(result.ToString(), out int count) ? count : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching pending loads: " + ex.Message);
                return 0;
            }
        }



        private int GetAvailableUnitsCount(string unitType)
        {
            string query = unitType switch
            {
                "Driver" => "SELECT COUNT(*) FROM [User] WHERE IsAvailable = 1 AND Role = 'Driver'",
                "Assistant" => "SELECT COUNT(*) FROM [User] WHERE IsAvailable = 1 AND Role = 'Assistant'",
                "Lorry" => "SELECT COUNT(*) FROM Lorry WHERE IsAvailable = 1",
                "Container" => "SELECT COUNT(*) FROM Container WHERE IsAvailable = 1",
                _ => null
            };

            if (query == null) return 0;

            try
            {
                object result = Database.ExecuteSingleValue(query);

                if (result != null && int.TryParse(result.ToString(), out int count))
                    return count;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching available unit count: " + ex.Message);
                return 0;
            }
        }



        private void DashBoard_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            pnlMainContent.Controls.Add(lblWelcome);
            pnlMainContent.Controls.Add(btnAddCustomer);
            pnlMainContent.Controls.Add(btnCreateJob);
            pnlMainContent.Controls.Add(btnAddLoad);
            pnlMainContent.Controls.Add(btnAddTransportUnit);
            pnlMainContent.Controls.Add(pnlInfoBoxes);

            LoadDashboardInfo();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            var custControl = new CustomerManagementControl() { Dock = DockStyle.Fill };
            pnlMainContent.Controls.Add(custControl);
        }

        private void BtnJobs_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            var jobControl = new JobManagementControl
            {
                Dock = DockStyle.Fill           // fills pnlMainContent
            };
            pnlMainContent.Controls.Add(jobControl);
        }

        private void btnLoads_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            var loadControl = new LoadManagementControl
            {
                Dock = DockStyle.Fill
            };
            pnlMainContent.Controls.Add(loadControl);


        }
        private void btnTransportUnits_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            var control = new TransportUnitManagementControl();
            control.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(control);
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var form = new AddCustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDashboardInfo();
            }
        }

        private void btnCreateJob_Click(object sender, EventArgs e)
        {
            var form = new AddJobForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDashboardInfo();
            }
        }

        private void btnAddTransportUnit_Click_1(object sender, EventArgs e)
        {
            var form = new AddTransportUnit();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDashboardInfo();
            }
        }


        private void pnlMainContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMainContent_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dashBoard_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAssignTransportUnit_Click_1(object sender, EventArgs e)
        {

        }

        private void lblJobsToday_Click(object sender, EventArgs e)
        {

        }

        private void lblAvailableLorries_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblAvailableDrivers_Click(object sender, EventArgs e)
        {

        }

        private void btnVehicle_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            var control = new VehicleManagementControl();
            control.Dock = DockStyle.Fill;

            pnlMainContent.Controls.Add(control);
        }

        private void btnContainer_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            var control = new ContainerManagementControl();
            control.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(control);
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();

            // Then close this dashboard
            this.FormClosing -= EmployeeDashboard_FormClosing; // prevent app exit
            this.Close();
        }
        private void EmployeeDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();//shuttign down the app so app completley closes.
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            var userControl = new UserManagementControl { Dock = DockStyle.Fill };
            pnlMainContent.Controls.Add(userControl);
        }

        private void btnAddLoad_Click_1(object sender, EventArgs e)
        {
            var form = new AddLoadForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // reload data if needed
                LoadDashboardInfo(); // or refresh current view
            }
        }

        private void pnlInfoBoxes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
