using Microsoft.AspNetCore.Identity;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewCreate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;
using static PracticeControl.WebAPI.Converters.PracticeConverter;
using static PracticeControl.WebAPI.Converters.PracticeScheduleConverter;

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

        //Сотрудник
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

        //Группа
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

        //Студент
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

        //Практика
        public bool CreatePracticeSchedule(CreatePracticeScheduleView schedule)
        {
            var test = _getRepository.GetPractice(schedule.PracticeName).Result.Id;
            Practiceschedule practiceschedule = new Practiceschedule
            {
                IdPractice = _getRepository.GetPractice(schedule.PracticeName).Result.Id,
                IdGroup = _getRepository.GetGroup(schedule.GroupName).Result.Id,
                IdEmployee = _getRepository.GetEmployee(schedule.PracticeLead).Result.Id,
                Startdate = DateOnly.Parse(schedule.DateStart),
                Enddate = DateOnly.Parse(schedule.DateEnd),
            };

            var response = _postRepository.CreatePracticeSchedule(practiceschedule);
            return response;
        }

        #region Блок проверки уникальности

        //Практика
        public Task<bool> CheckUnique(PracticeView practiceView)
        {
            Practice practice = ConvertToPractice(practiceView);

            var isExist = _postRepository.CheckUnique(practice);

            return isExist;
        }

        //Расписание
        public async Task<bool> CheckUnique(PracticeScheduleView practiceScheduleView)
        {
            Practiceschedule practiceschedule = await ConvertToPracticeSchedule(practiceScheduleView);

            var isExist = await _postRepository.CheckUnique(practiceschedule);

            return isExist;

        }

        //Группа
        public async Task<bool> CheckUniqueGroup(string groupName)
        {

            var isExist = await _postRepository.CheckUniqueGroup(groupName);

            return isExist;
        }

        //Сотрудник
        public async Task<bool> CheckUnique(string login)
        {

            var isExist = await _postRepository.CheckUnique(login);

            return isExist;
        }
        
        //Студент
        public Task<bool> CheckUniqueStudent(string login)
        {

            var isExist = _postRepository.CheckUniqueStudent(login);
            
            return isExist;
        }
        #endregion

        public bool CheckValidDateForPractice(CreatePracticeScheduleView createPracticeView)
        {
            List<Practiceschedule> practiceScheduleFromDb = _getRepository.GetPracticeScheduleList().Result
                .Where(practice => practice.IdGroupNavigation.Name == createPracticeView.GroupName).ToList();

            if (practiceScheduleFromDb.Count() == 0)
            {
                return true;
            }

            DateOnly dateStart = DateOnly.Parse(createPracticeView.DateStart);
            DateOnly dateEnd = DateOnly.Parse(createPracticeView.DateEnd);

            foreach (Practiceschedule practice in practiceScheduleFromDb)
            {
                if (practice.Startdate >= dateStart && practice.Enddate <= dateEnd) return false;

                if(practice.Startdate <= dateStart &&  practice.Enddate >= dateStart) return false;

                if(practice.Startdate <= dateEnd &&  practice.Enddate >= dateEnd) return false;

            }
            return true;
        
        }

        public bool CreatePractice(CreatePracticeView practiceView)
        {
            Practice practice = ConvertToPractice(practiceView);

            var isCreated = _postRepository.CreatePractice(practice);

            return isCreated;
        }
    }

}
