using System;
using Xamarin.Forms;

namespace PracticeControl.XamarinForm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private async void login_Button_Clicked(object sender, EventArgs e)
        {
            if (login_TextBox.Text is null)
            {
                DisplayAlert("Уведомление", "Введите логин", "ОК");
                return;
            }
            if (password_TextBox.Text is null)
            {
                DisplayAlert("Уведомление", "Введите пароль ", "ОК");
                return;
            }

            AuthRequest authForm = new AuthRequest(login_TextBox.Text, password_TextBox.Text);
            User = await Authorization(authForm);

            if (User is null)
            {
                DisplayAlert("Предупреждение", "Неверный логин или пароль", "ОК");
                return;
            }
    }
}