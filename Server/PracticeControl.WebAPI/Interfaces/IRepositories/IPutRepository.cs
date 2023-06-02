using PracticeControl.WebAPI.Database;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IPutRepository
    {
        Task<Employee> UpdateEmployee(Employee employee, string loginSearch);
        Task<Student> UpdateStudent(Student student, string loginSearch);
        Task<Group> UpdateGroup(string oldName, string nameSearch);
        bool UpdateAttendance(List<Attendance> attendances);
        bool UpdatePractice(Practice practice);
    }
}
