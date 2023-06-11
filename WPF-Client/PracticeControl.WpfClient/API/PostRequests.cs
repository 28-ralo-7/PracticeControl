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
using DocumentFormat.OpenXml.Office2016.Excel;

namespace PracticeControl.WpfClient.API
{
    public class PostRequests
    {
        //Сотрудник
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

        //Группа
        public static async Task<bool?> CreateGroupAsync(CreateGroupView? newGroup)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

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
        }//Готово

        //Студент
        public static async Task<bool?> CreateStudentAsync(CreateStudentView? newStudent)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

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
        }//Готово

        //Расписание
        public static async Task<bool> CreatePracticeSchedule(CreatePracticeScheduleView practiceView)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(practiceView);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7063/api/post/createPracticeSchedule", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var createdGroup = JsonConvert.DeserializeObject<bool>(responseData);

            return createdGroup;
        }//Готово

        //Практика
        public static async Task<bool> CreatePractice(PracticeView practiceView)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(practiceView);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7063/api/post/createPractice", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var createdGroup = JsonConvert.DeserializeObject<bool>(responseData);

            return createdGroup;
        }//Готово

        #region Уникальность
        //Проверка уникальности практики
        public static async Task<bool> CheckUnique(PracticeView practiceView)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(practiceView);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client
                .PostAsync("https://localhost:7063/api/post/checkUniquePractice", data)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var isExist = JsonConvert.DeserializeObject<bool>(responseData);

            return isExist;
        }

        //Проверка уникальности сотрудника
        public static async Task<bool> CheckUnique(string login)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client
                .PostAsync("https://localhost:7063/api/post/checkUniqueEmployee", data)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var isExist = JsonConvert.DeserializeObject<bool>(responseData);

            return isExist;
        }

        //Проверка уникальности группы
        public static async Task<bool> CheckUniqueGroup(string groupName)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(groupName);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client
                .PostAsync("https://localhost:7063/api/post/checkUniqueGroup", data)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var isExist = JsonConvert.DeserializeObject<bool>(responseData);

            return isExist;
        }

        //Проверка уникальности студенты
        public static async Task<bool> CheckUniqueStudent(string login)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client
                .PostAsync("https://localhost:7063/api/post/checkUniqueStudent", data)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var isExist = JsonConvert.DeserializeObject<bool>(responseData);

            return isExist;
        }
        #endregion
        //Проверка доступности дат
        public static async Task<bool> CheckValidDateForPractice(CreatePracticeScheduleView practice)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTToken);

                var json = JsonConvert.SerializeObject(practice);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client
                    .PostAsync("https://localhost:7063/api/post/checkValidDateForPractice", data)
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var isValid = JsonConvert.DeserializeObject<bool>(responseData);

                return isValid;
            }
            catch
            {
                return false;
            }
        }



    }
}
