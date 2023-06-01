using PracticeControl.XamarinClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static PracticeControl.XamarinClient.API.APIService;

namespace PracticeControl.XamarinClient
{
    public partial class MainPage : ContentPage
    {
        private AuthResponseMobile User;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void login_Button_Clicked(object sender, EventArgs e)
        {

            if (login_TextBox.Text is null )
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

            DisplayAlert("Успех", "Успех", "Успех");

            WorkPage workPage = new WorkPage(); 

        }

    }
}

