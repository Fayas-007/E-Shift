using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class VehicleReportViewer : Form
    {
        private DataTable _reportData;

        public VehicleReportViewer(DataTable reportData)
        {
            InitializeComponent();
            _reportData = reportData;
        }

        private void VehicleReportViewer_Load(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("No data available for the vehicle report.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            rptvVehicles.LocalReport.DataSources.Clear();

            // Adjust path to your actual RDLC location
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "VehicleReport.rdlc");
            rptvVehicles.LocalReport.ReportPath = reportPath;

            // The "VehicleDataSet" must match the dataset name in your RDLC file
            ReportDataSource rds = new ReportDataSource("VehicleDataSet", _reportData);
            rptvVehicles.LocalReport.DataSources.Add(rds);

            rptvVehicles.RefreshReport();
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                byte[] pdfContent = rptvVehicles.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save Vehicle Report";
                    saveFileDialog.FileName = "VehicleReport.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, pdfContent);
                        MessageBox.Show("Report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to export PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSavePdf_Click_1(object sender, EventArgs e)
        {

            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                // Render the report as PDF bytes
                byte[] pdfContent = rptvVehicles.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "PDF Files (*.pdf)|*.pdf";
                    dlg.FileName = "CustomerReport.pdf";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(dlg.FileName, pdfContent);
                        MessageBox.Show("Report saved successfully.", "Success");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving PDF: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
