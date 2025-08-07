using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class AddContainerForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblContainerNumber;
        private Label lblDescription;
        private Label lblCapacity;
        private Label lblIsAvailable;

        private TextBox txtContainerNumber;
        private TextBox txtDescription;
        private TextBox txtCapacity;
        private ComboBox cmbIsAvailable;

        private Button btnAddContainer;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblContainerNumber = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblIsAvailable = new System.Windows.Forms.Label();
            this.txtContainerNumber = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.cmbIsAvailable = new System.Windows.Forms.ComboBox();
            this.btnAddContainer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.lblTitle.Text = "Add Container";
            // 
            // lblContainerNumber
            // 
            this.lblContainerNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContainerNumber.Location = new System.Drawing.Point(30, 80);
            this.lblContainerNumber.Name = "lblContainerNumber";
            this.lblContainerNumber.Size = new System.Drawing.Size(120, 25);
            this.lblContainerNumber.TabIndex = 1;
            this.lblContainerNumber.Text = "Container No:";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Location = new System.Drawing.Point(30, 135);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(120, 25);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";
            // 
            // lblCapacity
            // 
            this.lblCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCapacity.Location = new System.Drawing.Point(30, 250);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(120, 25);
            this.lblCapacity.TabIndex = 5;
            this.lblCapacity.Text = "Capacity:";
            // 
            // lblIsAvailable
            // 
            this.lblIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIsAvailable.Location = new System.Drawing.Point(31, 306);
            this.lblIsAvailable.Name = "lblIsAvailable";
            this.lblIsAvailable.Size = new System.Drawing.Size(120, 25);
            this.lblIsAvailable.TabIndex = 7;
            this.lblIsAvailable.Text = "Is Available:";
            // 
            // txtContainerNumber
            // 
            this.txtContainerNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContainerNumber.Location = new System.Drawing.Point(160, 80);
            this.txtContainerNumber.Name = "txtContainerNumber";
            this.txtContainerNumber.Size = new System.Drawing.Size(350, 25);
            this.txtContainerNumber.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(160, 132);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(350, 92);
            this.txtDescription.TabIndex = 4;
            // 
            // txtCapacity
            // 
            this.txtCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCapacity.Location = new System.Drawing.Point(160, 250);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(350, 25);
            this.txtCapacity.TabIndex = 6;
            // 
            // cmbIsAvailable
            // 
            this.cmbIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbIsAvailable.Location = new System.Drawing.Point(160, 303);
            this.cmbIsAvailable.Name = "cmbIsAvailable";
            this.cmbIsAvailable.Size = new System.Drawing.Size(350, 25);
            this.cmbIsAvailable.TabIndex = 8;
            // 
            // btnAddContainer
            // 
            this.btnAddContainer.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAddContainer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddContainer.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnAddContainer.ForeColor = System.Drawing.Color.White;
            this.btnAddContainer.Location = new System.Drawing.Point(127, 358);
            this.btnAddContainer.Name = "btnAddContainer";
            this.btnAddContainer.Size = new System.Drawing.Size(144, 40);
            this.btnAddContainer.TabIndex = 9;
            this.btnAddContainer.Text = "Add Container";
            this.btnAddContainer.UseVisualStyleBackColor = false;
            this.btnAddContainer.Click += new System.EventHandler(this.BtnAddContainer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(4)))), ((int)(((byte)(45)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Snow;
            this.btnCancel.Location = new System.Drawing.Point(307, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(144, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddContainerForm
            // 
            this.ClientSize = new System.Drawing.Size(560, 426);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddContainer);
            this.Controls.Add(this.cmbIsAvailable);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.lblIsAvailable);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtContainerNumber);
            this.Controls.Add(this.lblContainerNumber);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "AddContainerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Container";
            this.Load += new System.EventHandler(this.AddContainerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
