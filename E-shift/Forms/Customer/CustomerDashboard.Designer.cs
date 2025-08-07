using System;
using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms.Customer
{
    partial class CustomerDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlSidebar;
        private Button btnDashboard;
        private Button btnMyLoads;
        private Button btnLogout;
        private Button btnJobs;
        private Panel pnlMainContent;
        private Panel pnlDashboard;

        private Label lblWelcome;

        private Label lblPending;
        private Label lblInProgress;
        private Label lblCompleted;
        private Label lblCancelled;

        private Button btnAddJob;

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
            this.btnMyLoads = new System.Windows.Forms.Button();
            this.btnJobs = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblPending = new System.Windows.Forms.Label();
            this.lblInProgress = new System.Windows.Forms.Label();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.lblCancelled = new System.Windows.Forms.Label();
            this.btnAddJob = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlSidebar.Controls.Add(this.btnMyLoads);
            this.pnlSidebar.Controls.Add(this.btnJobs);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(193, 600);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnMyLoads
            // 
            this.btnMyLoads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnMyLoads.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMyLoads.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMyLoads.FlatAppearance.BorderSize = 0;
            this.btnMyLoads.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyLoads.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMyLoads.ForeColor = System.Drawing.Color.White;
            this.btnMyLoads.Location = new System.Drawing.Point(0, 100);
            this.btnMyLoads.Name = "btnMyLoads";
            this.btnMyLoads.Size = new System.Drawing.Size(193, 50);
            this.btnMyLoads.TabIndex = 1;
            this.btnMyLoads.Text = "My Loads";
            this.btnMyLoads.UseVisualStyleBackColor = false;
            this.btnMyLoads.Click += new System.EventHandler(this.btnMyLoads_Click_1);
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
            this.btnJobs.Location = new System.Drawing.Point(0, 50);
            this.btnJobs.Name = "btnJobs";
            this.btnJobs.Size = new System.Drawing.Size(193, 50);
            this.btnJobs.TabIndex = 2;
            this.btnJobs.Text = "Job Requests";
            this.btnJobs.UseVisualStyleBackColor = false;
            this.btnJobs.Click += new System.EventHandler(this.btnJobs_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(193, 50);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
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
            this.btnLogout.Location = new System.Drawing.Point(0, 550);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(193, 50);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlMainContent.Controls.Add(this.pnlDashboard);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(193, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(900, 600);
            this.pnlMainContent.TabIndex = 1;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Controls.Add(this.lblInfo);
            this.pnlDashboard.Controls.Add(this.panel4);
            this.pnlDashboard.Controls.Add(this.panel2);
            this.pnlDashboard.Controls.Add(this.panel1);
            this.pnlDashboard.Controls.Add(this.panel3);
            this.pnlDashboard.Controls.Add(this.lblWelcome);
            this.pnlDashboard.Controls.Add(this.lblPending);
            this.pnlDashboard.Controls.Add(this.lblInProgress);
            this.pnlDashboard.Controls.Add(this.lblCompleted);
            this.pnlDashboard.Controls.Add(this.lblCancelled);
            this.pnlDashboard.Controls.Add(this.btnAddJob);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(900, 600);
            this.pnlDashboard.TabIndex = 0;
            this.pnlDashboard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDashboard_Paint_1);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel4.Location = new System.Drawing.Point(521, 410);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(34, 137);
            this.panel4.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel2.Location = new System.Drawing.Point(511, 236);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(34, 137);
            this.panel2.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Location = new System.Drawing.Point(86, 410);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(34, 137);
            this.panel1.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel3.Location = new System.Drawing.Point(86, 236);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(34, 137);
            this.panel3.TabIndex = 9;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Verdana", 28F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblWelcome.Location = new System.Drawing.Point(25, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(461, 46);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, Customer!";
            // 
            // lblPending
            // 
            this.lblPending.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblPending.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPending.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPending.ForeColor = System.Drawing.Color.Black;
            this.lblPending.Location = new System.Drawing.Point(86, 239);
            this.lblPending.Name = "lblPending";
            this.lblPending.Padding = new System.Windows.Forms.Padding(5);
            this.lblPending.Size = new System.Drawing.Size(287, 134);
            this.lblPending.TabIndex = 2;
            this.lblPending.Text = "Pending: 0";
            this.lblPending.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInProgress
            // 
            this.lblInProgress.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblInProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInProgress.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblInProgress.ForeColor = System.Drawing.Color.Black;
            this.lblInProgress.Location = new System.Drawing.Point(86, 410);
            this.lblInProgress.Name = "lblInProgress";
            this.lblInProgress.Padding = new System.Windows.Forms.Padding(5);
            this.lblInProgress.Size = new System.Drawing.Size(287, 137);
            this.lblInProgress.TabIndex = 3;
            this.lblInProgress.Text = "In Progress: 0";
            this.lblInProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompleted
            // 
            this.lblCompleted.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCompleted.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCompleted.ForeColor = System.Drawing.Color.Black;
            this.lblCompleted.Location = new System.Drawing.Point(511, 236);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Padding = new System.Windows.Forms.Padding(5);
            this.lblCompleted.Size = new System.Drawing.Size(287, 137);
            this.lblCompleted.TabIndex = 4;
            this.lblCompleted.Text = "Completed: 0";
            this.lblCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCancelled
            // 
            this.lblCancelled.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCancelled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCancelled.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCancelled.ForeColor = System.Drawing.Color.Black;
            this.lblCancelled.Location = new System.Drawing.Point(521, 410);
            this.lblCancelled.Name = "lblCancelled";
            this.lblCancelled.Padding = new System.Windows.Forms.Padding(5);
            this.lblCancelled.Size = new System.Drawing.Size(287, 137);
            this.lblCancelled.TabIndex = 5;
            this.lblCancelled.Text = "Cancelled: 0";
            this.lblCancelled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddJob
            // 
            this.btnAddJob.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAddJob.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddJob.FlatAppearance.BorderSize = 0;
            this.btnAddJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddJob.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddJob.ForeColor = System.Drawing.Color.White;
            this.btnAddJob.Location = new System.Drawing.Point(699, 36);
            this.btnAddJob.Name = "btnAddJob";
            this.btnAddJob.Size = new System.Drawing.Size(175, 49);
            this.btnAddJob.TabIndex = 6;
            this.btnAddJob.Text = "Add Transport Request";
            this.btnAddJob.UseVisualStyleBackColor = false;
            this.btnAddJob.Click += new System.EventHandler(this.btnAddJob_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblInfo.Location = new System.Drawing.Point(246, 163);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(437, 51);
            this.lblInfo.TabIndex = 13;
            this.lblInfo.Text = "Loads Info : ";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // CustomerDashboard
            // 
            this.ClientSize = new System.Drawing.Size(1093, 600);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "CustomerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Dashboard";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlMainContent.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private Panel panel4;
        private Panel panel2;
        private Panel panel1;
        private Panel panel3;
        private Label lblInfo;
    }
}
