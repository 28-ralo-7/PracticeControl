using PracticeControl.XamarinClient.API;
using PracticeControl.XamarinClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeControl.XamarinClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void buttonLogIn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryLogin.Text) || string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                await DisplayAlert("Ошибка авторизации", "Заполните оба поля", "ОК");

                return;
            }

            AuthRequest authUser = new AuthRequest(entryLogin.Text, entryPassword.Text);

            var user = await APIService.Authorization(authUser);

            if (user is null)
            {
                await DisplayAlert("Ошибка авторизации", "Неверный логин или пароль", "ОК");
                return;
            }

            await Navigation.PushAsync(new MainContentPage(user.user));
        }

    }
}