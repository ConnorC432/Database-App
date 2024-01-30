using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DatabaseApp.SQLConnection;

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
        if (dbPortBox.SelectedIndex != 0)
        {
            dbPort = dbCustomPortBox.Text;
        }
        else
        {
            dbPort = "3306";
        }

        //Test Connection
        try
        {
            MySQLServer testServer = new MySQLServer();
            testServer.serverHost = dbIPBox.Text;
            testServer.serverPort = dbPort;
            testServer.serverName = dbNameBox.Text;
            testServer.serverUser = dbUserBox.Text;
            testServer.serverPass = dbPassBox.Text;
            testServer.TestConnection();
            dbTestConnectionBox.Content = "Test Successful";
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
        else if (dbPortBox.SelectedIndex != 0)
        {
            dbCustomPortBox.IsEnabled = true;
        }
        else
        {
            dbCustomPortBox.IsEnabled = false;
        }
    }
}