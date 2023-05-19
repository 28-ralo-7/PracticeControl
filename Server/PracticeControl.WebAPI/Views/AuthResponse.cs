using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Views
{
    public class AuthResponse
    {
        public string token { get; set; }  
        public EmployeeView user { get; set; }
        public AuthResponse(string token, EmployeeView user)
        {
            this.token = token;
            this.user = user;
        }
    }
}
