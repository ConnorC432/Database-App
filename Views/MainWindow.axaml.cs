using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

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
        string[] portValues = { "1433", "1434", "1521", "1830", "3306", "5432", "7210"};
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
            using (SqlConnection sqlTest = new SqlConnection($"Server={dbIPBox.Text},{dbPort};Database={dbNameBox.Text};User Id={dbUserBox.Text};Password={dbPassBox.Text};"))
            {
                sqlTest.Open();
                dbTestConnectionBox.Content = "Test Successful";
                sqlTest.Close();
            }
        }

        //Connection Failed
        catch (Exception exceptionText)
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

    //Enable Custom Port Box
    public async void ComboBoxSelectionMade(object sender, SelectionChangedEventArgs e)
    {
        /*if(dbPortBox.SelectedIndex != 0)
        {
            dbCustomPortBox.IsEnabled = true;
        }*/
    }
}