using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;
using System.Net;

namespace PracticeControl.WebAPI.Interfaces.IServices
{

    public interface IGetService
    {
        List<GroupView> GetGroupViews();
        EmployeeView? GetEmployee(int id);
        List<EmployeeView> GetEmployeeViewList();
        List<PracticeScheduleView> GetPracticeScheduleViewList();
    }
}
