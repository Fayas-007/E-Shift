using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class JobReportViewer : Form
    {
        private readonly DataTable _reportData;

        // RDLC path (must match exactly including case and location in project)
        private const string ReportResource = "E_shift.Reports.JobReport.rdlc";

        public JobReportViewer(DataTable reportData)
        {
            InitializeComponent();
            _reportData = reportData;
            Load += JobReportViewer_Load;
        }

        private void JobReportViewer_Load(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("No job data to display.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            bool found = Assembly.GetExecutingAssembly()
                                 .GetManifestResourceNames()
                                 .Any(r => string.Equals(r, ReportResource, StringComparison.Ordinal));
            if (!found)
            {
                MessageBox.Show($"Embedded report resource not found:\n{ReportResource}", "Missing Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                rptvJobs.Reset();
                rptvJobs.LocalReport.ReportEmbeddedResource = ReportResource;
                rptvJobs.LocalReport.DataSources.Clear();
                rptvJobs.LocalReport.DataSources.Add(new ReportDataSource("JobDataSet", _reportData));  // must match dataset name in RDLC
                rptvJobs.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading job report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSavePdf_Click_1(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                byte[] pdfBytes = rptvJobs.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "PDF files (*.pdf)|*.pdf";
                    dlg.FileName = "JobReport.pdf";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(dlg.FileName, pdfBytes);
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
