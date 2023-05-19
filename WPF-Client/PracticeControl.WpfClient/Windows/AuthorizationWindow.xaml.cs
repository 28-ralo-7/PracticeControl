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

        private void Auth_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(login_TextBox.Text) && string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            var userAuth = new AuthUser(login_TextBox.Text, passwordBox.Password);

            var user = Requests.Authorization(userAuth);

            if (user.Result is null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }


            MainContentWindow mainWindow = new MainContentWindow(user.Result.user);
            mainWindow.Show();
            this.Close();
            
        }
    }
}
