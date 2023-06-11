using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IDeleteService
    {

        Task<bool> DeleteEmployee(string login);
        Task<StudentView> DeleteStudent(string login);
        Task<GroupView> DeleteGroup(string name);
        Task<bool> DeletePracice(int id);
        Task<bool> DeletePracticeSchedule(int id);

    }
}
