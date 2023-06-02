using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model;
using PracticeControl.WpfClient.Model.View;
using System.Windows;

namespace PracticeControl.WpfClient.Windows
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private async void Auth_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(login_TextBox.Text) && string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            var userAuth = new AuthUser(login_TextBox.Text, passwordBox.Password);

            try
            {
                var user = await GetRequests.Authorization(userAuth);

                if (user is null)
                {
                    MessageBox.Show("Неверный логин или пароль", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MainContentWindow mainWindow = new MainContentWindow(user.user);
                mainWindow.Show();
                this.Close();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Ошибка авторизации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void close_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
