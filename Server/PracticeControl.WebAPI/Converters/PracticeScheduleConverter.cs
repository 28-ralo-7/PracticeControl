using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Converters
{
    public static class PracticeScheduleConverter
    {

        public static List<PracticeScheduleView> ConvertToPracticeScheduleView(List<Practiceschedule> practiceSchedules, IGetRepository getRepository)
        {

            List<PracticeScheduleView> practiceScheduleViews = new List<PracticeScheduleView>();

            foreach (var practiceSchedule in practiceSchedules)
            {
                PracticeScheduleView practiceScheduleView = new PracticeScheduleView();

                practiceScheduleView.PracticeScheduleID = Convert.ToInt32(practiceSchedule.Id);
                practiceScheduleView.Abbreviation = practiceSchedule.IdPracticeNavigation.Abbreviation;
                practiceScheduleView.PracticeModule = practiceSchedule.IdPracticeNavigation.Practicemodule;
                practiceScheduleView.Specialty = practiceSchedule.IdPracticeNavigation.Specialty;
                practiceScheduleView.StartDate = practiceSchedule.Startdate.ToString();
                practiceScheduleView.EndDate = practiceSchedule.Enddate.ToString();

                Employee? employee = getRepository.GetEmployee((int)practiceSchedule.IdEmployee);

                if (employee is not null)
                {
                    practiceScheduleView.Employee = new EmployeeView
                    {
                        EmployeeID = Convert.ToInt32(employee.Id),
                        LastName = employee.Lastname,
                        FirstName = employee.Firstname,
                        MiddleName = employee.Middlename,
                        Login = employee.Login,
                        IsAdmin = employee.IsAdmin
                    };
                }


                Database.Group group = practiceSchedule.IdGroupNavigation;

                practiceScheduleView.Group = new GroupView
                {
                    GroupID = (int)group.Id,
                    GroupName = group.Name,
                    StudentsView = group.Students.Select(student => new StudentView
                    {
                        StudentID = (int)student.Id,
                        LastName = student.Lastname,
                        FirstName = student.Firstname,
                        MiddleName = student.Middlename,
                        Login = student.Login
                    }).ToList()
                };

                practiceScheduleView.Attendances = practiceSchedule.Attendances.Select(attendance => new AttendanceView
                {
                    AttendanceID = Convert.ToInt32(attendance.Id),
                    Date = attendance.Date.ToString(),
                    Photo = attendance.Photo,
                    IsPresent = attendance.Ispresent,
                    PracticeScheduleView = practiceScheduleView,
                    StudentView = new StudentView
                    {
                        StudentID = (int)attendance.IdStudentNavigation.Id,
                        LastName = attendance.IdStudentNavigation.Lastname,
                        FirstName = attendance.IdStudentNavigation.Firstname,
                        MiddleName = attendance.IdStudentNavigation.Middlename,
                        Login = attendance.IdStudentNavigation.Login
                    }


                }).ToList();

                practiceScheduleViews.Add(practiceScheduleView);
            }
            return practiceScheduleViews;
        }
    }
}
