using System.Windows;

namespace DormitoryApp.Views
{
    public partial class StudentWindow : Window
    {
        private string _currentUserLogin; 

        public StudentWindow(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;

            WelcomeLabel.Content = $"Вітаємо, {_currentUserLogin}!"; 
        }


        private void OpenForm_Click(object sender, RoutedEventArgs e)
        {
            new ApplicationFormWindow(_currentUserLogin).ShowDialog();
        }

        private void OpenStatus_Click(object sender, RoutedEventArgs e)
        {
            new ApplicationStatusWindow(_currentUserLogin).ShowDialog();
        }

        private void OpenEdit_Click(object sender, RoutedEventArgs e)
        {
            new EditApplicationWindow(_currentUserLogin).ShowDialog();
        }

        private void OpenAvailability_Click(object sender, RoutedEventArgs e)
        {
            new AvailabilityWindow().ShowDialog();
        }

        private void OpenMessages_Click(object sender, RoutedEventArgs e)
        {
            new MessagesWindow(_currentUserLogin).ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}