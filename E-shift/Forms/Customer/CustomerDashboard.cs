using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using E_shift.DataAccess;
using E_shift.Models;

namespace E_shift.Forms.Customer
{
    public partial class CustomerDashboard : Form
    {
        private readonly User _currentUser;
        private int _customerId;  // mapped from UserID ➜ Customer table

        public CustomerDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;

            ApplyButtonStyles();
            MapUserToCustomerId();

            this.Load += CustomerDashboard_Load;
            this.FormClosing += (s, e) => Application.Exit();

            // Hook up button events
            btnDashboard.Click += btnDashboard_Click;
            btnMyLoads.Click += btnMyLoads_Click;
            btnLogout.Click += btnLogout_Click;
        }

        private static void StyleButton(Button btn, Color bg)
        {
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        private void ApplyButtonStyles()
        {
            StyleButton(btnDashboard, Color.FromArgb(60, 60, 60));
            StyleButton(btnMyLoads, Color.FromArgb(60, 60, 60));
            StyleButton(btnLogout, Color.FromArgb(220, 53, 69));
        }


        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_currentUser.Username}";
            ShowSummaryCards();
        }

        private void MapUserToCustomerId()
        {
            const string sql = "SELECT CustomerID FROM Customer WHERE UserID = @uid";
            var idObj = Database.ExecuteSingleValue(sql, new Dictionary<string, object> { { "@uid", _currentUser.UserID } });

            if (idObj == null)
            {
                MessageBox.Show("Your customer record was not found. Please contact admin.");
                _customerId = -1;
            }
            else
            {
                _customerId = Convert.ToInt32(idObj);
            }
        }

        private void ShowSummaryCards()
        {
            if (_customerId <= 0) return;

            var parms = new Dictionary<string, object> { { "@cid", _customerId } };

            // total jobs
            int totalJobs = Convert.ToInt32(Database
                   .ExecuteSingleValue("SELECT COUNT(*) FROM Job WHERE CustomerID = @cid", parms));

            int inProg = Convert.ToInt32(Database
                   .ExecuteSingleValue("SELECT COUNT(*) FROM Job WHERE CustomerID = @cid AND Status = 'InProgress'", parms));
            int pending = Convert.ToInt32(Database
                   .ExecuteSingleValue("SELECT COUNT(*) FROM Job WHERE CustomerID = @cid AND Status = 'Pending'", parms));
            int completed = Convert.ToInt32(Database
                   .ExecuteSingleValue("SELECT COUNT(*) FROM Job WHERE CustomerID = @cid AND Status = 'Completed'", parms));
            int cancelled = Convert.ToInt32(Database
                   .ExecuteSingleValue("SELECT COUNT(*) FROM Job WHERE CustomerID = @cid AND Status = 'Cancelled'", parms));

            // update labels only
            lblInProgress.Text = $"In Progress: {inProg}";
            lblPending.Text = $"Pending: {pending}";
            lblCompleted.Text = $"Completed: {completed}";
            lblCancelled.Text = $"Cancelled: {cancelled}";
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            pnlMainContent.Controls.Add(pnlDashboard);  
            ShowSummaryCards();                         
        }

        private void btnMyLoads_Click(object sender, EventArgs e)
        {
            // Example: load the customer loads user control
            // pnlMainContent.Controls.Clear();
            // var loads = new CustomerLoadsControl(_customerId) { Dock = DockStyle.Fill };
            // pnlMainContent.Controls.Add(loads);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            // Example: load the profile user control
            // pnlMainContent.Controls.Clear();
            // var prof = new CustomerProfileControl(_customerId) { Dock = DockStyle.Fill };
            // pnlMainContent.Controls.Add(prof);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.FormClosing -= (s, ev) => Application.Exit();
            Close();
        }

        private void pnlDashboard_Paint(object sender, PaintEventArgs e)
        {
            // Optional: custom painting logic if needed
        }

        private void lblPending_Click(object sender, EventArgs e)
        {
            // Optional: add click event if needed
        }

        private void pnlCompleted_Paint(object sender, PaintEventArgs e)
        {
            // Optional: custom painting logic if needed
        }

        private void pnlDashboard_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnMyLoads_Click_1(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            var loadsControl = new CustomerLoadsControl(_customerId) { Dock = DockStyle.Fill };
            pnlMainContent.Controls.Add(loadsControl);
        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            if (_customerId <= 0)
            {
                MessageBox.Show("Invalid customer. Cannot add job.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var addJobForm = new AddJobForm(_customerId))
            {
                var result = addJobForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ShowSummaryCards();  // Refresh the dashboard summary
                }
            }
        }

        private void btnJobs_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            var loadsControl = new CustomerJobControl(_customerId) { Dock = DockStyle.Fill };
            pnlMainContent.Controls.Add(loadsControl);
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
