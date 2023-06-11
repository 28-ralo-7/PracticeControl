using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewMobile;

namespace PracticeControl.WebAPI.Views
{
    public class AuthResponseDesktop
    {
        public string token { get; set; }  
        public EmployeeView user { get; set; }
        public AuthResponseDesktop(string token, EmployeeView user)
        {
            this.token = token;
            this.user = user;
        }
    }

}
