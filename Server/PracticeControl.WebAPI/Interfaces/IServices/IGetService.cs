using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;
using System.Net;

namespace PracticeControl.WebAPI.Interfaces.IServices
{

    public interface IGetService
    {
        Task<List<GroupView>> GetGroupViews();
        Task<EmployeeView> GetEmployee(int id);
        Task<List<EmployeeView>> GetEmployeeViewList();
        Task<List<PracticeScheduleView>> GetPracticeScheduleViewList();
        Task<List<StudentView>> GetStudentGroup(string groupName);
        Task<GroupView> GetGroupForName(string name);
        Task<List<StudentView>> GetStudents();
        Task<List<PracticeView>> GetPracticeList();


    }
}
