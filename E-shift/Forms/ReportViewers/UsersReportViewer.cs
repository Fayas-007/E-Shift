using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class UsersReportViewer : Form
    {
        private DataTable _reportData;

        public UsersReportViewer(DataTable reportData)
        {
            InitializeComponent();
            _reportData = reportData;
        }

        private void UsersReportViewer_Load(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("No data available for the report.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            rptvUsers.LocalReport.DataSources.Clear();

            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "UsersReport.rdlc");
            rptvUsers.LocalReport.ReportPath = reportPath;

            ReportDataSource rds = new ReportDataSource("UsersDataSet", _reportData);
            rptvUsers.LocalReport.DataSources.Add(rds);

            rptvUsers.RefreshReport();
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = rptvUsers.LocalReport.Render(
                    "PDF", null, out string mimeType, out string encoding,
                    out string extension, out string[] streamIds, out Warning[] warnings);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save Users Report";
                    saveFileDialog.FileName = "UsersReport.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, bytes);
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
                byte[] pdfContent = rptvUsers.LocalReport.Render(
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
