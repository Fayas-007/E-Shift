namespace E_shift.Forms
{
    partial class EditTransportUnit
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLorry;
        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.Label lblAssistant;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.Label lblUnitName;
        private System.Windows.Forms.Label lblIsAvailable;
        private System.Windows.Forms.Label lblAssignmentWarning;
        private System.Windows.Forms.ComboBox cmbLorry;
        private System.Windows.Forms.ComboBox cmbDriver;
        private System.Windows.Forms.ComboBox cmbAssistant;
        private System.Windows.Forms.ComboBox cmbContainer;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.ComboBox cmbIsAvailable;
        private System.Windows.Forms.Button btnSave;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLorry = new System.Windows.Forms.Label();
            this.lblDriver = new System.Windows.Forms.Label();
            this.cmbIsAvailable = new System.Windows.Forms.ComboBox();
            this.lblAssistant = new System.Windows.Forms.Label();
            this.lblContainer = new System.Windows.Forms.Label();
            this.lblUnitName = new System.Windows.Forms.Label();
            this.lblIsAvailable = new System.Windows.Forms.Label();
            this.cmbLorry = new System.Windows.Forms.ComboBox();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.cmbAssistant = new System.Windows.Forms.ComboBox();
            this.cmbContainer = new System.Windows.Forms.ComboBox();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblAssignmentWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Edit Transport Unit";
            // 
            // lblLorry
            // 
            this.lblLorry.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLorry.Location = new System.Drawing.Point(28, 90);
            this.lblLorry.Name = "lblLorry";
            this.lblLorry.Size = new System.Drawing.Size(100, 25);
            this.lblLorry.TabIndex = 1;
            this.lblLorry.Text = "Lorry:";
            // 
            // lblDriver
            // 
            this.lblDriver.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDriver.Location = new System.Drawing.Point(28, 145);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(100, 25);
            this.lblDriver.TabIndex = 3;
            this.lblDriver.Text = "Driver:";
            // 
            // cmbIsAvailable
            // 
            this.cmbIsAvailable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbIsAvailable.FormattingEnabled = true;
            this.cmbIsAvailable.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbIsAvailable.Location = new System.Drawing.Point(143, 352);
            this.cmbIsAvailable.Name = "cmbIsAvailable";
            this.cmbIsAvailable.Size = new System.Drawing.Size(300, 25);
            this.cmbIsAvailable.TabIndex = 12;
            // 
            // lblAssistant
            // 
            this.lblAssistant.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAssistant.Location = new System.Drawing.Point(28, 202);
            this.lblAssistant.Name = "lblAssistant";
            this.lblAssistant.Size = new System.Drawing.Size(100, 25);
            this.lblAssistant.TabIndex = 5;
            this.lblAssistant.Text = "Assistant:";
            // 
            // lblContainer
            // 
            this.lblContainer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContainer.Location = new System.Drawing.Point(28, 251);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(100, 25);
            this.lblContainer.TabIndex = 7;
            this.lblContainer.Text = "Container:";
            // 
            // lblUnitName
            // 
            this.lblUnitName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUnitName.Location = new System.Drawing.Point(26, 300);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(100, 25);
            this.lblUnitName.TabIndex = 9;
            this.lblUnitName.Text = "Unit Name:";
            // 
            // lblIsAvailable
            // 
            this.lblIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIsAvailable.Location = new System.Drawing.Point(28, 355);
            this.lblIsAvailable.Name = "lblIsAvailable";
            this.lblIsAvailable.Size = new System.Drawing.Size(100, 25);
            this.lblIsAvailable.TabIndex = 11;
            this.lblIsAvailable.Text = "Is Available:";
            // 
            // cmbLorry
            // 
            this.cmbLorry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLorry.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLorry.Location = new System.Drawing.Point(143, 90);
            this.cmbLorry.Name = "cmbLorry";
            this.cmbLorry.Size = new System.Drawing.Size(300, 25);
            this.cmbLorry.TabIndex = 2;
            // 
            // cmbDriver
            // 
            this.cmbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDriver.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDriver.Location = new System.Drawing.Point(143, 145);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(300, 25);
            this.cmbDriver.TabIndex = 4;
            // 
            // cmbAssistant
            // 
            this.cmbAssistant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssistant.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAssistant.Location = new System.Drawing.Point(143, 202);
            this.cmbAssistant.Name = "cmbAssistant";
            this.cmbAssistant.Size = new System.Drawing.Size(300, 25);
            this.cmbAssistant.TabIndex = 6;
            // 
            // cmbContainer
            // 
            this.cmbContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContainer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbContainer.Location = new System.Drawing.Point(143, 251);
            this.cmbContainer.Name = "cmbContainer";
            this.cmbContainer.Size = new System.Drawing.Size(300, 25);
            this.cmbContainer.TabIndex = 8;
            // 
            // txtUnitName
            // 
            this.txtUnitName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUnitName.Location = new System.Drawing.Point(143, 300);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(300, 25);
            this.txtUnitName.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(98, 445);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(4)))), ((int)(((byte)(45)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Snow;
            this.button2.Location = new System.Drawing.Point(248, 444);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 40);
            this.button2.TabIndex = 18;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblAssignmentWarning
            // 
            this.lblAssignmentWarning.AutoSize = true;
            this.lblAssignmentWarning.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblAssignmentWarning.ForeColor = System.Drawing.Color.DarkRed;
            this.lblAssignmentWarning.Location = new System.Drawing.Point(26, 396);
            this.lblAssignmentWarning.Name = "lblAssignmentWarning";
            this.lblAssignmentWarning.Size = new System.Drawing.Size(456, 17);
            this.lblAssignmentWarning.TabIndex = 10;
            this.lblAssignmentWarning.Text = "This Transport Unit is assigned to a job, availability cannot be changed.";
            this.lblAssignmentWarning.Visible = false;
            this.lblAssignmentWarning.Click += new System.EventHandler(this.lblAssignmentWarning_Click);
            // 
            // EditTransportUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 499);
            this.Controls.Add(this.lblAssignmentWarning);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblLorry);
            this.Controls.Add(this.cmbLorry);
            this.Controls.Add(this.cmbIsAvailable);
            this.Controls.Add(this.lblDriver);
            this.Controls.Add(this.cmbDriver);
            this.Controls.Add(this.lblAssistant);
            this.Controls.Add(this.cmbAssistant);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.cmbContainer);
            this.Controls.Add(this.lblUnitName);
            this.Controls.Add(this.txtUnitName);
            this.Controls.Add(this.lblIsAvailable);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTransportUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Transport Unit";
            this.Load += new System.EventHandler(this.EditTransportUnit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
    }
}
