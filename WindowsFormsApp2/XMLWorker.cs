using System;
using System.Linq;
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

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            string celllValue = cell.Value.ToString().Trim();
                            bool isAllZeros = celllValue.Where(x => char.IsDigit(x))
                                                    .All(x => x == '0');
                            if (!isAllZeros)
                            {
                                if (cell.ColumnIndex == 3) //1в
                                {
                                    dataElement.SetAttribute("СинтСчёт", celllValue);
                                }

                                if (cell.ColumnIndex == 4) //1г
                                {
                                    dataElement.SetAttribute("КОСГУ", celllValue);
                                }

                                if (cell.ColumnIndex != 4 & cell.ColumnIndex != 3 & cell.ColumnIndex != 0)
                                {
                                    dataElement.SetAttribute("_x" + cell.ColumnIndex.ToString(), celllValue);
                                }
                            }
                        }
                    }

                    var t = row.Index;
                    rootElement.AppendChild(dataElement);
                }

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
