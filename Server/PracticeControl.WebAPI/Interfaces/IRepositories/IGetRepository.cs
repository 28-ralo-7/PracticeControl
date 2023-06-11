using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewMobile;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IGetRepository
    {
        Task<List<Group>> GetGroups();
        Task<Employee> GetEmployee(int id);
        Task<List<Employee>> GetEmployeeList();
        Task<List<Practiceschedule>> GetPracticeScheduleList();
        Task<List<Student>> GetStudentsGroup(string groupName);
        Task<List<Student>> GetStudents();
        Task<Group> GetGroup(string name);
        Task<Practice> GetPractice(string name);
        Task<Employee> GetEmployee(string name);
        Task<List<Practice>> GetPracticeList();
        Task<Attendance> GetAttendance(int idStudent, DateOnly date);

    }
}
