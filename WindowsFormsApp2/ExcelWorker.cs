using System;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp2
{
    public static class ExcelWorker
    {
        public static void CreateExcel(string path)
        {
            //Excel.Workbooks objBooks;
            //Excel.Sheets objSheets;
            //Excel._Worksheet objSheet;
            //Excel.Range range;
            Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            //Количество листов в рабочей книге
            ex.SheetsInNewWorkbook = 2;
            //Добавить рабочую книгу
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            ex.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Отчет за 13.12.2017";
            //Пример заполнения ячеек
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j < 9; j++)
                    sheet.Cells[i, j] = String.Format("Boom {0} {1}", i, j);
            }
            //Захватываем диапазон ячеек
            Excel.Range range1 = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[9, 9]);
            //Шрифт для диапазона
            range1.Cells.Font.Name = "Tahoma";
            //Размер шрифта для диапазона
            range1.Cells.Font.Size = 10;
            //Захватываем другой диапазон ячеек
            Excel.Range range2 = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[9, 2]);
            range2.Cells.Font.Name = "Times New Roman";
            //Задаем цвет этого диапазона. Необходимо подключить System.Drawing
            range2.Cells.Font.Color = ColorTranslator.ToOle(Color.Green);
            //Фоновый цвет
            range2.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0xFF, 0xFF, 0xCC));
            var sh = workBook.Sheets;
            Excel.Worksheet sheetPivot = (Excel.Worksheet)sh.Add(Type.Missing, sh[1], Type.Missing, Type.Missing);
            sheetPivot.Name = "Сводная таблица";
            ex.Application.ActiveWorkbook.SaveAs(path);
        }
    }
}
