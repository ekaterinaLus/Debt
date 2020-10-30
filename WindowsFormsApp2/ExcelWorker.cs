using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp2
{
    public static class ExcelWorker
    {
        public static void CreateExcel(this DataGridView dataGridView1, string path)
        {
            Excel.Application ObjWorkExcel = new Excel.Application();
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Add();
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
            ObjWorkSheet.Cells[1, 1] = "Номер (код) счета бюджетного учета";
            ObjWorkExcel.FillBodyExcel(ObjWorkSheet, dataGridView1);
            ObjWorkExcel.ActiveWorkbook.SaveAs(path);
            ObjWorkExcel.DisplayAlerts = false;

            ObjWorkBook?.Close(false, path, Type.Missing);
            ObjWorkExcel.Application.Quit();
            Marshal.FinalReleaseComObject(ObjWorkSheet);
            Marshal.FinalReleaseComObject(ObjWorkBook);
            Marshal.FinalReleaseComObject(ObjWorkExcel);
            GC.Collect();
            MessageBox.Show("Файл Excel успешно добавлен");
        }

        public static void FillBodyExcel(this Excel.Application ObjWorkExcel, Excel.Worksheet ObjWorkSheet, DataGridView dataGridView1)
        {
            try
            {
                List<string> columnName = new List<string>();
                var totalAmount = new List<KeyValuePair<string, decimal?>>();
                var line = new List<KeyValuePair<string, decimal?>>();
                string columnFirst = null;
                List<int> deleteEmptyRow = new List<int>();

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    columnName.Add(dataGridView1.Columns[i].Name);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null & cell.ColumnIndex != 0)
                        {
                            string cellValue = cell.Value.ToString().Trim();

                            if (cell.ColumnIndex == 1 || cell.ColumnIndex == 2 || cell.ColumnIndex == 3 || cell.ColumnIndex == 4)
                            {
                                columnFirst += $"{columnName[cell.ColumnIndex]} = {cellValue}; ";
                            }

                            else
                            {
                                ObjWorkSheet.Cells[1, cell.ColumnIndex - 3] = columnName[cell.ColumnIndex]; //вычитаем индекс, т.к.первые четыре столбца складываютс в один
                                ObjWorkSheet.Cells[row.Index + 2, cell.ColumnIndex - 3] = cellValue;
                            }

                            line.Add(new KeyValuePair<string, decimal?>(row.Cells[0].Value.ToString(), Convert.ToDecimal(cell.Value.ToString().Trim())));
                            totalAmount.Add(new KeyValuePair<string, decimal?>(cell.ColumnIndex.ToString(), Convert.ToDecimal(cellValue)));
                        }
                    }

                    ObjWorkSheet.ExcelStyle(dataGridView1, row.Index + 2);

                    var resultLineSum = line.SumValue().Values.FirstOrDefault();

                    if (resultLineSum == null || resultLineSum == Convert.ToDecimal(0))
                    {
                        deleteEmptyRow.Add(row.Index + 2);
                    }

                    line.Clear();
                    ObjWorkSheet.Cells[row.Index + 2, 1] = columnFirst; 
                    columnFirst = null;
                }

                ObjWorkExcel.FillTotalLine(dataGridView1, totalAmount); //Заполняем итоговую строку

                foreach (var row in deleteEmptyRow) //удаление пустых и нулевых строк
                {
                    Excel.Range deleteRow = (Excel.Range)ObjWorkSheet.Rows[row, Type.Missing];
                    deleteRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void FillTotalLine(this Excel.Application ObjWorkExcel, DataGridView dataGridView1, List<KeyValuePair<string, decimal?>> totalAmount)
        {
            var columnCnt = dataGridView1.ColumnCount;
            var rowCnt = dataGridView1.RowCount;
            string columnFirst = "Всего задолженности:\n";
            var resultDic = totalAmount.SumValue();

            for (int i = 1; i < columnCnt; i++)
            {
                if (i <= 4)
                {
                    columnFirst += $" {dataGridView1.Columns[i].Name} = {resultDic[i.ToString()]};";
                }

                else
                {
                    ObjWorkExcel.Cells[rowCnt + 1, i - 3] = resultDic[i.ToString()];
                }
            }

            ObjWorkExcel.Cells[rowCnt + 1, 1] = columnFirst;
        }

        //Method sets the style of the Excel file
        public static void ExcelStyle(this Excel.Worksheet sheet, DataGridView dataGridView1, int index)
        {
            var rangeStyle = sheet.Range[sheet.Cells[1, 1], sheet.Cells[index, dataGridView1.Columns.Count - 4]];
            sheet.Cells[index, 1].ColumnWidth = 38;
            sheet.Range[sheet.Cells[index, 1], sheet.Cells[index, 1]].WrapText = true;
            rangeStyle.Borders.Color = Color.Black; //обвод ячеек
            sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, dataGridView1.Columns.Count - 4]].Interior.Color = Color.LightGray;//первая ячейка цвет
            sheet.Range[sheet.Cells[dataGridView1.RowCount + 1, 1], sheet.Cells[dataGridView1.RowCount + 1, dataGridView1.Columns.Count - 4]].Interior.Color = Color.Yellow; //последняя ячейка цвет
            rangeStyle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            rangeStyle.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        }
    }
}

