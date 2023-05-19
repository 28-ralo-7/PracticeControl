using PracticeControl.WebAPI.Views.blanksCreate;

namespace PracticeControl.WebAPI.Interfaces.IServices
{
    public interface IPutService
    {
        Task<CreateEmployeeView> UpdateEmployee(CreateEmployeeView employee);
    }
}
