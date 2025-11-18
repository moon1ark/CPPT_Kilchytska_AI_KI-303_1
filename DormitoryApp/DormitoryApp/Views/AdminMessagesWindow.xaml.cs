using System.Windows;

namespace DormitoryApp.Views
{
    public partial class AdminMessagesWindow : Window
    {
        private readonly TcpClientService _clientService;

        public AdminMessagesWindow()
        {
            InitializeComponent();
            _clientService = new TcpClientService();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string target = TargetLoginTxt.Text;
            string message = MessageTextTxt.Text;

            if (string.IsNullOrWhiteSpace(target) || string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Отримувач та текст не можуть бути порожніми.");
                return;
            }

            string request = $"SEND_MESSAGE|{target}|{message}";
            string response = await _clientService.SendRequestAsync(request);

            if (response.StartsWith("OK"))
            {
                MessageBox.Show("Повідомлення успішно надіслано!");
                this.Close();
            }
            else
            {
                MessageBox.Show(response.Split('|')[1], "Помилка");
            }
        }
    }
}