using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;


namespace PracticeControl.WebAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        //Добавление сотрудника
        public CreateEmployeeView CreateEmployee(CreateEmployeeView employeeView)
        {
            if (employeeView is not null) 
            {
                Employee employee = ConvertToEmployee(employeeView);

                var salt = PasswordHelper.GetSalt();
                var passwordHash = PasswordHelper.GetHash(salt, employeeView.Password);

                employee.Passwordhash = passwordHash;
                employee.Passwordsalt = salt;
                CreateEmployeeView response = ConvertToCreateEmployeeView(_postRepository.CreateEmployee(employee));

                return response;
            }

            return null;
        }
        //Добавление группы
        public CreateGroupView CreateGroup(CreateGroupView groupView)
        {
            if (groupView is not null)
            {
                //Group group = ConvertToEmployee(groupView);

                //var salt = PasswordHelper.GetSalt();
                //var passwordHash = PasswordHelper.GetHash(salt, employeeView.Password);

                //employee.Passwordhash = passwordHash;
                //employee.Passwordsalt = salt;
                //CreateEmployeeView response = ConvertToCreateEmployeeView(_postRepository.CreateEmployee(employee));

                //return response;
            }

            return null;
        }
    }
}
