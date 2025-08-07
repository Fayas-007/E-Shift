using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class EditContainerForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblContainerNo;
        private Label lblDescription;
        private Label lblCapacity;
        private Label lblIsAvailable;

        private TextBox txtContainerNo;
        private TextBox txtDescription;
        private TextBox txtCapacity;
        private ComboBox cmbIsAvailable;
        private Label lblAssignmentWarning;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblContainerNo = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblIsAvailable = new System.Windows.Forms.Label();
            this.txtContainerNo = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.cmbIsAvailable = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAssignmentWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Edit Container";
            // 
            // lblContainerNo
            // 
            this.lblContainerNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContainerNo.Location = new System.Drawing.Point(31, 89);
            this.lblContainerNo.Name = "lblContainerNo";
            this.lblContainerNo.Size = new System.Drawing.Size(120, 25);
            this.lblContainerNo.TabIndex = 1;
            this.lblContainerNo.Text = "Container No:";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Location = new System.Drawing.Point(31, 139);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(120, 25);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";
            // 
            // lblCapacity
            // 
            this.lblCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCapacity.Location = new System.Drawing.Point(31, 244);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(120, 25);
            this.lblCapacity.TabIndex = 5;
            this.lblCapacity.Text = "Capacity:";
            // 
            // lblIsAvailable
            // 
            this.lblIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIsAvailable.Location = new System.Drawing.Point(31, 294);
            this.lblIsAvailable.Name = "lblIsAvailable";
            this.lblIsAvailable.Size = new System.Drawing.Size(120, 25);
            this.lblIsAvailable.TabIndex = 7;
            this.lblIsAvailable.Text = "Is Available:";
            // 
            // txtContainerNo
            // 
            this.txtContainerNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContainerNo.Location = new System.Drawing.Point(161, 89);
            this.txtContainerNo.Name = "txtContainerNo";
            this.txtContainerNo.Size = new System.Drawing.Size(328, 25);
            this.txtContainerNo.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(161, 139);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(328, 86);
            this.txtDescription.TabIndex = 4;
            // 
            // txtCapacity
            // 
            this.txtCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCapacity.Location = new System.Drawing.Point(161, 244);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(328, 25);
            this.txtCapacity.TabIndex = 6;
            // 
            // cmbIsAvailable
            // 
            this.cmbIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbIsAvailable.Location = new System.Drawing.Point(161, 294);
            this.cmbIsAvailable.Name = "cmbIsAvailable";
            this.cmbIsAvailable.Size = new System.Drawing.Size(328, 25);
            this.cmbIsAvailable.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(92, 370);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(144, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(4)))), ((int)(((byte)(45)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Snow;
            this.btnCancel.Location = new System.Drawing.Point(277, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(144, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblAssignmentWarning
            // 
            this.lblAssignmentWarning.AutoSize = true;
            this.lblAssignmentWarning.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignmentWarning.ForeColor = System.Drawing.Color.DarkRed;
            this.lblAssignmentWarning.Location = new System.Drawing.Point(160, 331);
            this.lblAssignmentWarning.Name = "lblAssignmentWarning";
            this.lblAssignmentWarning.Size = new System.Drawing.Size(309, 17);
            this.lblAssignmentWarning.TabIndex = 10;
            this.lblAssignmentWarning.Text = "Assigned to job — availability can\'t be changed.";
            this.lblAssignmentWarning.Visible = false;
            // 
            // EditContainerForm
            // 
            this.ClientSize = new System.Drawing.Size(525, 422);
            this.Controls.Add(this.lblAssignmentWarning);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbIsAvailable);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtContainerNo);
            this.Controls.Add(this.lblIsAvailable);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblContainerNo);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditContainerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Container";
            this.Load += new System.EventHandler(this.EditContainerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
