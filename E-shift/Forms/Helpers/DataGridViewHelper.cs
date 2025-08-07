using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Helpers
{
    public static class DataGridViewHelper
    {
        public static void ApplyTheme(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.CellPainting -= Dgv_CellTheme;
            dgv.CellPainting += Dgv_CellTheme;

        }

        public static void AttachCellClickEvent(DataGridView dgv, DataGridViewCellEventHandler handler)
        {
            dgv.CellClick += handler;
        }
        private static void Dgv_CellTheme(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgv.Columns[e.ColumnIndex].Name;

            if (colName == "Edit" || colName == "Delete")
            {
                e.PaintBackground(e.CellBounds, true);

                string buttonText = colName;
                Color backColor = colName == "Delete"
                    ? Color.FromArgb(231, 76, 60)       
                    : Color.FromArgb(52, 152, 219);    

                using (SolidBrush brush = new SolidBrush(backColor))
                    e.Graphics.FillRectangle(brush, e.CellBounds);

                using (Pen borderPen = new Pen(Color.White, 1.2f))
                    e.Graphics.DrawRectangle(borderPen, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);

                TextRenderer.DrawText(e.Graphics, buttonText, dgv.Font, e.CellBounds, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }



    }
}
