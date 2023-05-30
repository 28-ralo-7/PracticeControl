using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Repositories;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksUpdate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PracticeControl.WebAPI.Services
{
    public class PutService : IPutService
    {
        private readonly IPutRepository _putRepository;
        private readonly IGetRepository _getRepository;

        private byte[] PasswordSalt { get; set; }
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

        //Обновление студента
        public async Task<bool> UpdateStudent(UpdateStudentView updateStudent)
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

                var response = await _putRepository.UpdateStudent(student, updateStudent.LoginForSearch);

                if (response != null)
                    return true;
            }

            return false;
        }

        //Обновление группы
        public async Task<GroupView> UpdateGroup(string oldName, string groupName)
        {
            if (groupName is not null && oldName is not null)
            {
                var response = await _putRepository.UpdateGroup(oldName, groupName);

                if(response is not null)
                    return ConvertToGroupView(response);
            }

            return null;
        }

        //Обновление посещений
        public async Task<bool> UpdateAttendance(List<UpdateAttendanceView> attendanceView)
        {
            if (attendanceView is not null)
            {
                List<Attendance> attendances = attendanceView.Select(attendance => new Attendance
                {
                    Id = attendance.AttendanceID,
                    IdStudent = attendance.StudentID,
                    IdPractice = attendance.PracticeID,
                    Date = DateOnly.Parse(attendance.Date),
                    Ispresent = attendance.IsPresence
                }).ToList();

                var response = _putRepository.UpdateAttendance(attendances);

                return response;
            }
            return false;
        }
    }
}
