namespace E_shift.Forms.ReportViewers
{
    partial class ContainerReportViewer
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Reporting.WinForms.ReportViewer rptvContainers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSavePdf;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rptvContainers = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSavePdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rptvContainers
            // 
            this.rptvContainers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rptvContainers.Location = new System.Drawing.Point(1, 59);
            this.rptvContainers.Name = "rptvContainers";
            this.rptvContainers.ServerReport.BearerToken = null;
            this.rptvContainers.Size = new System.Drawing.Size(767, 802);
            this.rptvContainers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 34);
            this.label1.TabIndex = 4;
            this.label1.Text = "Container Info";
            // 
            // btnSavePdf
            // 
            this.btnSavePdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePdf.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSavePdf.FlatAppearance.BorderSize = 0;
            this.btnSavePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePdf.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSavePdf.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSavePdf.Location = new System.Drawing.Point(624, 10);
            this.btnSavePdf.Name = "btnSavePdf";
            this.btnSavePdf.Size = new System.Drawing.Size(119, 32);
            this.btnSavePdf.TabIndex = 3;
            this.btnSavePdf.Text = "Save PDF";
            this.btnSavePdf.UseVisualStyleBackColor = false;
            this.btnSavePdf.Click += new System.EventHandler(this.btnSavePdf_Click);
            // 
            // ContainerReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 861);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSavePdf);
            this.Controls.Add(this.rptvContainers);
            this.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.Name = "ContainerReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Container Report";
            this.Load += new System.EventHandler(this.ContainerReportViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
