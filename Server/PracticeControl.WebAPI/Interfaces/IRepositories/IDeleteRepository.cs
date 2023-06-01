using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IDeleteRepository
    {
        Task<Employee> DeleteEmployee(string login);
        Task<Student> DeleteStudent(string login);
        Task<Group> DeleteGroup(string name);
    }
}
