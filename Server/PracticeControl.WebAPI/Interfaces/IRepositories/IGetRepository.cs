using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;

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
    }
}
