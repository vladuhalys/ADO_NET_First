using Data;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ADO_NET_First
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //Data Source="10.0.0.40, 1433";User ID=student;Password=1111;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False
    public partial class MainWindow : Window
    {
        DBManager dBManager;
        Test test;
        public MainWindow()
        {
            InitializeComponent();
            dBManager = new DBManager();
            dgMain.ItemsSource = dBManager.SelectFromDb();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dBManager.InsertToDb(new Test(id: 0, text: tbName.Text));
                dgMain.ItemsSource = dBManager.SelectFromDb();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgMain_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //Test test = (Test)dgMain.SelectedItem;
            //tbName.Text = test.Text;
        }

        private void dgMain_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Test test = (Test)dgMain.SelectedItem;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var element = e.EditingElement as TextBox;
                test.Text = element.Text;
                dBManager.UpdateToDb(test);
            }
        }
    }
}