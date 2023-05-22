using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Repositories;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksUpdate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;

namespace PracticeControl.WebAPI.Services
{
    public class PutService : IPutService
    {
        private readonly IPutRepository _putRepository;
        private readonly IGetRepository _getRepository;
        public PutService(IPutRepository putRepository, IGetRepository getRepository)
        {
            _putRepository = putRepository;
            _getRepository = getRepository;
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

        public async Task<StudentView> UpdateStudent(UpdateStudentView updateStudent)
        {
            if(updateStudent is not null)
            {
                Student student = ConvertToStudent(updateStudent);

                var group = _getRepository.GetGroup(updateStudent.GroupName).Result as Group;

                student.IdGroup = Convert.ToInt32(group.Id);

                if (!string.IsNullOrWhiteSpace(updateStudent.Password))
                {
                    var salt = PasswordHelper.GetSalt();
                    var passwordHash = PasswordHelper.GetHash(salt, updateStudent.Password);

                    student.Passwordhash = passwordHash;
                    student.Passwordsalt = salt;
                }

                StudentView response = ConvertToView(
                    await _putRepository.UpdateStudent(student, updateStudent.LoginForSearch));

                return response;
            }

            return null;
        }
    }
}
