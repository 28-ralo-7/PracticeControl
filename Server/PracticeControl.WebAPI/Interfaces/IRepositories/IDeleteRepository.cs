using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IDeleteRepository
    {
        Task<Employee> DeleteEmployee(string login);
    }
}
