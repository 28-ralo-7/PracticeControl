using Newtonsoft.Json;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Model.ViewUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.API
{
    public class UpdateRequests
    {
        public static async Task<EmployeeView> UpdateEmployeeAsync(UpdateEmployeeView updateEmployee)
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


        public static async Task<bool> UpdateStudentAsync(UpdateStudentView updateEmployee)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(updateEmployee);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7063/api/put/updateStudent/", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var studentView = JsonConvert.DeserializeObject<bool>(responseData);

            return studentView;
        }

        public static async Task<GroupView> RenameGroup(string newName, string oldName)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(newName);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7063/api/put/renameGroup/{oldName}", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var studentView = JsonConvert.DeserializeObject<GroupView>(responseData);

            return studentView;
        }



        public static async Task<bool> UpdateAttendanceAsync(List<UpdateAttendanceView> updateAttendances)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(updateAttendances);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7063/api/put/updateAttendances/", data).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var updatedAttendances = JsonConvert.DeserializeObject<bool>(responseData);

            return updatedAttendances;
        }



    }
}
