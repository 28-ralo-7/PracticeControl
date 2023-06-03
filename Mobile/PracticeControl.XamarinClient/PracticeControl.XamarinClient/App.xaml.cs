using PracticeControl.XamarinClient.API;
using PracticeControl.XamarinClient.Models;
using PracticeControl.XamarinClient.Pages;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeControl.XamarinClient
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var isKey = Application.Current.Properties.Any(x=>x.Key == "IsLoggedIn");

            if (isKey)
            {
                var isLogged = Application.Current.Properties["IsLoggedIn"];

                if (!(bool)isLogged)
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    string login = Application.Current.Properties["Login"].ToString();
                    string password = Application.Current.Properties["Password"].ToString();

                    AuthRequest authUser = new AuthRequest(login, password);

                    var user = APIService.Authorization(authUser).Result;


                    MainPage = new NavigationPage(new MainContentPage(user.user));
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
