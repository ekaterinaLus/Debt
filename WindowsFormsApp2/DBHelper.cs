using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    //static class for creating tables, getting values, entering data into the database, update database
    public static class DBHelper
    {
        static string tableName = @"Check";
        static List<string> columns = new List<string>();

        public static void SelectDB(string connectionString, DataSet ds, DataTable dt, DataGridView dataGridView1)
        {
            string querySelect = $"Select * from \"{tableName}\"";

            using (NpgsqlConnection connection = new NpgsqlConnection(
                       connectionString))
            {
                connection.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(querySelect, connection);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        public static void InsertIntoDB(string connectionString, string path)
        {
            try
            {
                string line = null;
                DataTable dt = new DataTable();

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding(1251)))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.StartsWith("#") && !line.StartsWith("*") && !line.Equals("ТБ=01"))
                        {
                            string columnName = null;
                            var splitStrings = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < splitStrings.Length; i++)
                            {
                                splitStrings[i] = "'" + splitStrings[i] + "'";
                            }

                            CreateStructTable(connectionString, splitStrings.Length - 1);
                            string valueName = string.Join(",", splitStrings);
                            columnName = string.Join(",", columns).Replace("'", "\"");
                            string query = $"INSERT INTO \"{tableName}\" ({columnName}) Values({valueName})";
                            CommandExecution(connectionString, query);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateStructTable(string connectionString, int columnCount)
        {
            string column;
            if (columns.Count == 0)
            {
                int j = 1;
                int length = 65 + columnCount;
                //т.к. столбцы носят названия букв и чисел, то для создания или изменения столбцов таблицы удобнее использовать цикл - количество столбцов динамически изменяется
                for (int i = 65; i <= length; i++)
                {
                    if (i > 68) //После A, B, C, D начинается с 2-х согласно шаблону(A,B,C,D,E2,F3 b т.д.)
                    {
                        j++;
                        column = string.Join("", (char)i, j.ToString());
                        columns.Add(@"'" + column + @"'");
                    }

                    else
                    {
                        column = @"'" + (char)i + @"'";
                        columns.Add(column);
                    }
                }

                string queryString = $"select createCommand(\'{tableName}\', array[{string.Join(",", columns)}])";
                CommandExecution(connectionString, queryString);
            }        
        }

        public static void UpdateDB(string connectionString, DataGridView dataGridView1)
        {
            try
            {
                string querySelect = $"Select * from \"{tableName}\"";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
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

        //procedure for checking and creating tables and columns if they are missing in the database
        public static void CreateObjectDB(string connectionString)
        {
            string queryString = @"CREATE OR REPLACE function createCommand(tableName varchar(30), columnName varchar(100)[]) 
                            RETURNS void AS
                            $func$
                            DECLARE
                                i integer;
                                BEGIN
		                            EXECUTE format('create table if not exists %I.%I (Id serial NOT NULL  primary key)'
						            , 'public', (tableName));	
		                            FOR i IN 1 ..array_length(columnName, 1)
    		                            LOOP
        		                            RAISE NOTICE 'i = %', i;
				                            EXECUTE format('ALTER TABLE %I.%I ADD COLUMN IF NOT EXISTS %I %s', 'public', tableName, columnName[i], ' varchar(100)');
    		                            END LOOP;		 
                                END
                            $func$  LANGUAGE plpgsql;";

            CommandExecution(connectionString, queryString);
        }

        public static void CommandExecution(string connectionString, string query)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
             connectionString))
            {
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
