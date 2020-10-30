using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp2
{
    public static class XMLWorker
    {
        public static void CreaterXML(this DataGridView dataGridView1, string path)
        {
            try
            {
                XmlDocument XMLDoc = new XmlDocument();

                XmlDeclaration XMLDec = XMLDoc.CreateXmlDeclaration("1.0", "WINDOWS-1251", null);
                XMLDoc.AppendChild(XMLDec);

                XmlElement rootElement = XMLDoc.CreateElement("RootXml");
                XMLDoc.AppendChild(rootElement);

                XmlElement reportElement = XMLDoc.CreateElement("Report");
                reportElement.SetAttribute("Code", "042");
                reportElement.SetAttribute("AlbumCode", "МЕС_К");
                rootElement.AppendChild(reportElement);

                XmlElement formatElement = XMLDoc.CreateElement("FormVariant");
                formatElement.SetAttribute("Number", "1");
                formatElement.SetAttribute("NsiVariantCode", "0000");
                rootElement.AppendChild(formatElement);

                XmlElement tableElement = XMLDoc.CreateElement("Table");
                tableElement.SetAttribute("Code", "Строка");
                rootElement.AppendChild(tableElement);

                XMLDoc.FillBodyXML(rootElement, dataGridView1);
                XMLDoc.Save(path);
                MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //filling dynamic XML fields
        public static void FillBodyXML(this XmlDocument XMLDoc, XmlElement rootElement, DataGridView dataGridView1)
        {
            List<string> columnName = new List<string>(); //хранит числовые столбцы
            var totalAmount = new List<KeyValuePair<string, decimal?>>();
            var line = new List<KeyValuePair<string, decimal?>>();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                string numericPart = Regex.Match(dataGridView1.Columns[i].Name, "\\d+").Value.ToString();
                columnName.Add(numericPart);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                XmlElement dataElement = XMLDoc.CreateElement("Data");
                DataGridViewCellCollection reverseCells = row.Cells;

                //for (int i = reverseCells.Count - 1; i > 2 & i <= reverseCells.Count - 1; i--)
                for (int i = 3; i <= reverseCells.Count - 1; i++)
                {
                    if (reverseCells[i].Value != null)
                    {
                        string cellValue = reverseCells[i].Value.ToString().Trim();
                        string nameAttribute = null;
                        bool isAllZeros = cellValue.Where(x => char.IsDigit(x))
                                                .All(x => x == '0');
                        if (!isAllZeros || reverseCells[i].ColumnIndex == 4) // КОСГУ из столбца D все равно добавляем, даже если равно нулю
                        {
                            if (reverseCells[i].ColumnIndex == 3) //1в
                            {
                                nameAttribute = "СинтСчёт";
                                dataElement.SetAttribute(nameAttribute, cellValue);
                            }

                            if (reverseCells[i].ColumnIndex == 4) //1г
                            {
                                nameAttribute = "КОСГУ";
                                dataElement.SetAttribute("КОСГУ", cellValue);
                            }

                            if (reverseCells[i].ColumnIndex != 4 & reverseCells[i].ColumnIndex != 3 & reverseCells[i].ColumnIndex != 0)
                            {
                                nameAttribute = "_x" + columnName[i].ToString();
                                dataElement.SetAttribute(nameAttribute, cellValue);
                            }

                            line.Add(new KeyValuePair<string, decimal?>(reverseCells[0].Value.ToString(), Convert.ToDecimal(cellValue)));
                            totalAmount.Add(new KeyValuePair<string, decimal?>(nameAttribute, Convert.ToDecimal(cellValue)));
                        }
                    }
                }

                var resultLineSum = line.SumValue().Values.FirstOrDefault();

                if (resultLineSum != null & resultLineSum != Convert.ToDecimal(0))
                {
                    rootElement.AppendChild(dataElement);
                }

                line.Clear();
            }

            XmlElement totalElement = XMLDoc.CreateElement("Data");
            totalElement.FillTotalLine(totalAmount);
            rootElement.AppendChild(totalElement);
        }

        //filling in the total amount field by column
        public static void FillTotalLine(this XmlElement element, List<KeyValuePair<string, decimal?>> list)
        {
            var resultDic = list.SumValue();

            foreach (var elem in resultDic)
            {
                if (elem.Key != "КОСГУ" & elem.Key != "СинтСчёт")
                {
                    element.SetAttribute(elem.Key, elem.Value.ToString());
                }

                else
                {
                    var value = elem.Key == "КОСГУ" ? "888" : "88888";
                    element.SetAttribute(elem.Key, value);
                }
            }
        }
    }
}
