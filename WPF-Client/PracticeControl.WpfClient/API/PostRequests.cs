using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PracticeControl.WpfClient.Model;
using PracticeControl.WpfClient.Model.View;
using System.Data.SqlTypes;
using PracticeControl.WpfClient.Model.ViewCreate;

namespace PracticeControl.WpfClient.API
{
    public class PostRequests
    {
        #region Employee
        public static async Task<CreateEmployeeView> CreateEmoloyeesAsync(CreateEmployeeView newEmployee)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(newEmployee);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7063/api/post/createEmployee/", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var createdEmployee = JsonConvert.DeserializeObject<CreateEmployeeView>(responseData);

            return createdEmployee;
        } //Готово


        #endregion

        #region Group
        public static async Task<bool?> CreateGroupAsync(CreateGroupView? newGroup)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(newGroup);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7063/api/post/createGroup", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var createdGroup = JsonConvert.DeserializeObject<bool>(responseData);

            return createdGroup;
        }
        #endregion

        public static async Task<bool?> CreateStudentAsync(CreateStudentView? newStudent)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(newStudent);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7063/api/post/createStudent", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var createdGroup = JsonConvert.DeserializeObject<bool>(responseData);

            return createdGroup;
        }


    }
}
