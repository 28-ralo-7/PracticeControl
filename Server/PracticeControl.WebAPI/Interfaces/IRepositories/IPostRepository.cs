using PracticeControl.WebAPI.Database;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IPostRepository
    {
        Employee CreateEmployee(Employee employee);
        Group CreateGroup(Group group);
        Student CreateStudent(Student student);
        bool CreatePracticeSchedule(Practiceschedule schedule);
        bool CreatePractice(Practice practice);


        Task<bool> CheckUnique(Practice practice);
        Task<bool> CheckUnique(Practiceschedule practiceSchedule);
        Task<bool> CheckUniqueGroup(string groupName);
        Task<bool> CheckUnique(string login);
        Task<bool> CheckUniqueStudent(string login);
    }
}
