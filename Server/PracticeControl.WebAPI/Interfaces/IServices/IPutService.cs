using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksUpdate;


namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IPutService
    {
        Task<EmployeeView> UpdateEmployee(UpdateEmployeeView employee);
        Task<bool> UpdateStudent(UpdateStudentView student);
        Task<GroupView> UpdateGroup(string oldName, string groupName);
    }
}
