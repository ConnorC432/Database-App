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
using Mysqlx.Resultset;

namespace DatabaseApp.SQLDatabases
{
    public class SQLDatabaseEntries
    {
        //Populate Stack Panel with database list
        public void PopulateDatabases(StackPanel DatabaseStackPanel, string dbString)
        {
            using (var conn = new MySqlConnection(dbString))
            {
                conn.Open();
                var databaseList = FindDatabases(conn);

                foreach (var database in databaseList)
                {
                    //Add Expander for each database
                    var expander = new Expander();
                    expander.Header = database;

                    //Add Grid for each Database
                    var tableList = FindTables(conn, database);
                    var grid = new Grid();
                    int gridRow = 0;
                    foreach (var table in tableList)
                    {
                        //Add Button to Grid for each table
                        var button = new Button();
                        button.Content = table;
                        grid.RowDefinitions.Add(new RowDefinition());
                        grid.Children.Add(button);
                        Grid.SetRow(button, gridRow);
                        gridRow++;
                    }

                    //Add grid to expander
                    expander.Content = grid;

                    //Add expander to stack panel
                    //DatabaseStackPanel.ContextMenu = expander;
                    DatabaseStackPanel.Children.Add(expander);
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