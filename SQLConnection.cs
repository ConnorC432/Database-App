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
        public MySQLServer()
        {
            mySqlConnectionString = $"Server={serverHost};Database={serverName};Uid={serverUser};Pwd={serverPass};";
        }

        public void TestConnection()
        {
            MySqlConnection testConnection = new MySqlConnection(mySqlConnectionString);
            testConnection.Open();
            Console.WriteLine("Test Successful");
        }
    }
}