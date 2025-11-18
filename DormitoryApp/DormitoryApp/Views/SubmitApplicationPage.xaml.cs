using System.Windows;
using System.Windows.Controls;

namespace DormitoryApp.Views
{
    public partial class SubmitApplicationPage : Page
    {
        private string _currentUserLogin;

        public SubmitApplicationPage(string userLogin)
        {
            InitializeComponent();
            _currentUserLogin = userLogin;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullNameTextBox.Text) || string.IsNullOrWhiteSpace(StudentIdTextBox.Text))
                {
                    SubmitStatusTextBlock.Text = "ПІБ та номер студентського є обов'язковими.";
                    return;
                }

                SubmitStatusTextBlock.Text = "Надсилання... (Сервер не підключено)";

                SubmitStatusTextBlock.Text = "Заявку успішно подано (заглушка)!";
            }
            catch (System.Exception ex)
            {
                SubmitStatusTextBlock.Text = $"Виникла помилка: {ex.Message}";
            }
        }
    }
}