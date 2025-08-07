using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class SharedDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlSidebar;
        private Button dashBoard;
        private Button btnLogout;
        private Button btnAssignedLoads;

        private Panel pnlMainContent;
        private Label lblWelcome;
        private Label lblTransportUnitInfo;

        private Panel pnlInfoBoxes;

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
            this.btnAssignedLoads = new System.Windows.Forms.Button();
            this.dashBoard = new System.Windows.Forms.Button();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlInfoBoxes = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblThisweek = new System.Windows.Forms.Label();
            this.lblJobsToday = new System.Windows.Forms.Label();
            this.lblJobsThisWeek = new System.Windows.Forms.Label();
            this.lblTransportUnitInfo = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.pnlInfoBoxes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnAssignedLoads);
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
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnAssignedLoads
            // 
            this.btnAssignedLoads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnAssignedLoads.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssignedLoads.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAssignedLoads.FlatAppearance.BorderSize = 0;
            this.btnAssignedLoads.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignedLoads.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAssignedLoads.ForeColor = System.Drawing.Color.White;
            this.btnAssignedLoads.Location = new System.Drawing.Point(0, 50);
            this.btnAssignedLoads.Name = "btnAssignedLoads";
            this.btnAssignedLoads.Size = new System.Drawing.Size(193, 50);
            this.btnAssignedLoads.TabIndex = 2;
            this.btnAssignedLoads.Text = "Assigned Loads";
            this.btnAssignedLoads.UseVisualStyleBackColor = false;
            this.btnAssignedLoads.Click += new System.EventHandler(this.btnAssignedLoads_Click);
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
            this.pnlMainContent.Controls.Add(this.pnlInfoBoxes);
            this.pnlMainContent.Controls.Add(this.lblTransportUnitInfo);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(193, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(937, 593);
            this.pnlMainContent.TabIndex = 1;
            this.pnlMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMainContent_Paint);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblWelcome.Location = new System.Drawing.Point(21, 50);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(585, 59);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, Employee!";
            // 
            // pnlInfoBoxes
            // 
            this.pnlInfoBoxes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlInfoBoxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoBoxes.Controls.Add(this.panel2);
            this.pnlInfoBoxes.Controls.Add(this.panel1);
            this.pnlInfoBoxes.Controls.Add(this.lblToday);
            this.pnlInfoBoxes.Controls.Add(this.lblThisweek);
            this.pnlInfoBoxes.Controls.Add(this.lblJobsToday);
            this.pnlInfoBoxes.Controls.Add(this.lblJobsThisWeek);
            this.pnlInfoBoxes.Location = new System.Drawing.Point(0, 255);
            this.pnlInfoBoxes.Name = "pnlInfoBoxes";
            this.pnlInfoBoxes.Size = new System.Drawing.Size(1056, 347);
            this.pnlInfoBoxes.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel2.Location = new System.Drawing.Point(105, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(43, 179);
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel1.Location = new System.Drawing.Point(582, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(43, 179);
            this.panel1.TabIndex = 4;
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblToday.Location = new System.Drawing.Point(643, 49);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(158, 25);
            this.lblToday.TabIndex = 3;
            this.lblToday.Text = "Loads Today";
            this.lblToday.Click += new System.EventHandler(this.lblToday_Click);
            // 
            // lblThisweek
            // 
            this.lblThisweek.AutoSize = true;
            this.lblThisweek.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThisweek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblThisweek.Location = new System.Drawing.Point(158, 49);
            this.lblThisweek.Name = "lblThisweek";
            this.lblThisweek.Size = new System.Drawing.Size(201, 25);
            this.lblThisweek.TabIndex = 2;
            this.lblThisweek.Text = "Loads this week";
            // 
            // lblJobsToday
            // 
            this.lblJobsToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.lblJobsToday.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobsToday.ForeColor = System.Drawing.Color.Black;
            this.lblJobsToday.Location = new System.Drawing.Point(588, 88);
            this.lblJobsToday.Name = "lblJobsToday";
            this.lblJobsToday.Size = new System.Drawing.Size(265, 179);
            this.lblJobsToday.TabIndex = 0;
            this.lblJobsToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblJobsThisWeek
            // 
            this.lblJobsThisWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.lblJobsThisWeek.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobsThisWeek.ForeColor = System.Drawing.Color.Black;
            this.lblJobsThisWeek.Location = new System.Drawing.Point(110, 88);
            this.lblJobsThisWeek.Name = "lblJobsThisWeek";
            this.lblJobsThisWeek.Size = new System.Drawing.Size(279, 179);
            this.lblJobsThisWeek.TabIndex = 1;
            this.lblJobsThisWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTransportUnitInfo
            // 
            this.lblTransportUnitInfo.AutoSize = true;
            this.lblTransportUnitInfo.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransportUnitInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTransportUnitInfo.Location = new System.Drawing.Point(91, 180);
            this.lblTransportUnitInfo.Name = "lblTransportUnitInfo";
            this.lblTransportUnitInfo.Size = new System.Drawing.Size(190, 25);
            this.lblTransportUnitInfo.TabIndex = 2;
            this.lblTransportUnitInfo.Text = "Transport Unit:";
            // 
            // SharedDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 593);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SharedDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlMainContent.ResumeLayout(false);
            this.pnlMainContent.PerformLayout();
            this.pnlInfoBoxes.ResumeLayout(false);
            this.pnlInfoBoxes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblToday;
        private Label lblThisweek;
        private Label lblJobsToday;
        private Label lblJobsThisWeek;
        private Panel panel2;
        private Panel panel1;
    }
}
