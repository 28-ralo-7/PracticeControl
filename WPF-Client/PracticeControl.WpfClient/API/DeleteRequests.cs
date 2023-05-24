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
    }
}
