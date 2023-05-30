using PracticeControl.WebAPI.Database;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IPostRepository
    {
        Employee CreateEmployee(Employee employee);
        Group CreateGroup(Group group);
        Student CreateStudent(Student student);
        bool CreatePracticeSchedule(Practiceschedule schedule);
    }
}
