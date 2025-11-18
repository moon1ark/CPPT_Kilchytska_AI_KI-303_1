using System.Windows;
using System.Windows.Media;

namespace DormitoryApp.Views
{
    public partial class ApplicationStatusWindow : Window
    {
        private string _currentUserLogin;
        private readonly TcpClientService _clientService;

        public ApplicationStatusWindow(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            _clientService = new TcpClientService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckStatus();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            CheckStatus();
        }

        private async void CheckStatus()
        {
            StatusTextBlock.Text = "Оновлення...";
            StatusTextBlock.Foreground = Brushes.Wheat;

            string request = $"GET_STATUS|{_currentUserLogin}";
            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                string status = response.Split('|')[1];
                StatusTextBlock.Text = status;

                if (status.StartsWith("Поселено")) 
                    StatusTextBlock.Foreground = Brushes.LightGreen;
                else if (status == "Відхилено")
                    StatusTextBlock.Foreground = Brushes.Red;
                else 
                    StatusTextBlock.Foreground = Brushes.Orange;
            }
            else
            {
                StatusTextBlock.Text = response.Split('|')[1];
                StatusTextBlock.Foreground = Brushes.Red;
            }
        }
    }
}