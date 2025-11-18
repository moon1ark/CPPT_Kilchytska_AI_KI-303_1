using System.Windows;

namespace DormitoryApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly TcpClientService _clientService;
        private string _currentUserLogin; 

        public LoginWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string pass = UserPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
            {
                StatusTextBlock.Text = "Логін та пароль не можуть бути порожніми.";
                return;
            }

            StatusTextBlock.Text = "Підключення до сервера...";
            StatusTextBlock.Foreground = System.Windows.Media.Brushes.Wheat;

            string request = $"LOGIN|{login}|{pass}";
            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                string role = response.Split('|')[1]; 
                _currentUserLogin = login; 
                if (role == "Admin")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
                else 
                {
                    StudentWindow studentWindow = new StudentWindow(_currentUserLogin);
                    studentWindow.Show();
                    this.Close();
                }
            }
            else
            {
                StatusTextBlock.Text = response.Split('|')[1];
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Owner = this;
            regWindow.ShowDialog();
        }
    }
}