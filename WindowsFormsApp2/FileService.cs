using Npgsql;
using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    //
    public static class FileService
    {
        public static string SaveFile()
        {
            string path = null;
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "xml files|*.xml";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;
                }

                return path;
            }
        }

        public static string OpenFile()
        {
            string path = null;
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "txt files|*.txt";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                }

                return path;
            }
        }
    }
}
