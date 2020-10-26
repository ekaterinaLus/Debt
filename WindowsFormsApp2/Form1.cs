using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ProgramDebt : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private readonly string myConnectionString = ConfigurationManager.ConnectionStrings["localDBConnection"].ConnectionString;

        public ProgramDebt()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBHelper.CreateObjectDB(myConnectionString);
            DBHelper.SelectDB(myConnectionString, ds, dt, dataGridView1);
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
                DBHelper.InsertIntoDB(myConnectionString, path);
                DBHelper.SelectDB(myConnectionString, ds, dt, dataGridView1);
            }
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is DataTable d)
            {
                string searchValue = textBoxSearch.Text.Trim();
                d.DefaultView.RowFilter =
                           $"(a like '{searchValue}%') or (b like '{searchValue}%') or (v like '{searchValue}%') or (g like '{searchValue}%')";
            }
        }

        private void Select_button_Click(object sender, EventArgs e)
        {
            DBHelper.SelectDB(myConnectionString, ds, dt, dataGridView1);
        }

        private void Save_report_button_Click(object sender, EventArgs e)
        {
            string path = FileService.SaveFile();
            dataGridView1.CreaterXML(path);      
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DBHelper.UpdateDB(myConnectionString, dataGridView1);
        }
    }
}
