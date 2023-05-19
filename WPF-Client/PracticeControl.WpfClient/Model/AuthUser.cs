using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model
{
    public class AuthUser
    {
        public string Login { get; set; } = null!;
        public string PasswordString { get; set; } = null!;

        public AuthUser(string login, string password)
        {
            Login = login;
            PasswordString = password;
        }
    }
}
