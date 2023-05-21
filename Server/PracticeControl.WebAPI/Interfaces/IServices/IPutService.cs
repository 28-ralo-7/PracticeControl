using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksUpdate;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IPutService
    {
        Task<EmployeeView> UpdateEmployee(UpdateEmployeeView employee);
    }
}
