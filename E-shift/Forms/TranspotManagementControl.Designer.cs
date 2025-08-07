using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class TransportUnitManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvTransportUnits;
        private System.Windows.Forms.Button btnAddTransportUnit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbSearchBy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvTransportUnits = new System.Windows.Forms.DataGridView();
            this.btnAddTransportUnit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransportUnits)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTransportUnits
            // 
            this.dgvTransportUnits.AllowUserToAddRows = false;
            this.dgvTransportUnits.AllowUserToDeleteRows = false;
            this.dgvTransportUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransportUnits.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransportUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransportUnits.Location = new System.Drawing.Point(33, 147);
            this.dgvTransportUnits.Name = "dgvTransportUnits";
            this.dgvTransportUnits.ReadOnly = true;
            this.dgvTransportUnits.RowTemplate.Height = 28;
            this.dgvTransportUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransportUnits.Size = new System.Drawing.Size(971, 405);
            this.dgvTransportUnits.TabIndex = 0;
            this.dgvTransportUnits.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransportUnits_CellContentClick);
            // 
            // btnAddTransportUnit
            // 
            this.btnAddTransportUnit.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAddTransportUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTransportUnit.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTransportUnit.ForeColor = System.Drawing.Color.White;
            this.btnAddTransportUnit.Location = new System.Drawing.Point(806, 82);
            this.btnAddTransportUnit.Name = "btnAddTransportUnit";
            this.btnAddTransportUnit.Size = new System.Drawing.Size(198, 49);
            this.btnAddTransportUnit.TabIndex = 1;
            this.btnAddTransportUnit.Text = "Add Transport Unit";
            this.btnAddTransportUnit.UseVisualStyleBackColor = false;
            this.btnAddTransportUnit.Click += new System.EventHandler(this.BtnAddTransportUnit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitle.Location = new System.Drawing.Point(26, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(486, 42);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Manage Transport Units";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtSearch.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtSearch.Location = new System.Drawing.Point(33, 109);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(221, 22);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "Search...";
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearch.GotFocus += new System.EventHandler(this.TxtSearch_GotFocus);
            this.txtSearch.LostFocus += new System.EventHandler(this.TxtSearch_LostFocus);
            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cmbSearchBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchBy.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.ItemHeight = 20;
            this.cmbSearchBy.Items.AddRange(new object[] {
            "TransportUnitID",
            "LorryName",
            "DriverName",
            "AssistantName",
            "ContainerID",
            "UnitName",
            "IsAvailable"});
            this.cmbSearchBy.Location = new System.Drawing.Point(276, 105);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(162, 26);
            this.cmbSearchBy.TabIndex = 4;
            this.cmbSearchBy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSearchBy_DrawItem);
            // 
            // TransportUnitManagementControl
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.cmbSearchBy);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddTransportUnit);
            this.Controls.Add(this.dgvTransportUnits);
            this.Name = "TransportUnitManagementControl";
            this.Size = new System.Drawing.Size(1035, 589);
            this.Load += new System.EventHandler(this.TransportUnitManagementControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransportUnits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
