using System.Windows;

namespace DormitoryApp.Views
{
    public partial class ApplicationFormWindow : Window
    {
        private string _currentUserLogin;

        private readonly TcpClientService _clientService;

        public ApplicationFormWindow(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            _clientService = new TcpClientService(); 
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {

            string fullName = NameTxt.Text;
            string studentId = IdTxt.Text;
            string reason = NoteTxt.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("ПІБ та Студентський квиток є обов'язковими.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string request = $"SUBMIT_APP|{_currentUserLogin}|{fullName}|{studentId}|{reason}";

            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                MessageBox.Show("Заявку успішно подано! Тепер адмін може її переглянути.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(response.Split('|')[1], "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}