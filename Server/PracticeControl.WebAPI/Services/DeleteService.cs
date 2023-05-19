using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.blanks;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;

namespace PracticeControl.WebAPI.Services
{
    public class DeleteService : IDeleteService
    {
        private readonly IDeleteRepository _deleteRepository;
        public DeleteService(IDeleteRepository deleteRepository)
        {
            _deleteRepository= deleteRepository;
        }
        public async Task<EmployeeView> DeleteEmployee(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            Employee? employee = await _deleteRepository.DeleteEmployee(login);

            if (employee is null)
            {
                return null;
            }

            EmployeeView employeeView = ConvertToEmployeeView(employee);

            return employeeView;

        }
    }
}
