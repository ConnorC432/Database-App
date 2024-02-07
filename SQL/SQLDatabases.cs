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
    public class SQLDatabaseTab
    {
        public void PopulateDatabases(string dbString)
        {
            using (MySqlConnection conn = new MySqlConnection(dbString))
            {
                conn.Open();
                var databases = FindDatabases(conn);

                foreach (var database in databases)
                {
                    var tabItem = new TabItem();
                    tabItem.Header = database;

                    var databaseEntry = new Expander();
                    databaseEntry.Header = database;
                    var grid = new Grid();

                    var tableEntry = FindTables(conn, database);
                    int row = 0;
                    foreach (var table in tableEntry)
                    {
                        var button = new Button();
                        button.Content = table;
                        button.Click += (sender, e) =>
                        {
                            Console.WriteLine($"Table '{table}' clicked");
                        };
                        grid.RowDefinitions.Add(new RowDefinition());
                        grid.Children.Add(button);
                        Grid.SetRow(button, row);
                        row++;
                    }
                
                databaseEntry.Content = grid;
                tabItem.Content = databaseEntry;
                var tabControl = new TabControl();
                tabControl.Items.Add(tabItem);
                }
            }
        }

        //Find Database tables
        private List<string> FindDatabases(MySqlConnection conn)
        {
            var databases = new List<string>();
            var findDB = new MySqlCommand("SHOW DATABASES WHERE `Database` NOT IN ('information_schema', 'performance_schema', 'mysql')", conn);
            using (var reader = findDB.ExecuteReader())
            {
                while (reader.Read())
                {
                    databases.Add(reader.GetString(0));
                }
            }
            return databases;
        }

        //Find Tables
        private List<string> FindTables(MySqlConnection conn, string database)
        {
            var tables = new List<string>();
            var findTables = new MySqlCommand($"SHOW TABLES FROM {database}", conn);
            using (var reader = findTables.ExecuteReader())
            {
                while (reader.Read())
                {
                    tables.Add(reader.GetString(0));
                }
            }
            return tables;
        }
    }
}