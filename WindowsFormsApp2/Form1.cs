using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp2
{
    public partial class ProgramDebt : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        //string queryCeate = @"CREATE TABLE IF NOT EXISTS Proverka (Id INT PRIMARY KEY, ""a"" NCHAR(100),
								//	 ""b"" NCHAR(100) ,""v"" NCHAR(100) ,""g"" NCHAR(100) ,""2"" NCHAR(100) ,
								//	""3"" NCHAR(100) ,""4"" NCHAR(100) ,""5"" NCHAR(100) ,""6"" NCHAR(100) ,
								//	""7"" NCHAR(100) ,""8"" NCHAR(100) ,""9"" NCHAR(100) ,""10"" NCHAR(100) ,""11"" NCHAR(100) ,
								//	 ""12"" NCHAR(100) ,""13"" NCHAR(100) ,""14"" NCHAR(100))";

        string myConnectionString = ConfigurationManager.ConnectionStrings["localDBConnection"].ConnectionString;
        string querySelect = @"SELECT * FROM public.proverka";

        public ProgramDebt()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DBHelper.CreateCommand(myConnectionString, queryCeate);
            DBHelper.SelectDB(querySelect, myConnectionString, ds, dt, dataGridView1);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                DataGridViewCell cell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.CurrentCell = cell;
                dataGridView1.BeginEdit(true);
            }
        }

        private void Add_button_Click(object sender, System.EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow(); 
            ds.Tables[0].Rows.Add(row);
        }

        private void Delete_button_Click(object sender, System.EventArgs e)
        {
            foreach (DataGridViewRow dg in dataGridView1.SelectedRows)
            {
                try
                {
                    dataGridView1.Rows.Remove(dg);
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Load_button_Click(object sender, EventArgs e)
        {
            string path = FileService.OpenFile();

            if (path != null)
            {
                DBHelper.InsertIntoDB(path, myConnectionString);
            }
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                           String.Format("(a like '{0}%') or (b like '{0}%') or (v like '{0}%') or (g like '{0}%')", textBoxSearch.Text.Trim());
        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void Select_button_Click(object sender, EventArgs e)
        {
            DBHelper.SelectDB(querySelect, myConnectionString, ds, dt, dataGridView1);
        }

        private void Save_report_button_Click(object sender, EventArgs e)
        {
            var path = FileService.SaveFile();
            dataGridView1.CreaterXML(path);      
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DBHelper.UpdateDB(dataGridView1, myConnectionString, querySelect);
        }
    }
}
