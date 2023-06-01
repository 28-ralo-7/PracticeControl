using Newtonsoft.Json;
using PracticeControl.XamarinClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracticeControl.XamarinClient.API
{
    public static class APIService
    {
        public static async Task<AuthResponseMobile> Authorization(AuthRequest auth) 
        {
            try
            {
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(auth);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client
                    .PostAsync("http://192.168.1.228:3000/api/auth/authorization/", content)
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var data = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<AuthResponseMobile>(data);

                if (user is null)
                {
                    return null;
                }

                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["Login"] = user.user.Login;
                Application.Current.Properties["Password"] = auth.PasswordString;

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
