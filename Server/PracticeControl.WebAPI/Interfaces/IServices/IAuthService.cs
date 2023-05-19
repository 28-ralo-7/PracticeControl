using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IAuthService
    {
        AuthResponse Authenticate(string login, string password);
        string CreateToken(Employee employee);
    }
}
