using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace E_shift.Helpers
{
    public static class SearchHelper
    {
        public static void AttachSearch(TextBox txtSearch, ComboBox cmbSearchBy, Action<string, string> onSearch)
        {
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Search...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            txtSearch.TextChanged += (s, e) =>
            {
                string keyword = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword) || keyword == "Search...")
                {
                    onSearch(null, null);
                    return;
                }

                if (cmbSearchBy.SelectedItem != null)
                {
                    string selectedCol = cmbSearchBy.SelectedItem.ToString();
                    onSearch(selectedCol, keyword);
                }
            };
        }

        public static DataTable LinearSearch(DataTable sourceTable, string columnName, string keyword)
        {
            if (string.IsNullOrWhiteSpace(columnName) || string.IsNullOrWhiteSpace(keyword))
                return sourceTable;

            DataTable resultTable = sourceTable.Clone();

            foreach (DataRow row in sourceTable.Rows)
            {
                string value;

                if (columnName == "Availability")
                {
                    bool isAvailable = row.Table.Columns.Contains("IsAvailable") && (bool)row["IsAvailable"];
                    value = isAvailable ? "Yes" : "No";
                }
                else
                {
                    value = row[columnName]?.ToString();
                }

                if (value != null && value.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultTable.ImportRow(row);
                }
            }

            return resultTable;
        }

    }
}
