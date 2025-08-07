using System;
using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class EditLoadForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblJob;
        private Label lblWeight;
        private Label lblStatus;
        private Label lblStartDate;
        private Label lblEndDate;
        private Label lblStartLocation;
        private Label lblEndLocation;
        private TextBox txtStartLocation;
        private TextBox txtEndLocation;
        private Label lblTransportUnit;
        private ComboBox cmbTransportUnit;
        private ComboBox cmbJob;
        private ComboBox cmbStatus;
        private TextBox txtWeight;

        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;

        private Button btnSave;
        private Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblJob = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStartLocation = new System.Windows.Forms.Label();
            this.txtStartLocation = new System.Windows.Forms.TextBox();
            this.lblEndLocation = new System.Windows.Forms.Label();
            this.txtEndLocation = new System.Windows.Forms.TextBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.cmbJob = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTransportUnit = new System.Windows.Forms.Label();
            this.cmbTransportUnit = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitle.Location = new System.Drawing.Point(32, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Edit Load";
            // 
            // lblJob
            // 
            this.lblJob.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblJob.Location = new System.Drawing.Point(33, 95);
            this.lblJob.Name = "lblJob";
            this.lblJob.Size = new System.Drawing.Size(100, 25);
            this.lblJob.TabIndex = 1;
            this.lblJob.Text = "Job:";
            // 
            // lblWeight
            // 
            this.lblWeight.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWeight.Location = new System.Drawing.Point(33, 149);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(100, 25);
            this.lblWeight.TabIndex = 3;
            this.lblWeight.Text = "Weight (kg):";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(33, 293);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 25);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Status:";
            // 
            // lblStartLocation
            // 
            this.lblStartLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStartLocation.Location = new System.Drawing.Point(33, 199);
            this.lblStartLocation.Name = "lblStartLocation";
            this.lblStartLocation.Size = new System.Drawing.Size(100, 25);
            this.lblStartLocation.TabIndex = 5;
            this.lblStartLocation.Text = "Start Location:";
            // 
            // txtStartLocation
            // 
            this.txtStartLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStartLocation.Location = new System.Drawing.Point(153, 199);
            this.txtStartLocation.Name = "txtStartLocation";
            this.txtStartLocation.Size = new System.Drawing.Size(321, 25);
            this.txtStartLocation.TabIndex = 6;
            // 
            // lblEndLocation
            // 
            this.lblEndLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEndLocation.Location = new System.Drawing.Point(33, 246);
            this.lblEndLocation.Name = "lblEndLocation";
            this.lblEndLocation.Size = new System.Drawing.Size(100, 25);
            this.lblEndLocation.TabIndex = 7;
            this.lblEndLocation.Text = "End Location:";
            // 
            // txtEndLocation
            // 
            this.txtEndLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEndLocation.Location = new System.Drawing.Point(153, 246);
            this.txtEndLocation.Name = "txtEndLocation";
            this.txtEndLocation.Size = new System.Drawing.Size(321, 25);
            this.txtEndLocation.TabIndex = 8;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStartDate.Location = new System.Drawing.Point(33, 340);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(100, 25);
            this.lblStartDate.TabIndex = 11;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEndDate.Location = new System.Drawing.Point(33, 387);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(100, 25);
            this.lblEndDate.TabIndex = 13;
            this.lblEndDate.Text = "End Date:";
            // 
            // cmbJob
            // 
            this.cmbJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJob.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbJob.Location = new System.Drawing.Point(153, 95);
            this.cmbJob.Name = "cmbJob";
            this.cmbJob.Size = new System.Drawing.Size(321, 25);
            this.cmbJob.TabIndex = 2;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.Location = new System.Drawing.Point(153, 293);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(321, 25);
            this.cmbStatus.TabIndex = 10;
            // 
            // txtWeight
            // 
            this.txtWeight.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWeight.Location = new System.Drawing.Point(153, 149);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(321, 25);
            this.txtWeight.TabIndex = 4;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(153, 340);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(321, 25);
            this.dtpStartDate.TabIndex = 12;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(153, 387);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(321, 25);
            this.dtpEndDate.TabIndex = 14;
            // 
            // lblTransportUnit
            // 
            this.lblTransportUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransportUnit.Location = new System.Drawing.Point(33, 434);
            this.lblTransportUnit.Name = "lblTransportUnit";
            this.lblTransportUnit.Size = new System.Drawing.Size(110, 25);
            this.lblTransportUnit.TabIndex = 15;
            this.lblTransportUnit.Text = "Transport Unit:";
            // 
            // cmbTransportUnit
            // 
            this.cmbTransportUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransportUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTransportUnit.Location = new System.Drawing.Point(153, 434);
            this.cmbTransportUnit.Name = "cmbTransportUnit";
            this.cmbTransportUnit.Size = new System.Drawing.Size(321, 25);
            this.cmbTransportUnit.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(116, 508);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 40);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Update Load";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(4)))), ((int)(((byte)(45)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.Snow;
            this.buttonCancel.Location = new System.Drawing.Point(290, 508);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(142, 40);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // EditLoadForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(529, 568);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblJob);
            this.Controls.Add(this.cmbJob);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.lblStartLocation);
            this.Controls.Add(this.txtStartLocation);
            this.Controls.Add(this.lblEndLocation);
            this.Controls.Add(this.txtEndLocation);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblTransportUnit);
            this.Controls.Add(this.cmbTransportUnit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditLoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Load";
            this.Load += new System.EventHandler(this.EditLoadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
