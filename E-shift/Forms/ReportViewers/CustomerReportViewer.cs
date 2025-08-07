using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class CustomerReportViewer : Form
    {
        private DataTable _reportData;

        public CustomerReportViewer(DataTable reportData)
        {
            InitializeComponent();
            _reportData = reportData;
        }

        private void CustomerReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                if (_reportData == null || _reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No data to display in the report.");
                    this.Close();
                    return;
                }

                rptvCustomers.Reset();

                string reportPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Reports", "CustomerReport.rdlc");

                rptvCustomers.LocalReport.ReportPath = reportPath;
                rptvCustomers.LocalReport.DataSources.Clear();
                rptvCustomers.LocalReport.DataSources.Add(
                    new ReportDataSource("CustomerDataSet", _reportData)); // Match dataset name in RDLC

                rptvCustomers.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }


        private void btnSavePdf_Click_1(object sender, EventArgs e)
        {

            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                byte[] pdfContent = rptvCustomers.LocalReport.Render(
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
