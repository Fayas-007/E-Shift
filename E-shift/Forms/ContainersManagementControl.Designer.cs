using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Forms
{
    partial class ContainerManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvContainers;
        private Button btnAddContainer;
        private Label lblTitle;
        private TextBox txtSearch;
        private ComboBox cmbSearchBy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvContainers = new DataGridView();
            this.btnAddContainer = new Button();
            this.lblTitle = new Label();
            this.txtSearch = new TextBox();
            this.cmbSearchBy = new ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvContainers)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Verdana", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitle.Location = new Point(25, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(440, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Container Management";

            // 
            // txtSearch
            // 
            this.txtSearch.Font = new Font("Microsoft Sans Serif", 9.75F);
            this.txtSearch.ForeColor = SystemColors.ControlDarkDark;
            this.txtSearch.Location = new Point(30, 110);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(221, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search...";
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);

            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.BackColor = SystemColors.HotTrack;
            this.cmbSearchBy.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmbSearchBy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSearchBy.Font = new Font("Cambria", 11.25F, FontStyle.Bold);
            this.cmbSearchBy.ForeColor = Color.White;
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.ItemHeight = 20;
            this.cmbSearchBy.Location = new Point(275, 106);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new Size(165, 26);
            this.cmbSearchBy.TabIndex = 2;
            this.cmbSearchBy.DrawItem += new DrawItemEventHandler(this.cmbSearchBy_DrawItem);

            // 
            // btnAddContainer
            // 
            this.btnAddContainer.BackColor = Color.SeaGreen;
            this.btnAddContainer.FlatStyle = FlatStyle.Flat;
            this.btnAddContainer.Font = new Font("Verdana", 11.25F, FontStyle.Bold);
            this.btnAddContainer.ForeColor = Color.White;
            this.btnAddContainer.Location = new Point(846, 105);
            this.btnAddContainer.Name = "btnAddContainer";
            this.btnAddContainer.Size = new Size(158, 40);
            this.btnAddContainer.TabIndex = 3;
            this.btnAddContainer.Text = "Add Container";
            this.btnAddContainer.UseVisualStyleBackColor = false;
            this.btnAddContainer.Click += new System.EventHandler(this.BtnAddContainer_Click);

            // 
            // dgvContainers
            // 
            this.dgvContainers.AllowUserToAddRows = false;
            this.dgvContainers.AllowUserToDeleteRows = false;
            this.dgvContainers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContainers.BackgroundColor = Color.White;
            this.dgvContainers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContainers.Location = new Point(30, 160);
            this.dgvContainers.Name = "dgvContainers";
            this.dgvContainers.ReadOnly = true;
            this.dgvContainers.RowTemplate.Height = 28;
            this.dgvContainers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvContainers.Size = new Size(974, 405);
            this.dgvContainers.TabIndex = 4;

            // 
            // ContainerManagementControl
            // 
            this.BackColor = Color.WhiteSmoke;
            this.Controls.Add(this.cmbSearchBy);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddContainer);
            this.Controls.Add(this.dgvContainers);
            this.Name = "ContainerManagementControl";
            this.Size = new Size(1035, 589);
            this.Load += new System.EventHandler(this.ContainerManagementControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvContainers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
