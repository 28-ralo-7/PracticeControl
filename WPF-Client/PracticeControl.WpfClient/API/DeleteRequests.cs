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
    }
}
