using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class ContainerReportViewer : Form
    {
        private DataTable _reportData;

        public ContainerReportViewer()
        {
            InitializeComponent();
        }

        // Constructor that accepts a DataTable with the report data
        public ContainerReportViewer(DataTable data) : this()
        {
            _reportData = data;
        }

        private void ContainerReportViewer_Load(object sender, EventArgs e)
        {
            if (_reportData != null)
            {
                // Assuming you have an embedded RDLC report named ContainerReport.rdlc in your project resources
                rptvContainers.LocalReport.ReportEmbeddedResource = "E_shift.Reports.ContainerReport.rdlc";

                // Clear previous data sources
                rptvContainers.LocalReport.DataSources.Clear();

                // Add new data source with the passed DataTable
                ReportDataSource rds = new ReportDataSource("ContainerDataSet", _reportData);
                rptvContainers.LocalReport.DataSources.Add(rds);

                rptvContainers.RefreshReport();
            }
        }


        private void btnSavePdf_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                // Render the report as PDF bytes
                byte[] pdfContent = rptvContainers.LocalReport.Render(
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
    }
}
