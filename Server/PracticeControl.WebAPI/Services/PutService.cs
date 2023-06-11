using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Repositories;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewUpdate;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;
using static PracticeControl.WebAPI.Converters.PracticeConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PracticeControl.WebAPI.Views.ViewMobile;

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

        //Сотрудник
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

        //Студент
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

        //Группа
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

        //Посещение
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

        //Практика
        public async Task<bool> UpdatePractice(PracticeView practiceView)
        {
            try
            {
                Practice practice = ConvertToPractice(practiceView);

                var isUpdate = _putRepository.UpdatePractice(practice);

                return isUpdate;
            }
            catch (Exception)
            {
                return false;
            }


        }

        //ФотоДобавить
        public async Task<bool> UpdateAttendance(StudentAttendanceView attendanceView)
        {
            var studentID = attendanceView.Student.StudentID;
            var date = DateOnly.Parse(attendanceView.DateNow.ToShortDateString());

            Attendance attendance = await _getRepository.GetAttendance(studentID, date);
            if (attendance is null)
                return false;

            attendance.Photo = attendanceView.Photo;

            var isUpdate = _putRepository.UpdateAttendance(attendance);

            return isUpdate.Result;
        }
    }
}
