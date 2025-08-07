using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class ClerkDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlSidebar;
        private Button dashBoard;
        private Button btnCustomers;
        private Button btnJobs;
        private Button BtnLoads;
        private Button btnTransportUnits;

        private Button btnLogout;

        private Panel pnlMainContent;
        private Label lblWelcome;

        private Button btnAddCustomer;
        private Button btnCreateJob;
        private Button btnAddLoad;
        private Button btnAddTransportUnit;
        private Button btnVehicle;
        private Button btnContainer;
        private Panel pnlInfoBoxes;
        private Label lblJobsToday;
        private Label lblJobsThisWeek;
        private Label lblAvailableLorries;
        private Label lblAvailableDrivers;
        private Label lblAvailableAssistants;
        private Label lblAvailableContainers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnContainer = new System.Windows.Forms.Button();
            this.btnVehicle = new System.Windows.Forms.Button();
            this.btnTransportUnits = new System.Windows.Forms.Button();
            this.BtnLoads = new System.Windows.Forms.Button();
            this.btnJobs = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.dashBoard = new System.Windows.Forms.Button();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnCreateJob = new System.Windows.Forms.Button();
            this.btnAddLoad = new System.Windows.Forms.Button();
            this.btnAddTransportUnit = new System.Windows.Forms.Button();
            this.pnlInfoBoxes = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblJobsToday = new System.Windows.Forms.Label();
            this.lblJobsThisWeek = new System.Windows.Forms.Label();
            this.lblAvailableDrivers = new System.Windows.Forms.Label();
            this.lblAvailableLorries = new System.Windows.Forms.Label();
            this.lblAvailableAssistants = new System.Windows.Forms.Label();
            this.lblAvailableContainers = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.pnlInfoBoxes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnContainer);
            this.pnlSidebar.Controls.Add(this.btnVehicle);
            this.pnlSidebar.Controls.Add(this.btnTransportUnits);
            this.pnlSidebar.Controls.Add(this.BtnLoads);
            this.pnlSidebar.Controls.Add(this.btnJobs);
            this.pnlSidebar.Controls.Add(this.btnCustomers);
            this.pnlSidebar.Controls.Add(this.dashBoard);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(193, 593);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 543);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(193, 50);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnContainer
            // 
            this.btnContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnContainer.FlatAppearance.BorderSize = 0;
            this.btnContainer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContainer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnContainer.ForeColor = System.Drawing.Color.White;
            this.btnContainer.Location = new System.Drawing.Point(0, 300);
            this.btnContainer.Name = "btnContainer";
            this.btnContainer.Size = new System.Drawing.Size(193, 50);
            this.btnContainer.TabIndex = 7;
            this.btnContainer.Text = "Container";
            this.btnContainer.UseVisualStyleBackColor = false;
            this.btnContainer.Click += new System.EventHandler(this.btnContainer_Click);
            // 
            // btnVehicle
            // 
            this.btnVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnVehicle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVehicle.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVehicle.FlatAppearance.BorderSize = 0;
            this.btnVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehicle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnVehicle.ForeColor = System.Drawing.Color.White;
            this.btnVehicle.Location = new System.Drawing.Point(0, 250);
            this.btnVehicle.Name = "btnVehicle";
            this.btnVehicle.Size = new System.Drawing.Size(193, 50);
            this.btnVehicle.TabIndex = 6;
            this.btnVehicle.Text = "Vehicle";
            this.btnVehicle.UseVisualStyleBackColor = false;
            this.btnVehicle.Click += new System.EventHandler(this.btnVehicle_Click);
            // 
            // btnTransportUnits
            // 
            this.btnTransportUnits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnTransportUnits.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransportUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransportUnits.FlatAppearance.BorderSize = 0;
            this.btnTransportUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransportUnits.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTransportUnits.ForeColor = System.Drawing.Color.White;
            this.btnTransportUnits.Location = new System.Drawing.Point(0, 200);
            this.btnTransportUnits.Name = "btnTransportUnits";
            this.btnTransportUnits.Size = new System.Drawing.Size(193, 50);
            this.btnTransportUnits.TabIndex = 4;
            this.btnTransportUnits.Text = "Transport Units";
            this.btnTransportUnits.UseVisualStyleBackColor = false;
            this.btnTransportUnits.Click += new System.EventHandler(this.btnTransportUnits_Click);
            // 
            // BtnLoads
            // 
            this.BtnLoads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BtnLoads.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLoads.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnLoads.FlatAppearance.BorderSize = 0;
            this.BtnLoads.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoads.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnLoads.ForeColor = System.Drawing.Color.White;
            this.BtnLoads.Location = new System.Drawing.Point(0, 150);
            this.BtnLoads.Name = "BtnLoads";
            this.BtnLoads.Size = new System.Drawing.Size(193, 50);
            this.BtnLoads.TabIndex = 3;
            this.BtnLoads.Text = "Loads";
            this.BtnLoads.UseVisualStyleBackColor = false;
            this.BtnLoads.Click += new System.EventHandler(this.btnLoads_Click);
            // 
            // btnJobs
            // 
            this.btnJobs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnJobs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJobs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnJobs.FlatAppearance.BorderSize = 0;
            this.btnJobs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJobs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnJobs.ForeColor = System.Drawing.Color.White;
            this.btnJobs.Location = new System.Drawing.Point(0, 100);
            this.btnJobs.Name = "btnJobs";
            this.btnJobs.Size = new System.Drawing.Size(193, 50);
            this.btnJobs.TabIndex = 2;
            this.btnJobs.Text = "Jobs";
            this.btnJobs.UseVisualStyleBackColor = false;
            this.btnJobs.Click += new System.EventHandler(this.BtnJobs_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCustomers.ForeColor = System.Drawing.Color.White;
            this.btnCustomers.Location = new System.Drawing.Point(0, 50);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(193, 50);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.BtnCustomers_Click);
            // 
            // dashBoard
            // 
            this.dashBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dashBoard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dashBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.dashBoard.FlatAppearance.BorderSize = 0;
            this.dashBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashBoard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.dashBoard.ForeColor = System.Drawing.Color.White;
            this.dashBoard.Location = new System.Drawing.Point(0, 0);
            this.dashBoard.Name = "dashBoard";
            this.dashBoard.Size = new System.Drawing.Size(193, 50);
            this.dashBoard.TabIndex = 0;
            this.dashBoard.Text = "Dashboard";
            this.dashBoard.UseVisualStyleBackColor = false;
            this.dashBoard.Click += new System.EventHandler(this.DashBoard_Click);
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.SystemColors.MenuBar;
            this.pnlMainContent.Controls.Add(this.lblWelcome);
            this.pnlMainContent.Controls.Add(this.btnAddCustomer);
            this.pnlMainContent.Controls.Add(this.btnCreateJob);
            this.pnlMainContent.Controls.Add(this.btnAddLoad);
            this.pnlMainContent.Controls.Add(this.btnAddTransportUnit);
            this.pnlMainContent.Controls.Add(this.pnlInfoBoxes);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(193, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1056, 593);
            this.pnlMainContent.TabIndex = 1;
            this.pnlMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMainContent_Paint_1);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblWelcome.Location = new System.Drawing.Point(23, 32);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(360, 45);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, Clerk!";
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddCustomer.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddCustomer.Location = new System.Drawing.Point(78, 130);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(189, 59);
            this.btnAddCustomer.TabIndex = 1;
            this.btnAddCustomer.Text = "Add New Customer";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            // 
            // btnCreateJob
            // 
            this.btnCreateJob.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateJob.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnCreateJob.Location = new System.Drawing.Point(320, 130);
            this.btnCreateJob.Name = "btnCreateJob";
            this.btnCreateJob.Size = new System.Drawing.Size(189, 59);
            this.btnCreateJob.TabIndex = 2;
            this.btnCreateJob.Text = "Create New Job";
            this.btnCreateJob.UseVisualStyleBackColor = true;
            // 
            // btnAddLoad
            // 
            this.btnAddLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddLoad.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddLoad.Location = new System.Drawing.Point(561, 130);
            this.btnAddLoad.Name = "btnAddLoad";
            this.btnAddLoad.Size = new System.Drawing.Size(189, 59);
            this.btnAddLoad.TabIndex = 3;
            this.btnAddLoad.Text = "Add New Load";
            this.btnAddLoad.UseVisualStyleBackColor = true;
            // 
            // btnAddTransportUnit
            // 
            this.btnAddTransportUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddTransportUnit.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddTransportUnit.Location = new System.Drawing.Point(800, 130);
            this.btnAddTransportUnit.Name = "btnAddTransportUnit";
            this.btnAddTransportUnit.Size = new System.Drawing.Size(189, 59);
            this.btnAddTransportUnit.TabIndex = 4;
            this.btnAddTransportUnit.Text = "Add Transport Unit";
            this.btnAddTransportUnit.UseVisualStyleBackColor = true;
            this.btnAddTransportUnit.Click += new System.EventHandler(this.btnAddTransportUnit_Click_1);
            // 
            // pnlInfoBoxes
            // 
            this.pnlInfoBoxes.BackColor = System.Drawing.SystemColors.MenuBar;
            this.pnlInfoBoxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoBoxes.Controls.Add(this.panel6);
            this.pnlInfoBoxes.Controls.Add(this.panel5);
            this.pnlInfoBoxes.Controls.Add(this.panel4);
            this.pnlInfoBoxes.Controls.Add(this.panel3);
            this.pnlInfoBoxes.Controls.Add(this.panel2);
            this.pnlInfoBoxes.Controls.Add(this.panel1);
            this.pnlInfoBoxes.Controls.Add(this.lblJobsToday);
            this.pnlInfoBoxes.Controls.Add(this.lblJobsThisWeek);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableDrivers);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableLorries);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableAssistants);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableContainers);
            this.pnlInfoBoxes.Location = new System.Drawing.Point(0, 232);
            this.pnlInfoBoxes.Name = "pnlInfoBoxes";
            this.pnlInfoBoxes.Size = new System.Drawing.Size(1056, 361);
            this.pnlInfoBoxes.TabIndex = 5;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel6.Location = new System.Drawing.Point(728, 214);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(33, 116);
            this.panel6.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel5.Location = new System.Drawing.Point(728, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(33, 116);
            this.panel5.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel4.Location = new System.Drawing.Point(383, 214);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(34, 116);
            this.panel4.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel3.Location = new System.Drawing.Point(383, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(34, 116);
            this.panel3.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel2.Location = new System.Drawing.Point(41, 214);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(32, 116);
            this.panel2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Location = new System.Drawing.Point(41, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(32, 116);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblJobsToday
            // 
            this.lblJobsToday.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblJobsToday.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblJobsToday.ForeColor = System.Drawing.Color.Black;
            this.lblJobsToday.Location = new System.Drawing.Point(41, 44);
            this.lblJobsToday.Name = "lblJobsToday";
            this.lblJobsToday.Size = new System.Drawing.Size(269, 116);
            this.lblJobsToday.TabIndex = 0;
            this.lblJobsToday.Text = "Jobs Today: 0";
            this.lblJobsToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJobsToday.Click += new System.EventHandler(this.lblJobsToday_Click);
            // 
            // lblJobsThisWeek
            // 
            this.lblJobsThisWeek.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblJobsThisWeek.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblJobsThisWeek.ForeColor = System.Drawing.Color.Black;
            this.lblJobsThisWeek.Location = new System.Drawing.Point(41, 214);
            this.lblJobsThisWeek.Name = "lblJobsThisWeek";
            this.lblJobsThisWeek.Size = new System.Drawing.Size(269, 116);
            this.lblJobsThisWeek.TabIndex = 1;
            this.lblJobsThisWeek.Text = "Jobs This Week: 0";
            this.lblJobsThisWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvailableDrivers
            // 
            this.lblAvailableDrivers.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAvailableDrivers.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvailableDrivers.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableDrivers.Location = new System.Drawing.Point(383, 44);
            this.lblAvailableDrivers.Name = "lblAvailableDrivers";
            this.lblAvailableDrivers.Size = new System.Drawing.Size(275, 116);
            this.lblAvailableDrivers.TabIndex = 3;
            this.lblAvailableDrivers.Text = "Drivers Available: 0";
            this.lblAvailableDrivers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvailableDrivers.Click += new System.EventHandler(this.lblAvailableDrivers_Click);
            // 
            // lblAvailableLorries
            // 
            this.lblAvailableLorries.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAvailableLorries.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvailableLorries.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableLorries.Location = new System.Drawing.Point(728, 44);
            this.lblAvailableLorries.Name = "lblAvailableLorries";
            this.lblAvailableLorries.Size = new System.Drawing.Size(281, 116);
            this.lblAvailableLorries.TabIndex = 2;
            this.lblAvailableLorries.Text = "Lorries Available: 0";
            this.lblAvailableLorries.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvailableLorries.Click += new System.EventHandler(this.lblAvailableLorries_Click);
            // 
            // lblAvailableAssistants
            // 
            this.lblAvailableAssistants.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAvailableAssistants.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvailableAssistants.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableAssistants.Location = new System.Drawing.Point(383, 214);
            this.lblAvailableAssistants.Name = "lblAvailableAssistants";
            this.lblAvailableAssistants.Size = new System.Drawing.Size(275, 116);
            this.lblAvailableAssistants.TabIndex = 4;
            this.lblAvailableAssistants.Text = "Assistants Available: 0";
            this.lblAvailableAssistants.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvailableContainers
            // 
            this.lblAvailableContainers.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAvailableContainers.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvailableContainers.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableContainers.Location = new System.Drawing.Point(728, 214);
            this.lblAvailableContainers.Name = "lblAvailableContainers";
            this.lblAvailableContainers.Size = new System.Drawing.Size(281, 116);
            this.lblAvailableContainers.TabIndex = 5;
            this.lblAvailableContainers.Text = "Containers Available: 0";
            this.lblAvailableContainers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClerkDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 593);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ClerkDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clerk Dashboard";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlMainContent.ResumeLayout(false);
            this.pnlMainContent.PerformLayout();
            this.pnlInfoBoxes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
    }
}
