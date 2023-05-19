using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Repositories;
using PracticeControl.WebAPI.Views.blanksCreate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;

namespace PracticeControl.WebAPI.Services
{
    public class PutService : IPutService
    {
        private readonly IPutRepository _putRepository;
        public PutService(IPutRepository putRepository)
        {
            _putRepository = putRepository;
        }
        //Обновление сотрудника
        public async Task<CreateEmployeeView> UpdateEmployee(CreateEmployeeView updateEmployee)
        {
            if (updateEmployee is not null)
            {
                Employee employee = ConvertToEmployee(updateEmployee);

                if(!string.IsNullOrWhiteSpace(updateEmployee.Password))
                {
                    var salt = PasswordHelper.GetSalt();
                    var passwordHash = PasswordHelper.GetHash(salt, updateEmployee.Password);

                    employee.Passwordhash = passwordHash;
                    employee.Passwordsalt = salt;
                }

                CreateEmployeeView response = ConvertToCreateEmployeeView(
                    await _putRepository.UpdateEmployee(employee));

                return response;
            }

            return null;
        }
    }
}
