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
    public class Requests
    {
        public static async Task<User?> Authorization(AuthUser systemUserAuth)
        {
            HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(systemUserAuth);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7063/api/auth/authorization/", content).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<User>(data);

            if (user is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(user.token))
            {
                Properties.Settings.Default.JWTToken = user.token;
                Properties.Settings.Default.Save();
            }

            return user;
        }

        public static async Task<List<GroupView>?> GetGroupsAsync()
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var response = await client.GetAsync("https://localhost:7063/api/get/getGroups/").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadAsStringAsync();
            var allGroups = JsonConvert.DeserializeObject<List<GroupView>>(data);

            return allGroups;
        }//Готово

        public static async Task<List<EmployeeView>?> GetAllEmployeesAsync()
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var response = await client.GetAsync("https://localhost:7063/api/get/getEmployeeList/").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadAsStringAsync();
            var allEmployees = JsonConvert.DeserializeObject<List<EmployeeView>>(data);

            return allEmployees;
        }//Готово


        public static async Task<List<PracticeScheduleView>?> GetAllPracticesAsync()
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var response = await client.GetAsync("https://localhost:7063/api/get/getPracticeScheduleList/").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadAsStringAsync();

            var allPractices = JsonConvert.DeserializeObject<List<PracticeScheduleView>>(data);

            return allPractices;
        }//Готово


        public static async Task<List<PracticeScheduleView>> GetPracticesLeadAsync(int PracticeLeadId)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var response = await client.GetAsync("GET-Practice-LeadID");

            var data = await response.Content.ReadAsStringAsync();

            var PracticesLead = JsonConvert.DeserializeObject<List<PracticeScheduleView>>(data);

            return PracticesLead;
        }

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

        public static async Task<EmployeeView> DeleteEmployeeAsync(string login)
        {
            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync($"https://localhost:7063/api/delete/deleteEmployee/{login}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var employeeView = JsonConvert.DeserializeObject<EmployeeView>(responseData);

            return employeeView;

        }
        public static async Task<EmployeeView> UpdateEmployeeAsync(CreateEmployeeView updateEmployee)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(updateEmployee);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7063/api/put/updateEmployee/", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var employeeView = JsonConvert.DeserializeObject<EmployeeView>(responseData);

            return employeeView;
        }
    
    
    }
}
