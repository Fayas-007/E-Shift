using System;
using System.Data;
using System.Linq;

namespace E_shift.Helpers
{
    public static class LinearSearchHelper
    {

        public static DataTable FilterDataTable(DataTable sourceTable, string columnName, string keyword)
        {
            if (string.IsNullOrWhiteSpace(columnName) || string.IsNullOrWhiteSpace(keyword))
                return sourceTable;

            try
            {
                var filteredRows = sourceTable.AsEnumerable()
                    .Where(row => row[columnName]?.ToString()
                        .IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);

                return filteredRows.Any()
                    ? filteredRows.CopyToDataTable()
                    : sourceTable.Clone(); // Return empty table with same schema
            }
            catch
            {
                return sourceTable;
            }
        }
    }
}
