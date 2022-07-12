using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using ToolsDev.Utilities.Model;

namespace ToolsDev.Utilities.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// Read Excel convert to DataTable
        /// </summary>
        /// <param name="filePath">Excel file full path</param>
        /// <param name="sheetPosition">The sheet position, default=1</param>
        /// <param name="firstColumnForHeader">First column in excel is header, default true. If not header, use false</param>
        /// <returns></returns>
        public static DataTable ReadExcelToDatatable(string filePath, int sheetPosition = 1, bool firstColumnForHeader = true)
        {
            try
            {
                var workbook = new XLWorkbook(filePath);

                //Read sheet
                IXLWorksheet worksheet = workbook.Worksheet(sheetPosition);

                DataTable dt = new DataTable(worksheet.Name);

                bool firstRow = true;
                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        //Process Column Name
                        int columnNumber = 1;
                        foreach (var cell in row.Cells())
                        {
                            if (firstColumnForHeader)
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            else
                            {
                                dt.Columns.Add($"Column{columnNumber}");
                                columnNumber++;
                            }
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Process Row Data
                        dt.Rows.Add();
                        int i = 0;

                        foreach (var cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.Write($"{ex.ToJsonString()}");
                return null;
            }
        }

        /// <summary>
        /// Read Excel convert to object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">Excel file full path</param>
        /// <param name="error">Error output</param>
        /// <param name="sheetPosition">The sheet position, default=1</param>
        /// <param name="firstColumnForHeader">First column in excel is header, default true. If not header, use false</param>
        /// <returns></returns>
        public static List<T> ReadExcelToList<T>(string filePath, out Error error, int sheetPosition = 1, bool firstColumnForHeader = true)
        {
            error = new Error();
            try
            {
                var workbook = new XLWorkbook(filePath);

                //Read sheet
                IXLWorksheet worksheet = workbook.Worksheet(sheetPosition);

                DataTable dt = new DataTable(worksheet.Name);

                bool firstRow = true;
                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        //Process Column Name
                        int columnNumber = 1;
                        foreach (var cell in row.Cells())
                        {
                            if (firstColumnForHeader)
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            else
                            {
                                dt.Columns.Add($"Column{columnNumber}");
                                columnNumber++;
                            }
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Process Row Data
                        dt.Rows.Add();
                        int i = 0;

                        foreach (var cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                var listOutput = UtilityHelper.ConvertDateTable<T>(dt);
                return listOutput;
            }
            catch (Exception ex)
            {
                error.ErrorCode = "9999";
                error.ErrorMsg = $"ExcelHelper ReadExcelToList exception, {ex.ToJsonString()}";
                return null;
            }
        }
    }
}
