using System.Windows;

namespace DormitoryApp.Views
{
    public partial class MainWindow : Window
    {
        private string _currentUserLogin;

        private SubmitApplicationPage _submitPage;
        private EditApplicationPage _editPage;
        private CheckStatusPage _statusPage;

        public MainWindow(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            WelcomeTextBlock.Text = $"Вітаємо, {_currentUserLogin}!";

            _submitPage = new SubmitApplicationPage(_currentUserLogin);
            _editPage = new EditApplicationPage(_currentUserLogin);
            _statusPage = new CheckStatusPage(_currentUserLogin);

            MainFrame.Navigate(_statusPage);
        }

        private void SubmitPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_submitPage);
        }

        private void EditPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_editPage);
        }

        private void StatusPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_statusPage);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}