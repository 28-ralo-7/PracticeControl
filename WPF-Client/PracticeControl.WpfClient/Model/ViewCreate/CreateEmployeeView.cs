﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model.ViewCreate
{
    public class CreateEmployeeView //Для создания нового сотрудника
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
