using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DatabaseApp.SQLConnection;
using Org.BouncyCastle.Tsp;

namespace DatabaseApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //Test Database Connection Button
    public async void TestDBConnection(object sender, RoutedEventArgs e)
    {
        //Disable Button
        dbTestConnectionBox.IsEnabled = false;

        //Determine Port
        string dbPort;
        string[] portValues = {"1433", "1434", "1521", "1830", "3306", "5432", "7210"};
        if (dbPortBox.SelectedIndex == 0)
        {
            dbPort = dbCustomPortBox.Text;
        }
        else
        {
            dbPort = portValues[(dbPortBox.SelectedIndex)-1];
        }
        Console.WriteLine($"Testing SQL Connection at: {dbIPBox.Text}:{dbPort}");

        //Test Connection
        try
        {
            MySQLServer testServer = new MySQLServer();
            testServer.serverHost = dbIPBox.Text;
            testServer.serverName = dbNameBox.Text;
            testServer.serverUser = dbUserBox.Text;
            testServer.serverPass = dbPassBox.Text;
            testServer.TestConnection();
        }
        //Connection Failed
        catch (MySql.Data.MySqlClient.MySqlException exceptionText)
        {
            dbTestConnectionBox.Content = "Test Unsuccessful";
            dbTestConnectionDebug.Text = exceptionText.ToString();
            Console.WriteLine(exceptionText);
        }
        
        // Wait 3s before re-enabling button.
        await Task.Delay(3000);
        dbTestConnectionBox.Content = "Test Database Connection";
        dbTestConnectionBox.IsEnabled = true;
    }

    //Enable Custom Port Box when selected in drop-down
    public void ComboBoxSelectionMade(object sender, SelectionChangedEventArgs e)
    {
        if (dbPortBox == null){return;}
        if (dbPortBox.SelectedIndex == 0)
        {
            dbCustomPortBox.IsEnabled = true;
        }
        else
        {
            dbCustomPortBox.IsEnabled = false;
        }
    }
}