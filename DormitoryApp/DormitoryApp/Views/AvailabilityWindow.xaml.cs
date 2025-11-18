using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace DormitoryApp.Views
{
    public partial class AvailabilityWindow : Window
    {
        private readonly TcpClientService _clientService;
        private List<RoomModel> _allRooms;

        public AvailabilityWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string response = await _clientService.SendRequestAsync("GET_ROOMS_DATA");

            if (response.StartsWith("ERROR") || response.StartsWith("EMPTY"))
            {
                MessageBox.Show(response.Split('|')[1]);
                return;
            }


            _allRooms = new List<RoomModel>();
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

            RoomsGrid.ItemsSource = _allRooms.Where(r => r.AvailableSeats > 0).ToList();
        }
    }
}