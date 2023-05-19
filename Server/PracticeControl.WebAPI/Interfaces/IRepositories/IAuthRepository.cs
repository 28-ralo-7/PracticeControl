using PracticeControl.WebAPI.Database;

namespace PracticeControl.WebAPI.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Employee? GetEmployee(string login);
    }
}
