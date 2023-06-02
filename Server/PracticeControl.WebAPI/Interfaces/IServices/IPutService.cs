using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewUpdate;


namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IPutService
    {
        Task<EmployeeView> UpdateEmployee(UpdateEmployeeView employee);
        Task<bool> UpdateStudent(UpdateStudentView student);
        Task<GroupView> UpdateGroup(string oldName, string groupName);
        Task<bool> UpdateAttendance(List<UpdateAttendanceView> attendanceView);
        Task<bool> UpdatePractice(PracticeView practiceView);
    }
}
