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
                List<string> columnName = new List<string>(); //хранит числовые столбцы

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    string numericPart = Regex.Match(dataGridView1.Columns[i].Name, "\\d+").Value.ToString();
                    columnName.Add(numericPart);
                }

                var totalAmount = new List<KeyValuePair<string, decimal?>>();

                XmlDocument xDoc = new XmlDocument();

                XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "WINDOWS-1251", null);
                xDoc.AppendChild(xmlDec);

                XmlElement rootElement = xDoc.CreateElement("RootXml");
                xDoc.AppendChild(rootElement);

                XmlElement reportElement = xDoc.CreateElement("Report");
                reportElement.SetAttribute("Code", "042");
                reportElement.SetAttribute("AlbumCode", "МЕС_К");
                rootElement.AppendChild(reportElement);

                XmlElement formatElement = xDoc.CreateElement("FormVariant");
                formatElement.SetAttribute("Number", "1");
                formatElement.SetAttribute("NsiVariantCode", "0000");
                rootElement.AppendChild(formatElement);

                XmlElement tableElement = xDoc.CreateElement("Table");
                tableElement.SetAttribute("Code", "Строка");
                rootElement.AppendChild(tableElement);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    XmlElement dataElement = xDoc.CreateElement("Data");
                    DataGridViewCellCollection reverseCells = row.Cells;

                    //for (int i = reverseCells.Count - 1; i > 2 & i <= reverseCells.Count - 1; i--)
                    for (int i = 3; i <= reverseCells.Count - 1; i++)
                    {
                        if (reverseCells[i].Value != null)
                        {
                            string celllValue = reverseCells[i].Value.ToString().Trim();
                            string nameAttribute = null;
                            bool isAllZeros = celllValue.Where(x => char.IsDigit(x))
                                                    .All(x => x == '0');
                            if (!isAllZeros || reverseCells[i].ColumnIndex == 4) // КОСГУ из столбца D все равно добавляем, даже если равно нулю
                            {
                                if (reverseCells[i].ColumnIndex == 3) //1в
                                {
                                    nameAttribute = "СинтСчёт";
                                    dataElement.SetAttribute(nameAttribute, celllValue);
                                }

                                if (reverseCells[i].ColumnIndex == 4) //1г
                                {
                                    nameAttribute = "КОСГУ";
                                    dataElement.SetAttribute("КОСГУ", celllValue);
                                }

                                if (reverseCells[i].ColumnIndex != 4 & reverseCells[i].ColumnIndex != 3 & reverseCells[i].ColumnIndex != 0)
                                {
                                    nameAttribute = "_x" + columnName[i].ToString();
                                    dataElement.SetAttribute(nameAttribute, celllValue);
                                }

                                totalAmount.Add(new KeyValuePair<string, decimal?>(nameAttribute, Convert.ToDecimal(celllValue)));
                            }
                        }
                    }

                    rootElement.AppendChild(dataElement);
                }

                var resultDic = totalAmount.GroupBy(groupElem => groupElem.Key)
                                                .ToDictionary(key => key.Key, value => value.Sum(z => z.Value));

                XmlElement totalElement = xDoc.CreateElement("TOTAL");

                foreach (var elem in resultDic)
                {
                    totalElement.SetAttribute(elem.Key, elem.Value.ToString());
                }

                rootElement.AppendChild(totalElement);

                xDoc.Save(path);
                MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
