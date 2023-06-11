using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeControl.WebAPI.Views.ViewMobile
{
    public class AuthRequest
    {
        public string Login { get; set; }
        public string PasswordString { get; set; }

        public AuthRequest(string Login, string PasswordString)
        {
            this.Login = Login;
            this.PasswordString = PasswordString;
        }
    }
}
