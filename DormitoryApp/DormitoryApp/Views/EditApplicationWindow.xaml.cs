using System.Windows;

namespace DormitoryApp.Views
{
    public partial class EditApplicationWindow : Window
    {
        private string _currentUserLogin;
        private readonly TcpClientService _clientService;

        public EditApplicationWindow(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            _clientService = new TcpClientService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string request = $"GET_APP_DATA|{_currentUserLogin}";
            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                string[] data = response.Split('|');
                NameTxt.Text = data[1];
                IdTxt.Text = data[2];
                NoteTxt.Text = data[3];
            }
            else
            {
                MessageBox.Show(response.Split('|')[1], "Помилка завантаження", MessageBoxButton.OK, MessageBoxImage.Error);
                NameTxt.Text = "Помилка";
                IdTxt.Text = "Помилка";
                NoteTxt.Text = "Помилка";
                NameTxt.IsEnabled = false;
                IdTxt.IsEnabled = false;
                NoteTxt.IsEnabled = false;
            }
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            string request = $"UPDATE_APP|{_currentUserLogin}|{NameTxt.Text}|{IdTxt.Text}|{NoteTxt.Text}";
            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                MessageBox.Show(response.Split('|')[1], "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(response.Split('|')[1], "Помилка збереження", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}