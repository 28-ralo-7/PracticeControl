using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IGetRepository
    {
        //Student? GetStudent(int id);
        //List<Student> GetStudents();
       //List<Attendance> GetAttendance();
        //Practiceschedule? GetPracticeschedule(int id);
        List<Group>? GetGroups();
        //Practice? GetPractice(int id);
        Employee? GetEmployee(int id);
        List<Employee> GetEmployeeList();
        List<Practiceschedule> GetPracticeScheduleList();
    }
}
