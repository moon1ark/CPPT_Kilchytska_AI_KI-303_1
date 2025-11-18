using System.Windows;

namespace DormitoryApp.Views
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void OpenApps_Click(object sender, RoutedEventArgs e) => new ApplicationsWindow().Show();
        private void OpenResidents_Click(object sender, RoutedEventArgs e) => new ResidentsListWindow().Show();
        private void OpenAdminMsg_Click(object sender, RoutedEventArgs e) => new AdminMessagesWindow().Show();
        private void OpenReports_Click(object sender, RoutedEventArgs e) => new ReportsWindow().Show();

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}