using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class AddTransportUnit
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblUnitName;
        private TextBox txtUnitName;

        private Label lblLorry;
        private ComboBox cmbLorry;

        private Label lblDriver;
        private ComboBox cmbDriver;

        private Label lblAssistant;
        private ComboBox cmbAssistant;

        private Label lblContainer;
        private ComboBox cmbContainer;

        private Label lblIsAvailable;
        private ComboBox cmbIsAvailable;  // <-- ComboBox instead of CheckBox

        private Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUnitName = new System.Windows.Forms.Label();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.lblLorry = new System.Windows.Forms.Label();
            this.cmbLorry = new System.Windows.Forms.ComboBox();
            this.lblDriver = new System.Windows.Forms.Label();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.lblAssistant = new System.Windows.Forms.Label();
            this.cmbAssistant = new System.Windows.Forms.ComboBox();
            this.lblContainer = new System.Windows.Forms.Label();
            this.cmbContainer = new System.Windows.Forms.ComboBox();
            this.lblIsAvailable = new System.Windows.Forms.Label();
            this.cmbIsAvailable = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.lblTitle.Text = "Add Transport Unit";
            // 
            // lblUnitName
            // 
            this.lblUnitName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUnitName.Location = new System.Drawing.Point(30, 80);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(120, 25);
            this.lblUnitName.TabIndex = 1;
            this.lblUnitName.Text = "Unit Name:";
            // 
            // txtUnitName
            // 
            this.txtUnitName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUnitName.Location = new System.Drawing.Point(160, 80);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(425, 25);
            this.txtUnitName.TabIndex = 2;
            // 
            // lblLorry
            // 
            this.lblLorry.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLorry.Location = new System.Drawing.Point(30, 130);
            this.lblLorry.Name = "lblLorry";
            this.lblLorry.Size = new System.Drawing.Size(120, 25);
            this.lblLorry.TabIndex = 3;
            this.lblLorry.Text = "Lorry (Reg No):";
            // 
            // cmbLorry
            // 
            this.cmbLorry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLorry.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLorry.Location = new System.Drawing.Point(160, 130);
            this.cmbLorry.Name = "cmbLorry";
            this.cmbLorry.Size = new System.Drawing.Size(425, 25);
            this.cmbLorry.TabIndex = 4;
            // 
            // lblDriver
            // 
            this.lblDriver.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDriver.Location = new System.Drawing.Point(30, 180);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(120, 25);
            this.lblDriver.TabIndex = 5;
            this.lblDriver.Text = "Driver:";
            // 
            // cmbDriver
            // 
            this.cmbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDriver.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDriver.Location = new System.Drawing.Point(160, 180);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(425, 25);
            this.cmbDriver.TabIndex = 6;
            // 
            // lblAssistant
            // 
            this.lblAssistant.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAssistant.Location = new System.Drawing.Point(30, 230);
            this.lblAssistant.Name = "lblAssistant";
            this.lblAssistant.Size = new System.Drawing.Size(120, 25);
            this.lblAssistant.TabIndex = 7;
            this.lblAssistant.Text = "Assistant:";
            // 
            // cmbAssistant
            // 
            this.cmbAssistant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssistant.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAssistant.Location = new System.Drawing.Point(160, 230);
            this.cmbAssistant.Name = "cmbAssistant";
            this.cmbAssistant.Size = new System.Drawing.Size(425, 25);
            this.cmbAssistant.TabIndex = 8;
            // 
            // lblContainer
            // 
            this.lblContainer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContainer.Location = new System.Drawing.Point(30, 280);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(120, 25);
            this.lblContainer.TabIndex = 9;
            this.lblContainer.Text = "Container:";
            // 
            // cmbContainer
            // 
            this.cmbContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContainer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbContainer.Location = new System.Drawing.Point(160, 280);
            this.cmbContainer.Name = "cmbContainer";
            this.cmbContainer.Size = new System.Drawing.Size(425, 25);
            this.cmbContainer.TabIndex = 10;
            // 
            // lblIsAvailable
            // 
            this.lblIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIsAvailable.Location = new System.Drawing.Point(31, 329);
            this.lblIsAvailable.Name = "lblIsAvailable";
            this.lblIsAvailable.Size = new System.Drawing.Size(120, 25);
            this.lblIsAvailable.TabIndex = 11;
            this.lblIsAvailable.Text = "Is Available:";
            // 
            // cmbIsAvailable
            // 
            this.cmbIsAvailable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbIsAvailable.Location = new System.Drawing.Point(160, 329);
            this.cmbIsAvailable.Name = "cmbIsAvailable";
            this.cmbIsAvailable.Size = new System.Drawing.Size(425, 25);
            this.cmbIsAvailable.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(148, 386);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 40);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Add Unit";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(4)))), ((int)(((byte)(45)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Snow;
            this.button1.Location = new System.Drawing.Point(334, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 40);
            this.button1.TabIndex = 18;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddTransportUnit
            // 
            this.ClientSize = new System.Drawing.Size(620, 456);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUnitName);
            this.Controls.Add(this.txtUnitName);
            this.Controls.Add(this.lblLorry);
            this.Controls.Add(this.cmbLorry);
            this.Controls.Add(this.lblDriver);
            this.Controls.Add(this.cmbDriver);
            this.Controls.Add(this.lblAssistant);
            this.Controls.Add(this.cmbAssistant);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.cmbContainer);
            this.Controls.Add(this.lblIsAvailable);
            this.Controls.Add(this.cmbIsAvailable);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTransportUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Transport Unit";
            this.Load += new System.EventHandler(this.AddTransportUnitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button button1;
    }
}
