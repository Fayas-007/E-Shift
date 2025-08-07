using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class LoadReportViewer : Form
    {
        private readonly DataTable _reportData;

        public LoadReportViewer(DataTable reportData)
        {
            InitializeComponent();

            _reportData = reportData ?? throw new ArgumentNullException(nameof(reportData));
            btnSavePdf.Click += BtnSavePdf_Click;
            this.Load += LoadReportViewer_Load;
        }

        private void LoadReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                if (_reportData == null || _reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No data to display in the report.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                rptvVehicles.Reset();

                // Adjust this path if your RDLC is embedded or at a different location
                string reportPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Reports", "LoadReport.rdlc");

                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found: " + reportPath, "Missing Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                rptvVehicles.LocalReport.ReportPath = reportPath;

                // Clear any existing data sources and add the one you passed with CustomerName included
                rptvVehicles.LocalReport.DataSources.Clear();
                rptvVehicles.LocalReport.DataSources.Add(
                    new ReportDataSource("LoadDataSet", _reportData)); // Ensure "LoadDataSet" matches the RDLC dataset name

                rptvVehicles.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSavePdf_Click(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                byte[] pdfContent = rptvVehicles.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "PDF Files (*.pdf)|*.pdf";
                    dlg.FileName = "LoadReport.pdf";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(dlg.FileName, pdfContent);
                        MessageBox.Show("Report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
