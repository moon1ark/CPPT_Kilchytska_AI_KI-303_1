using System.Windows;

namespace DormitoryApp.Views
{
    public partial class ReportsWindow : Window
    {
        private readonly TcpClientService _clientService;

        public ReportsWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
        }

        private async void SettlementReport_Click(object sender, RoutedEventArgs e)
        {
            ReportTextBox.Text = "Формування звіту...";

            string response = await _clientService.SendRequestAsync("GET_SETTLEMENT_REPORT");

            if (response.StartsWith("OK"))
            {
                ReportTextBox.Text = response.Split('|')[1];
            }
            else
            {
                ReportTextBox.Text = response.Split('|')[1];
            }
        }

        private void PaymentReport_Click(object sender, RoutedEventArgs e)
        {
            ReportTextBox.Text = "Функція 'Звіт по оплаті' наразі не реалізована.";
        }

        private async void AvailabilityReport_Click(object sender, RoutedEventArgs e)
        {
            ReportTextBox.Text = "Формування звіту...";

            string response = await _clientService.SendRequestAsync("GET_ROOMS_DATA");

            if (response.StartsWith("ERROR") || response.StartsWith("EMPTY"))
            {
                ReportTextBox.Text = response.Split('|')[1];
                return;
            }


            string[] dataRows = response.Split('|').Skip(1).ToArray();

            int totalCapacity = 0;
            int totalOccupied = 0;

            string report = "--- Звіт по кімнатах ---\n\n";
            report += "Кімната\tЗайнято/Всього\tВільних\n";
            report += "----------------------------------\n";

            foreach (string row in dataRows)
            {
                string[] fields = row.Split(';');
                string roomNum = fields[0];
                int capacity = int.Parse(fields[1]);
                int occupied = int.Parse(fields[2]);
                int available = capacity - occupied;

                totalCapacity += capacity;
                totalOccupied += occupied;

                report += $"{roomNum}\t{occupied}/{capacity}\t\t{available}\n";
            }

            report += "----------------------------------\n";
            report += $"Всього місць: {totalOccupied} / {totalCapacity}\n";
            report += $"Всього вільних: {totalCapacity - totalOccupied}";

            ReportTextBox.Text = report;
        }
    }
}