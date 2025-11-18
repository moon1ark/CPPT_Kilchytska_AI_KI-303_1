using System.Windows;
using System.Linq;

namespace DormitoryApp.Views
{
    public partial class MessagesWindow : Window
    {
        private string _currentUserLogin;
        private readonly TcpClientService _clientService;

        public MessagesWindow(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            _clientService = new TcpClientService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string request = $"GET_MESSAGES|{_currentUserLogin}";
            string response = await _clientService.SendRequestAsync(request);

            MessagesListBox.Items.Clear(); 

            if (response.StartsWith("OK"))
            {
                string[] messages = response.Split('|').Skip(1).ToArray();
                foreach (string msg in messages)
                {
                    MessagesListBox.Items.Add(msg.Replace(":", ": "));
                }
            }
            else if (response.StartsWith("EMPTY"))
            {
                MessagesListBox.Items.Add(response.Split('|')[1]); 
            }
            else
            {
                MessagesListBox.Items.Add(response.Split('|')[1]); 
            }
        }
    }
}