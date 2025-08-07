using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace E_shift.Forms.ReportViewers
{
    public partial class TransportUnitReportViewer : Form
    {
        private DataTable _reportData;

        public TransportUnitReportViewer(DataTable reportData)
        {
            InitializeComponent();
            _reportData = reportData;
        }

        private void TransportUnitReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                if (_reportData == null || _reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No transport unit data to display.");
                    this.Close();
                    return;
                }

                rptvUnits.Reset();
                rptvUnits.ProcessingMode = ProcessingMode.Local;

                // IMPORTANT: This must match the RDLC's namespace path
                rptvUnits.LocalReport.ReportEmbeddedResource = "E_shift.Reports.TransportUnitReport.rdlc";

                rptvUnits.LocalReport.DataSources.Clear();
                rptvUnits.LocalReport.DataSources.Add(
                    new ReportDataSource("TransportUnitDataSet", _reportData));

                rptvUnits.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                byte[] pdfContent = rptvUnits.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "PDF Files (*.pdf)|*.pdf";
                    dlg.FileName = "TransportUnitReport.pdf";

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
