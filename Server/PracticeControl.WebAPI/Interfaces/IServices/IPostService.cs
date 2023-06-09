﻿using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IPostService
    {
        CreateEmployeeView CreateEmployee(CreateEmployeeView employeeView);
        CreateGroupView CreateGroup(CreateGroupView employeeView);
    }
}
