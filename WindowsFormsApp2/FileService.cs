using System.Windows.Forms;

namespace WindowsFormsApp2
{
    //
    public static class FileService
    {
        static string path = null;
        public static string SaveFile(string format)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = $"{format} files|*.{format}";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;
                }

                return path;
            }
        }

        public static string OpenFile()
        {
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
