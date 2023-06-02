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

        public static async Task<CurrentPracticeStudentView> GetPracticeGroupAsync(string groupName)
         {
            try
            {
                HttpClient client = new HttpClient();

                var response = await client
                    .GetAsync($"https://littletealhouse37.conveyor.cloud/api/get/practiceGroup/{groupName}")
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var data = await response.Content.ReadAsStringAsync();

                var practice = JsonConvert.DeserializeObject<CurrentPracticeStudentView>(data);

                if (practice is null)
                {
                    return null;
                }

                return practice;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static async Task<bool> UpdateAttendanceAsync(StudentAttendanceView attendance)
        {
            try
            {
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(attendance);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client
                    .PutAsync("https://littletealhouse37.conveyor.cloud/api/put/studentAttendance/", content)
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var data = await response.Content.ReadAsStringAsync();

                var attendanceUpdate = JsonConvert.DeserializeObject<bool>(data);

                return attendanceUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static async Task<AuthResponseMobile> Authorization(AuthRequest auth) 
        {
            try
            {
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(auth);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client
                    .PostAsync("https://littletealhouse37.conveyor.cloud/api/auth/authorizationMobile/", content)
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
