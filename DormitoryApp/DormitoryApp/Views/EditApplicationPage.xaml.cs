using System.Windows;
using System.Windows.Controls;

namespace DormitoryApp.Views
{
    public partial class EditApplicationPage : Page
    {
        private string _currentUserLogin;

        public EditApplicationPage(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
            LoadData(); 
        }

        private void LoadData()
        {
            StatusTextBlock.Text = "Завантаження даних... (Сервер не підключено)";

            FullNameTextBox.Text = "Іванов Іван (заглушка)";
            StudentIdTextBox.Text = "KB12345 (заглушка)";
            ReasonTextBox.Text = "Пільги (заглушка)";
            StatusTextBlock.Text = "Поточні дані завантажено (заглушка).";
        }

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData(); 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "Збереження... (Сервер не підключено)";

            StatusTextBlock.Text = "Зміни збережено (заглушка)!";
        }
    }
}