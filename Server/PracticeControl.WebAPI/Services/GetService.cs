using Microsoft.AspNetCore.Mvc.Infrastructure;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.View;
using static PracticeControl.WebAPI.Converters.AttendanceConverter;
using static PracticeControl.WebAPI.Converters.PracticeScheduleConverter;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;
using static PracticeControl.WebAPI.Converters.PracticeConverter;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using PracticeControl.WebAPI.Converters;

namespace PracticeControl.WebAPI.Services
{
    public class GetService : IGetService
    {
        private readonly IGetRepository _getRepository;

        public GetService(IGetRepository getRepository)
        {
            _getRepository = getRepository;
        }

        //Возврат списка групп
        public async Task<List<GroupView>> GetGroupViews()
        {
            List<Database.Group>? groups = await _getRepository.GetGroups();

            List<GroupView>? groupViews = ConvertToListGroupView(groups);

            return groupViews;
        }

        //Возврат сотрудника при авторизации
        public async Task<EmployeeView> GetEmployee(int id)
        {
            var employee = await _getRepository.GetEmployee(id);
            var employeeView = ConvertToEmployeeView(employee);
            return employeeView;
        }

        //Возврат списка сотрудников для админа
        public async Task<List<EmployeeView>> GetEmployeeViewList()
        {
            List<Employee> employees = await _getRepository.GetEmployeeList();
            List<EmployeeView> employeeViews = ConvertToListEmployeeView(employees);

            return employeeViews;
        }

        //Возврат списка практик для админа
        public async Task<List<PracticeScheduleView>> GetPracticeScheduleViewList()
        {
            List<Practiceschedule> practiceSchedules = await _getRepository.GetPracticeScheduleList();

            List<PracticeScheduleView> practiceScheduleViews = await ConvertToPracticeScheduleView(practiceSchedules, _getRepository);

            return practiceScheduleViews;
        }

        //Возврат списка студентов группы
        public async Task<List<StudentView>> GetStudentGroup(string groupName)
        {

            List<Student> studentsGroup = await _getRepository.GetStudentsGroup(groupName);
            List<StudentView> studentsGroupViews = new List<StudentView>();

            foreach (var student in studentsGroup)
            {
                studentsGroupViews.Add(ConvertToView(student));
            }

            return studentsGroupViews;

        }

        //Возврат списка студентов
        public async Task<List<StudentView>> GetStudents()
        {
            List<Student> students = await _getRepository.GetStudents();
            List<StudentView> studentsGroupViews = new List<StudentView>();

            foreach (var student in students)
            {
                studentsGroupViews.Add(ConvertToView(student));
            }

            return studentsGroupViews;
        }

        //Возврат списка практик
        public async Task<List<PracticeView>> GetPracticeList()
        {
            List<Practice> practices = await _getRepository.GetPracticeList();
            List<PracticeView> practiceViews = practices.Select(practice => ConvertToPracticeView(practice)).ToList(); 
            
            return practiceViews;
        }

        public async Task<GroupView> GetGroupForName(string name)
        {
            Database.Group group = await _getRepository.GetGroup(name);

            return ConvertToGroupView(group);

        }
    }
}
