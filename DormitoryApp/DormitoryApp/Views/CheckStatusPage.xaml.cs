using System.Windows;
using System.Windows.Controls;

namespace DormitoryApp.Views
{
    public partial class CheckStatusPage : Page
    {
        private string _currentUserLogin;

        public CheckStatusPage(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            CheckStatus();
        }

        private void CheckStatusButton_Click(object sender, RoutedEventArgs e)
        {
            CheckStatus();
        }

        private void CheckStatus()
        {
            StatusResultTextBlock.Text = "Оновлення... (Сервер не підключено)";

            StatusResultTextBlock.Text = "Ваш статус: В обробці (заглушка)";
        }
    }
}