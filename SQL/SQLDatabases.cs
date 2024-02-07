using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DatabaseApp.SQLConnection;
using DatabaseApp.Views;
using System.Data;
using Avalonia.Controls;

namespace DatabaseApp.SQLDatabases
{
    public class DatabaseEntryObject
    {
        public string dbEntryName { get; set; }
        public List<string> tableName { get; set; }
    }
    public class SQLDatabaseTab
    {
        public void PopulateDatabases(string dbString, DataGrid dataGrid)
        {
            using (MySqlConnection conn = new MySqlConnection(dbString))
            {
                try
                {
                    conn.Open();
                    var databaseList = FindDatabases(conn);
                    foreach (var databaseEntry in databaseList)
                    {
                        var tableList = FindTables(conn, databaseEntry);
                        var entryObject = new DatabaseEntryObject
                        {
                            dbEntryName = databaseEntry,
                            tableName = tableList
                        };
                        dataGrid.Items.Add(entryObject);
                    }
                }
                catch
                {
                    Console.WriteLine("DB Population Failed");
                }
            }
        }

        //Find Database tables
        private List<string> FindDatabases(MySqlConnection conn)
        {
            databaseList = new List<string>();
            var findDB = new MySqlCommand("SHOW DATABASES WHERE `Database` NOT IN ('information_schema', 'performance_schema', 'mysql')", conn);
            using (var reader = findDB.ExecuteReader())
            {
                while (reader.Read())
                {
                    databaseList.Add(reader.GetString(0));
                }
            }
            return databaseList;
        }

        //Find Tables
        private List<string> FindTables(MySqlConnection conn, string databaseEntry)
        {
            tableList = new List<string>();
            var findTables = new MySqlCommand($"SHOW TABLES FROM {databaseEntry}", conn);
            using (var reader = findTables.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableList.Add(reader.GetString(0));
                }
            }
            return tableList;
        }
    }
}