using PracticeControl.WebAPI.Database;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IPutRepository
    {
        Task<Employee> UpdateEmployee(Employee employee, string loginSearch);
    }
}
