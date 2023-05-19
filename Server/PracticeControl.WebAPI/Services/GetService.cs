using Microsoft.AspNetCore.Mvc.Infrastructure;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.blanks;
using static PracticeControl.WebAPI.Converters.AttendanceConverter;
using static PracticeControl.WebAPI.Converters.PracticeScheduleConverter;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;
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
        public List<GroupView> GetGroupViews()
        {
            List<Database.Group>? groups = _getRepository.GetGroups();

            List<GroupView>? groupViews = ConvertToListGroupView(groups);

            return groupViews;
        }

        //Возврат сотрудника при авторизации
        public EmployeeView? GetEmployee(int id)
        {
            var employee = _getRepository.GetEmployee(id);
            var employeeView = ConvertToEmployeeView(employee);
            return employeeView;
        }

        //Возврат списка сотрудников для админа
        public List<EmployeeView> GetEmployeeViewList()
        {
            List<Employee> employees = _getRepository.GetEmployeeList();
            List<EmployeeView> employeeViews = ConvertToListEmployeeView(employees);

            return employeeViews;
        }

        //Возврат списка практик для админа
        public List<PracticeScheduleView> GetPracticeScheduleViewList()
        {
            List<Practiceschedule> practiceSchedules = _getRepository.GetPracticeScheduleList();

            List<PracticeScheduleView> practiceScheduleViews = ConvertToPracticeScheduleView(practiceSchedules, _getRepository);

            return practiceScheduleViews;
        }
    }
}
