using PracticeControl.WpfClient.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model
{
    public class User
    {
        public string token { get; set; }
        public EmployeeView user { get; set; }

        public User(string token, EmployeeView user)
        {
            this.token = token;
            this.user = user;
        }
    }
}
