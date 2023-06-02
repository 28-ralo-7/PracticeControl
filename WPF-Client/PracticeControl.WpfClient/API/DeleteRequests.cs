using Newtonsoft.Json;
using PracticeControl.WpfClient.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.API
{
    public class DeleteRequests
    {
        //Удаление сотрудника
        public static async Task<bool> DeleteEmployeeAsync(string login)
        {
            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync($"https://localhost:7063/api/delete/deleteEmployee/{login}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var employeeView = JsonConvert.DeserializeObject<bool>(responseData);

            return employeeView;

        }
        //Удаление струдента
        public static async Task<StudentView> DeleteStudentAsync(string login)
        {
            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync($"https://localhost:7063/api/delete/deleteStudent/{login}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var studentView = JsonConvert.DeserializeObject<StudentView>(responseData);

            return studentView;
        }

        //Удаление группы
        public static async Task<StudentView> DeleteGroupAsync(string name)
        {
            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync($"https://localhost:7063/api/delete/deleteGroup/{name}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var studentView = JsonConvert.DeserializeObject<StudentView>(responseData);

            return studentView;

        }

        //Удаление практики
        public static async Task<bool> DeletePracticeAsync(int id)
        {
            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync($"https://localhost:7063/api/delete/deletePractice/{id}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(responseData);

            return isDeleted;

        }


        //Удаление расписание практики
        public static async Task<bool> DeletePracticeScheduleAsync(PracticeScheduleView practiceScheduleView)
        {
            HttpClient client = new HttpClient();

            var response = await client
                .DeleteAsync($"https://localhost:7063/api/delete/deletePracticeSchedule/{practiceScheduleView.PracticeScheduleID}")
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var isDelete = JsonConvert.DeserializeObject<bool>(responseData);

            return isDelete;

        }
    }
}
