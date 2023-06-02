using PracticeControl.XamarinClient.API;
using PracticeControl.XamarinClient.Models;
using PracticeControl.XamarinClient.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeControl.XamarinClient
{
    public partial class App : Application
    {







        public App()
        {
            InitializeComponent();


            if (!(bool)Application.Current.Properties["IsLoggedIn"])
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
