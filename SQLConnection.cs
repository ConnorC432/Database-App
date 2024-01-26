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
        public string serverName;
        public string serverHost;
        public string serverPort;
        public string serverUser;
        public string serverPass;
        private string mySqlConnectionString;

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