using Npgsql;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    //static class for creating tables, getting values, entering data into the database, update database
    public static class DBHelper
    {
        public static void CreateCommand(string connectionString, string queryString)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
                       connectionString))
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void SelectDB(string queryString,
        string connectionString, DataSet ds, DataTable dt, DataGridView dataGridView1)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
                       connectionString))
            {
                connection.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(queryString, connection);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        public static async void InsertIntoDB(string path, string myConnectionString)
        {
            string line = null;

            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding(1251)))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.StartsWith("#") && !line.StartsWith("*") && !line.Equals("ТБ=01"))
                        {
                            var splitStrings = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < splitStrings.Length; i++)
                            {
                                splitStrings[i] = "'" + splitStrings[i] + "'";
                            }

                            string strOut = string.Join(",", splitStrings);
                            string query = @"INSERT INTO Proverka (""a"", ""b"", ""v"", ""g"", ""2"", ""3"", ""4"", ""5"", ""6"", ""7"", ""8"", ""9"", ""10"", ""11"", ""12"", ""13"", ""14"") " +
                                " Values(" + strOut + ")";

                            using (NpgsqlConnection connection = new NpgsqlConnection(
                            myConnectionString))
                            {
                                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                                command.Connection.Open();
                                command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateDB(DataGridView dataGridView1, string myConnectionString, string querySelect)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(myConnectionString))
                {
                    DataTable changes = ((DataTable)dataGridView1.DataSource).GetChanges();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(querySelect, connection);
                    if (changes != null)
                    {
                        NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(da);
                        da.UpdateCommand = builder.GetUpdateCommand();
                        da.Update(changes);
                        ((DataTable)dataGridView1.DataSource).AcceptChanges();
                        MessageBox.Show("Cell Updated");
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
