using Microsoft.AspNetCore.Identity;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;


namespace PracticeControl.WebAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IGetRepository _getRepository;
        public PostService(IPostRepository postRepository, IGetRepository getRepository)
        {
            _postRepository = postRepository;
            _getRepository = getRepository;
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
        public bool CreateGroup(CreateGroupView createGroupView)
        {
            if (createGroupView is not null)
            {
                Group group = ConvertToGroup(createGroupView);

                var groupCreated = _postRepository.CreateGroup(group);

                if (groupCreated is not null)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        //Добавление студента
        public CreateStudentView CreateStudent(CreateStudentView createStudentView)
        {
            if (createStudentView is not null)
            {
                Student student = ConvertToStudent(createStudentView);
                

                student.IdGroup = _getRepository.GetGroup(createStudentView.GroupName).Result.Id;

                var salt = PasswordHelper.GetSalt();
                var passwordHash = PasswordHelper.GetHash(salt, createStudentView.Password);

                student.Passwordhash = passwordHash;
                student.Passwordsalt = salt;

                CreateStudentView studentView = ConvertToCreateView(_postRepository.CreateStudent(student));

                return studentView;

            }

            return null;

        }

        public bool CreatePracticeSchedule(CreatePracticeSchedule schedule)
        {
            Practiceschedule practiceschedule = new Practiceschedule
            {
                IdPractice = schedule.PracticeID,
                IdGroup = schedule.GroupID,
                IdEmployee = schedule.EmployeeId,
                Startdate = DateOnly.Parse(schedule.StartDate.ToShortDateString()),
                Enddate = DateOnly.Parse(schedule.EndDate.ToShortDateString()),
                
            };

            var response = _postRepository.CreatePracticeSchedule(practiceschedule);
            return response;
        }
    }

}
