using E_shift.DataAccess;
using E_shift.Models;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms
{
    public partial class ClerkDashboard : Form
    {
        private User _loggedInUser;

        public ClerkDashboard(User user)
        {
            InitializeComponent();
            _loggedInUser = user;

            ApplyButtonStyles();
            btnAddCustomer.Click += btnAddCustomer_Click;
            btnCreateJob.Click += btnCreateJob_Click;
            btnAddLoad.Click += new System.EventHandler(this.btnAddLoad_Click);


            this.Load += ClerkDashboard_Load;

        }

        private void ClerkDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_loggedInUser.Username} ({_loggedInUser.Role})!";
            LoadDashboardInfo();
        }

        private void ApplyButtonStyles()
        {
            // Style sidebar buttons
            StyleButton(dashBoard, Color.FromArgb(60, 60, 60));
            StyleButton(btnCustomers, Color.FromArgb(60, 60, 60));
            StyleButton(btnJobs, Color.FromArgb(60, 60, 60));
            StyleButton(BtnLoads, Color.FromArgb(60, 60, 60));
            StyleButton(btnTransportUnits, Color.FromArgb(60, 60, 60));
            StyleButton(btnLogout, Color.FromArgb(220, 53, 69));

            // Style main content buttons
            StyleButton(btnAddCustomer, Color.FromArgb(40, 167, 69));
            StyleButton(btnCreateJob, Color.FromArgb(0, 123, 255));
            StyleButton(btnAddLoad, Color.FromArgb(23, 162, 184));
            StyleButton(btnAddTransportUnit, Color.FromArgb(220, 53, 69));
        }

        private void StyleButton(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        private void LoadDashboardInfo()
        {
            // TODO: Replace with your actual DB queries

            lblJobsToday.Text = $"Jobs Today: {GetJobsCount(DateTime.Today, DateTime.Today)}";
            lblJobsThisWeek.Text = $"Jobs This Week: {GetJobsCount(DateTime.Today.AddDays(-7), DateTime.Today)}";

            lblAvailableLorries.Text = $"Lorries Available: {GetAvailableUnitsCount("Lorry")}";
            lblAvailableDrivers.Text = $"Drivers Available: {GetAvailableUnitsCount("Driver")}";
            lblAvailableAssistants.Text = $"Assistants Available: {GetAvailableUnitsCount("Assistant")}";
            lblAvailableContainers.Text = $"Containers Available: {GetAvailableUnitsCount("Container")}";
        }

        private int GetJobsCount(DateTime fromDate, DateTime toDate)
        {
            // TODO: Implement your DB query here
            return 5; // dummy
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

            var jobControl = new JobManagementControl();
            {
                Dock = DockStyle.Fill; // this code is the one making the panel fit full size of the pnlMainContent
            };

            pnlMainContent.Controls.Add(jobControl);
        }

        private void btnLoads_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            var loadControl = new LoadManagementControl();
            {
                Dock = DockStyle.Fill;
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

        private void btnAddLoad_Click(object sender, EventArgs e)
        {
            var form = new AddLoadForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // reload data if needed
                LoadDashboardInfo(); // or refresh current view
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
            var control = new VechilcleManagementControl();
            control.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(control);
        }

        private void btnContainer_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            var control = new ContainersManagementControl();
            control.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(control);
        }
    }
}
