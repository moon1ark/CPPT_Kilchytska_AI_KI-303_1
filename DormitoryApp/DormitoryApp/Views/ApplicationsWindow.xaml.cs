using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormitoryApp.Views
{
    public partial class ApplicationsWindow : Window
    {
        private readonly TcpClientService _clientService;
        private List<ApplicationModel> _allApplications;
        private List<RoomModel> _allRooms; 

        public ApplicationsWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
            _allApplications = new List<ApplicationModel>();
            _allRooms = new List<RoomModel>();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadApplications();
            await LoadRooms();
        }

        private async Task LoadApplications()
        {
            string response = await _clientService.SendRequestAsync("GET_ALL_APPS");

            _allApplications.Clear();
            ApplicationsGrid.ItemsSource = null;

            if (response.StartsWith("OK"))
            {
                string[] dataRows = response.Split('|').Skip(1).ToArray();
                foreach (string row in dataRows)
                {
                    string[] fields = row.Split(';');

                    _allApplications.Add(new ApplicationModel
                    {
                        IsSelected = false,
                        Id = int.Parse(fields[0]),
                        FullName = fields[1],
                        StudentId = fields[2],
                        Status = fields[3],
                        Reason = fields[4],
                        UserLogin = fields[5]
                    });
                }
                ApplicationsGrid.ItemsSource = _allApplications;
            }
            else if (response.StartsWith("EMPTY"))
            {

            }
            else
            {
                MessageBox.Show(response.Split('|')[1]);
            }
        }

        private async Task LoadRooms()
        {
            string response = await _clientService.SendRequestAsync("GET_ROOMS_DATA");
            _allRooms.Clear();
            RoomsComboBox.ItemsSource = null; 

            if (response.StartsWith("OK"))
            {
                string[] dataRows = response.Split('|').Skip(1).ToArray();
                foreach (string row in dataRows)
                {
                    string[] fields = row.Split(';');
                    _allRooms.Add(new RoomModel
                    {
                        RoomNumber = fields[0],
                        Capacity = int.Parse(fields[1]),
                        Occupied = int.Parse(fields[2])
                    });
                }

                var availableRooms = _allRooms
                    .Where(r => r.AvailableSeats > 0)
                    .Select(r => r.RoomNumber)
                    .ToList();

                RoomsComboBox.ItemsSource = availableRooms;
                if (availableRooms.Any())
                {
                    RoomsComboBox.SelectedIndex = 0; 
                }
            }
        }

        private async void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedApps = _allApplications.Where(app => app.IsSelected).ToList();
            if (!selectedApps.Any())
            {
                MessageBox.Show("Оберіть хоча б одну заявку.");
                return;
            }

            if (RoomsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть кімнату для поселення.");
                return;
            }

            string selectedRoom = RoomsComboBox.SelectedItem.ToString();
            string fullReport = "";

            foreach (var app in selectedApps)
            {
                string request = $"APPROVE_ASSIGN_ROOM|{app.Id}|{selectedRoom}";
                string response = await _clientService.SendRequestAsync(request);

                if (response.StartsWith("OK"))
                {
                    fullReport += $"Студент {app.FullName} - {response.Split('|')[1]}\n";
                }
                else
                {
                    fullReport += $"Студент {app.FullName} - ПОМИЛКА: {response.Split('|')[1]}\n";
                }
            }

            MessageBox.Show(fullReport, "Результат поселення");

            await LoadApplications();
            await LoadRooms();
        }

        private async void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedApps = _allApplications.Where(app => app.IsSelected).ToList();
            if (!selectedApps.Any())
            {
                MessageBox.Show("Оберіть хоча б одну заявку.");
                return;
            }

            foreach (var app in selectedApps)
            {
                string request = $"UPDATE_APP_STATUS|{app.Id}|Відхилено";
                await _clientService.SendRequestAsync(request);
            }

            MessageBox.Show("Заявки відхилено!");
            await LoadApplications(); 
        }
    }
}