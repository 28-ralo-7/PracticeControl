using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views;
using PracticeControl.WebAPI.Views.ViewMobile;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDesktop> Authorize(string login, string password);
        Task<AuthResponseMobile> Authorize(Views.ViewMobile.AuthRequest parameters);
        string CreateToken(Employee employee);
    }
}
