using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Repositories;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksUpdate;
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
        public async Task<EmployeeView> UpdateEmployee(UpdateEmployeeView updateEmployee)
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

                EmployeeView response = ConvertToEmployeeView(
                    await _putRepository.UpdateEmployee(employee, updateEmployee.LoginForSearch));

                return response;
            }

            return null;
        }
    }
}
