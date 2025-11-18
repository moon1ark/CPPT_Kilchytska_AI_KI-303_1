using System.Windows;
using System.Collections.Generic; 
using System.Linq;
using System.Windows.Controls; 

namespace DormitoryApp.Views
{
    public partial class ResidentsListWindow : Window
    {
        private readonly TcpClientService _clientService;

        private List<ResidentModel> _allResidents;

        public ResidentsListWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
            _allResidents = new List<ResidentModel>(); 
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string response = await _clientService.SendRequestAsync("GET_RESIDENTS");

            if (response.StartsWith("ERROR") || response.StartsWith("EMPTY"))
            {
                MessageBox.Show(response.Split('|')[1]);
                return;
            }

            _allResidents.Clear(); 
            string[] dataRows = response.Split('|').Skip(1).ToArray();

            foreach (string row in dataRows)
            {
                string[] fields = row.Split(';');
                _allResidents.Add(new ResidentModel
                {
                    FullName = fields[0],
                    Login = fields[1],
                    StudentId = fields[2],
                    AssignedRoom = fields[3]
                });
            }

            ResidentsGrid.ItemsSource = _allResidents;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                ResidentsGrid.ItemsSource = _allResidents;
            }
            else
            {

                var filteredList = _allResidents
                    .Where(resident => resident.FullName.ToLower().Contains(searchText))
                    .ToList();

                ResidentsGrid.ItemsSource = filteredList;
            }
        }
    }
}