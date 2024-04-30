using Data;
using MaterialDesignThemes.Wpf;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO_NET_First
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //Data Source="10.0.0.40, 1433";User ID=student;Password=1111;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False
    public partial class MainWindow : Window
    {
        DBManager dBManager;
        public MainWindow()
        {
            InitializeComponent();
            dBManager = new DBManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (dBManager.ConnectionString == null)
                {
                    throw new Exception("Connection String is Null");
                }
                if (dBManager.ConnectToDB())
                {
                    if (tbQuery.Text.ToLower().StartsWith("select"))
                    {
                        var reader = dBManager.SelectFromDb(tbQuery.Text);
                        if (reader != null)
                        {
                            dgMain.ItemsSource = reader;
                            UpdateLayout();
                        }
                    }
                    else
                    {
                        int result = dBManager.CreateOrInsertOrDelete(tbQuery.Text);
                        MessageBox.Show(result.ToString(), "Result", MessageBoxButton.OK, MessageBoxImage.Information); 
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbConString_TextChanged(object sender, TextChangedEventArgs e)
        {
            dBManager.ConnectionString = tbConString.Text;
        }
    }
}