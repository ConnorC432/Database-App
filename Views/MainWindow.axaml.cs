using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DatabaseApp.SQLConnection;
using DatabaseApp.SQLDatabases;
using System.Configuration;

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
        var uiThread = Avalonia.Threading.Dispatcher.UIThread;
        MySQLServer testServer = null;
        bool testSuccess = false;

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
            await Task.Run(() =>
            {
                testServer = new MySQLServer();
                testServer.serverHost = uiThread.Invoke(() => dbIPBox.Text);
                testServer.serverPort = dbPort;
                testServer.serverName = uiThread.Invoke(() => dbNameBox.Text);
                testServer.serverUser = uiThread.Invoke(() => dbUserBox.Text);
                testServer.serverPass = uiThread.Invoke(() => dbPassBox.Text);
                testServer.TestConnection();
                uiThread.Post(() =>
                {
                    dbTestConnectionBox.Content = "Test Successful";
                });
                testSuccess = true;
            });
        }

        //Connection Failed
        catch (MySql.Data.MySqlClient.MySqlException exceptionText)
        {
            uiThread.Post(() =>
            {
                dbTestConnectionBox.Content = "Test Unsuccessful";
                dbTestConnectionDebug.Text = exceptionText.ToString();
                Console.WriteLine(exceptionText);
            });
        }

        //Save Database as new Object
        finally
        {
            if (testServer != null && testSuccess == true)
            {
                MySQLServer databaseServer = new MySQLServer();
                uiThread.Invoke(() => databaseServer.SaveVariables(testServer));

                //Populate Database Tab
                SQLDatabaseEntries populateDB = new SQLDatabaseEntries();
                uiThread.Invoke(() => populateDB.PopulateDatabases(databaseServer.GenerateConnectionString()));

                //Enable Database Tab
                databaseTab.IsEnabled = true;
            }
        }
        
        // Wait 3s before re-enabling button.
        await Task.Delay(3000);
        uiThread.Post(() =>
        {
            dbTestConnectionBox.Content = "Test & Save Database Connection";
            dbTestConnectionBox.IsEnabled = true;
        });
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