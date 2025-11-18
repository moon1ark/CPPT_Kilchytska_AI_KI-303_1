using System.Windows;

namespace DormitoryApp.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly TcpClientService _clientService;

        public RegisterWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string login = LoginTextBox.Text;
            string pass = UserPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(fullName))
            {
                StatusTextBlock.Text = "Всі поля є обов'язковими.";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            StatusTextBlock.Text = "Надсилання даних...";
            StatusTextBlock.Foreground = System.Windows.Media.Brushes.Wheat;

            string request = $"REGISTER|{login}|{pass}|{fullName}";
            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                StatusTextBlock.Text = "Реєстрація успішна! Можете закрити це вікно.";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.LightGreen;
            }
            else
            {
                StatusTextBlock.Text = response.Split('|')[1];
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}