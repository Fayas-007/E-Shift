using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class EmployeeDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlSidebar;
        private Button dashBoard;
        private Button btnCustomers;
        private Button btnJobs;
        private Button BtnLoads;
        private Button btnTransportUnits;
        private Button btnUsers;

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
        private Label lblLoadsToday;
        private Label lblPendingLoads;
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
            this.btnUsers = new System.Windows.Forms.Button();
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
            this.lblLoadsToday = new System.Windows.Forms.Label();
            this.lblPendingLoads = new System.Windows.Forms.Label();
            this.lblAvailableDrivers = new System.Windows.Forms.Label();
            this.lblAvailableLorries = new System.Windows.Forms.Label();
            this.lblAvailableAssistants = new System.Windows.Forms.Label();
            this.lblAvailableContainers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.pnlSidebar.Controls.Add(this.btnUsers);
            this.pnlSidebar.Controls.Add(this.dashBoard);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(193, 593);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
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
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click_1);
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
            this.btnContainer.Location = new System.Drawing.Point(0, 350);
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
            this.btnVehicle.Location = new System.Drawing.Point(0, 300);
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
            this.btnTransportUnits.Location = new System.Drawing.Point(0, 250);
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
            this.BtnLoads.Location = new System.Drawing.Point(0, 200);
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
            this.btnJobs.Location = new System.Drawing.Point(0, 150);
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
            this.btnCustomers.Location = new System.Drawing.Point(0, 100);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(193, 50);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.BtnCustomers_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUsers.ForeColor = System.Drawing.Color.White;
            this.btnUsers.Location = new System.Drawing.Point(0, 50);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(193, 50);
            this.btnUsers.TabIndex = 8;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Visible = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
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
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlMainContent.Controls.Add(this.lblWelcome);
            this.pnlMainContent.Controls.Add(this.btnAddCustomer);
            this.pnlMainContent.Controls.Add(this.btnCreateJob);
            this.pnlMainContent.Controls.Add(this.btnAddLoad);
            this.pnlMainContent.Controls.Add(this.btnAddTransportUnit);
            this.pnlMainContent.Controls.Add(this.pnlInfoBoxes);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(193, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1060, 593);
            this.pnlMainContent.TabIndex = 1;
            this.pnlMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMainContent_Paint_1);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblWelcome.Location = new System.Drawing.Point(3, 19);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(454, 45);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, Employee!";
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCustomer.FlatAppearance.BorderSize = 0;
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Location = new System.Drawing.Point(74, 115);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(189, 59);
            this.btnAddCustomer.TabIndex = 1;
            this.btnAddCustomer.Text = "Add New Customer";
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            // 
            // btnCreateJob
            // 
            this.btnCreateJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCreateJob.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateJob.FlatAppearance.BorderSize = 0;
            this.btnCreateJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateJob.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnCreateJob.ForeColor = System.Drawing.Color.White;
            this.btnCreateJob.Location = new System.Drawing.Point(316, 115);
            this.btnCreateJob.Name = "btnCreateJob";
            this.btnCreateJob.Size = new System.Drawing.Size(189, 59);
            this.btnCreateJob.TabIndex = 2;
            this.btnCreateJob.Text = "Create New Job";
            this.btnCreateJob.UseVisualStyleBackColor = false;
            // 
            // btnAddLoad
            // 
            this.btnAddLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnAddLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddLoad.FlatAppearance.BorderSize = 0;
            this.btnAddLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLoad.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddLoad.ForeColor = System.Drawing.Color.White;
            this.btnAddLoad.Location = new System.Drawing.Point(557, 115);
            this.btnAddLoad.Name = "btnAddLoad";
            this.btnAddLoad.Size = new System.Drawing.Size(189, 59);
            this.btnAddLoad.TabIndex = 3;
            this.btnAddLoad.Text = "Add New Load";
            this.btnAddLoad.UseVisualStyleBackColor = false;
            this.btnAddLoad.Click += new System.EventHandler(this.btnAddLoad_Click_1);
            // 
            // btnAddTransportUnit
            // 
            this.btnAddTransportUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnAddTransportUnit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTransportUnit.FlatAppearance.BorderSize = 0;
            this.btnAddTransportUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTransportUnit.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddTransportUnit.ForeColor = System.Drawing.Color.White;
            this.btnAddTransportUnit.Location = new System.Drawing.Point(796, 115);
            this.btnAddTransportUnit.Name = "btnAddTransportUnit";
            this.btnAddTransportUnit.Size = new System.Drawing.Size(189, 59);
            this.btnAddTransportUnit.TabIndex = 4;
            this.btnAddTransportUnit.Text = "Add Transport Unit";
            this.btnAddTransportUnit.UseVisualStyleBackColor = false;
            this.btnAddTransportUnit.Click += new System.EventHandler(this.btnAddTransportUnit_Click_1);
            // 
            // pnlInfoBoxes
            // 
            this.pnlInfoBoxes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlInfoBoxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoBoxes.Controls.Add(this.label2);
            this.pnlInfoBoxes.Controls.Add(this.label1);
            this.pnlInfoBoxes.Controls.Add(this.panel6);
            this.pnlInfoBoxes.Controls.Add(this.panel5);
            this.pnlInfoBoxes.Controls.Add(this.panel4);
            this.pnlInfoBoxes.Controls.Add(this.panel3);
            this.pnlInfoBoxes.Controls.Add(this.panel2);
            this.pnlInfoBoxes.Controls.Add(this.panel1);
            this.pnlInfoBoxes.Controls.Add(this.lblLoadsToday);
            this.pnlInfoBoxes.Controls.Add(this.lblPendingLoads);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableDrivers);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableLorries);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableAssistants);
            this.pnlInfoBoxes.Controls.Add(this.lblAvailableContainers);
            this.pnlInfoBoxes.Location = new System.Drawing.Point(2, 228);
            this.pnlInfoBoxes.Name = "pnlInfoBoxes";
            this.pnlInfoBoxes.Size = new System.Drawing.Size(1140, 374);
            this.pnlInfoBoxes.TabIndex = 5;
            this.pnlInfoBoxes.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInfoBoxes_Paint);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel6.Location = new System.Drawing.Point(736, 237);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(33, 116);
            this.panel6.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel5.Location = new System.Drawing.Point(736, 67);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(33, 116);
            this.panel5.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel4.Location = new System.Drawing.Point(391, 237);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(34, 116);
            this.panel4.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel3.Location = new System.Drawing.Point(391, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(34, 116);
            this.panel3.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel2.Location = new System.Drawing.Point(49, 237);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(32, 116);
            this.panel2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Location = new System.Drawing.Point(49, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(32, 116);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblLoadsToday
            // 
            this.lblLoadsToday.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblLoadsToday.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoadsToday.ForeColor = System.Drawing.Color.Black;
            this.lblLoadsToday.Location = new System.Drawing.Point(49, 67);
            this.lblLoadsToday.Name = "lblLoadsToday";
            this.lblLoadsToday.Size = new System.Drawing.Size(269, 116);
            this.lblLoadsToday.TabIndex = 0;
            this.lblLoadsToday.Text = "Jobs Today: 0";
            this.lblLoadsToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoadsToday.Click += new System.EventHandler(this.lblJobsToday_Click);
            // 
            // lblPendingLoads
            // 
            this.lblPendingLoads.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblPendingLoads.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblPendingLoads.ForeColor = System.Drawing.Color.Black;
            this.lblPendingLoads.Location = new System.Drawing.Point(49, 237);
            this.lblPendingLoads.Name = "lblPendingLoads";
            this.lblPendingLoads.Size = new System.Drawing.Size(269, 116);
            this.lblPendingLoads.TabIndex = 1;
            this.lblPendingLoads.Text = "Jobs This Week: 0";
            this.lblPendingLoads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvailableDrivers
            // 
            this.lblAvailableDrivers.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAvailableDrivers.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvailableDrivers.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableDrivers.Location = new System.Drawing.Point(391, 67);
            this.lblAvailableDrivers.Name = "lblAvailableDrivers";
            this.lblAvailableDrivers.Size = new System.Drawing.Size(287, 116);
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
            this.lblAvailableLorries.Location = new System.Drawing.Point(736, 67);
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
            this.lblAvailableAssistants.Location = new System.Drawing.Point(391, 237);
            this.lblAvailableAssistants.Name = "lblAvailableAssistants";
            this.lblAvailableAssistants.Size = new System.Drawing.Size(287, 116);
            this.lblAvailableAssistants.TabIndex = 4;
            this.lblAvailableAssistants.Text = "Assistants Available: 0";
            this.lblAvailableAssistants.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvailableContainers
            // 
            this.lblAvailableContainers.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblAvailableContainers.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvailableContainers.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableContainers.Location = new System.Drawing.Point(736, 237);
            this.lblAvailableContainers.Name = "lblAvailableContainers";
            this.lblAvailableContainers.Size = new System.Drawing.Size(281, 116);
            this.lblAvailableContainers.TabIndex = 5;
            this.lblAvailableContainers.Text = "Containers Available: 0";
            this.lblAvailableContainers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(346, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 372);
            this.label1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label2.Location = new System.Drawing.Point(516, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(442, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Resources Not Yet Assigned to a Transport Unit";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // EmployeeDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 593);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EmployeeDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Dashboard";
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
        private Label label1;
        private Label label2;
    }
}
