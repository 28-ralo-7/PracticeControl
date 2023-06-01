using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewCreate;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IPostService
    {
        CreateEmployeeView CreateEmployee(CreateEmployeeView employeeView);
        CreateStudentView CreateStudent(CreateStudentView createStudentView);
        bool CreateGroup(CreateGroupView employeeView);
        bool CreatePracticeSchedule(CreatePracticeView schedule);



        Task<bool> CheckUnique(PracticeView practiceView);
        Task<bool> CheckUnique(PracticeScheduleView practiceScheduleView);
        Task<bool> CheckUniqueGroup(string groupName);
        Task<bool> CheckUnique(string login);
        Task<bool> CheckUniqueStudent(string login);



        bool CheckValidDateForPractice(CreatePracticeView createPracticeView);
    }
}
