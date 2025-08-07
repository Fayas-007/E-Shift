namespace E_shift.Forms.ReportViewers
{
    partial class TransportUnitReportViewer
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Reporting.WinForms.ReportViewer rptvUnits;
        private System.Windows.Forms.Button btnSavePdf;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rptvUnits = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnSavePdf = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rptvUnits
            // 
            this.rptvUnits.Location = new System.Drawing.Point(0, 89);
            this.rptvUnits.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rptvUnits.Name = "rptvUnits";
            this.rptvUnits.ServerReport.BearerToken = null;
            this.rptvUnits.Size = new System.Drawing.Size(762, 772);
            this.rptvUnits.TabIndex = 0;
            // 
            // btnSavePdf
            // 
            this.btnSavePdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePdf.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSavePdf.FlatAppearance.BorderSize = 0;
            this.btnSavePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePdf.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePdf.ForeColor = System.Drawing.Color.White;
            this.btnSavePdf.Location = new System.Drawing.Point(654, 22);
            this.btnSavePdf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSavePdf.Name = "btnSavePdf";
            this.btnSavePdf.Size = new System.Drawing.Size(98, 28);
            this.btnSavePdf.TabIndex = 1;
            this.btnSavePdf.Text = "Save PDF";
            this.btnSavePdf.UseVisualStyleBackColor = false;
            this.btnSavePdf.Click += new System.EventHandler(this.btnSavePdf_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTitle.Location = new System.Drawing.Point(11, 16);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(322, 34);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Transport Unit Info";
            // 
            // TransportUnitReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 861);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSavePdf);
            this.Controls.Add(this.rptvUnits);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TransportUnitReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transport Unit Report";
            this.Load += new System.EventHandler(this.TransportUnitReportViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
