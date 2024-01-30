using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DatabaseApp.SQLConnection
{
    public class MySQLServer
    {
        public string serverName { get; set; } = "";
        public string serverHost { get; set; } = "";
        public string serverPort { get; set; } = "";
        public string serverUser { get; set; } = "";
        public string serverPass { get; set; } = "";
        private string mySqlConnectionString { get; set; } = "";

        public void TestConnection()
        {
            //Generate connection string
            mySqlConnectionString = $"Server={serverHost}; Port={serverPort}; Database={serverName}; Uid={serverUser}; Pwd={serverPass};";
            //Open Connection
            MySqlConnection testConnection = new MySqlConnection(mySqlConnectionString);
            testConnection.Open();
            Console.WriteLine("Test Successful");
            testConnection.Close();
        }
    }
}