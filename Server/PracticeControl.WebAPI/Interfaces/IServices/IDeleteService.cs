using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IDeleteService
    {

        Task<EmployeeView> DeleteEmployee(string login);

    }
}
