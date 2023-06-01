using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeControl.XamarinClient.Models
{
        public class AuthResponseMobile
        {
            public string token { get; set; }
            public StudentViewMobile user { get; set; }
            public AuthResponseMobile(string token, StudentViewMobile user)
            {
                this.token = token;
                this.user = user;
            }
        
        }
}
