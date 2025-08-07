using System.Windows.Forms;

namespace E_shift.Forms.Customer
{
    partial class CustomerLoadsControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblHeading;
        private DataGridView dgvLoads;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeading = new System.Windows.Forms.Label();
            this.dgvLoads = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblHeading.Location = new System.Drawing.Point(27, 11);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(333, 42);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "My Load History";
            // 
            // dgvLoads
            // 
            this.dgvLoads.AllowUserToAddRows = false;
            this.dgvLoads.AllowUserToDeleteRows = false;
            this.dgvLoads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoads.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoads.ColumnHeadersHeight = 30;
            this.dgvLoads.Location = new System.Drawing.Point(56, 131);
            this.dgvLoads.Name = "dgvLoads";
            this.dgvLoads.ReadOnly = true;
            this.dgvLoads.Size = new System.Drawing.Size(889, 420);
            this.dgvLoads.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(53, 78);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(757, 40);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "You can only edit or delete jobs and loads that are still \'Pending\' or \'Submitted" +
    "\'.";
            // 
            // CustomerLoadsControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.dgvLoads);
            this.Name = "CustomerLoadsControl";
            this.Size = new System.Drawing.Size(998, 570);
            this.Load += new System.EventHandler(this.CustomerLoadsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lblInfo;
    }
}
