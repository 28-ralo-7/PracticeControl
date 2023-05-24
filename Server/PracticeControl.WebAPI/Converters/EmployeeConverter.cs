using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;
using PracticeControl.WebAPI.Views.blanksUpdate;
using static PracticeControl.WebAPI.Converters.PracticeScheduleConverter;

namespace PracticeControl.WebAPI.Converters
{
    public static class EmployeeConverter
    {
        //Из бд модели в представление для создания
        public static CreateEmployeeView ConvertToCreateEmployeeView(Employee employee)
        {
            var employeeView = new CreateEmployeeView
            {
                LastName = employee.Lastname,
                FirstName = employee.Firstname,
                MiddleName = employee.Middlename,
                Login = employee.Login,
                IsAdmin = employee.IsAdmin
            };

            return employeeView;
        }
        //Из представление для создания в бд модели
        public static Employee ConvertToEmployee(CreateEmployeeView employeeView)
        {
            var employee = new Employee
            {
                Lastname = employeeView.LastName,
                Firstname = employeeView.FirstName,
                Middlename = employeeView.MiddleName,
                Login = employeeView.Login,
                IsAdmin = employeeView.IsAdmin
            };

            return employee;
        }
        //Из бд модели в представление для вывода
        public static EmployeeView ConvertToEmployeeView(Employee employee)
        {
            var employeeView = new EmployeeView();

            employeeView.IsAdmin = employee.IsAdmin;
            employeeView.MiddleName = employee.Middlename;
            employeeView.FirstName = employee.Firstname;
            employeeView.LastName = employee.Lastname;
            employeeView.EmployeeID = Convert.ToInt32(employee.Id);
            employeeView.Login = employee.Login;

            /*var practiceScheduleView = new List<PracticeScheduleView>();

            foreach (var schedule in employee.Practiceschedules)
            {
                practiceScheduleView.Add(new PracticeScheduleView
                {
                    Abbreviation = schedule.IdPracticeNavigation.Abbreviation,
                    PracticeModule = schedule.IdPracticeNavigation.Practicemodule,
                    Specialty = schedule.IdPracticeNavigation.Specialty,
                    StartDate = schedule.Startdate.ToString(),
                    EndDate = schedule.Enddate.ToString(),
                    Attendances = schedule.Attendances.Select(attendance => new AttendanceView
                    {
                        AttendanceID = Convert.ToInt32(attendance.Id),
                        Date = attendance.Date.ToString(),
                        IsPresent = attendance.Ispresent,
                        Photo = attendance.Photo,
                        StudentView = new StudentView
                        {
                            StudentID = Convert.ToInt32(attendance.IdStudentNavigation.Id),
                            LastName = attendance.IdStudentNavigation.Lastname,
                            FirstName = attendance.IdStudentNavigation.Firstname,
                            MiddleName = attendance.IdStudentNavigation.Middlename,
                            Login = attendance.IdStudentNavigation.Login,
                            Group = new GroupView
                            {
                                GroupID = Convert.ToInt32(attendance.IdStudentNavigation.IdGroup),
                                GroupName = attendance.IdStudentNavigation.IdGroupNavigation.Name,

                            }
                        }
                    }).ToList()

                });
            }*/

            var practiceSchedulesView = new List<PracticeScheduleView>();

            if (employee.Practiceschedules is not null)

            foreach (var item in employee.Practiceschedules)
            {
                var studentsView = new List<StudentView>();

                foreach (var st in item.IdGroupNavigation.Students)
                {
                    var student = new StudentView
                    {

                        FirstName = st.Firstname,
                        StudentID = Convert.ToInt32(st.Id),
                        LastName = st.Lastname,
                        Login = st.Login,
                        MiddleName = st.Middlename,

                    };

                    studentsView.Add(student);
                }


                var groupsView = new GroupView
                {
                    GroupID = Convert.ToInt32(item.IdGroupNavigation.Id),
                    GroupName = item.IdGroupNavigation.Name,
                    StudentsView = studentsView,
                };


                var attendancesView = new List<AttendanceView>();

                foreach (var att in item.Attendances)
                {
                    var attendance = new AttendanceView
                    {
                        AttendanceID = Convert.ToInt32(att.Id),
                        Date = att.Date.ToShortDateString(),
                        IsPresent = att.Ispresent,
                        Photo = att.Photo,
                        StudentView = groupsView.StudentsView.FirstOrDefault(b => b.StudentID == Convert.ToInt32(att.IdStudentNavigation.Id)),

                    };

                    attendancesView.Add(attendance);
                }




                practiceSchedulesView.Add(
                    new PracticeScheduleView
                    {
                        EndDate = item.Enddate.ToShortDateString(),
                        StartDate = item.Startdate.ToShortDateString(),
                        Abbreviation = item.IdPracticeNavigation.Abbreviation,
                        PracticeModule = item.IdPracticeNavigation.Practicemodule,
                        PracticeScheduleID = Convert.ToInt32(item.Id),
                        Attendances = attendancesView,
                        Group = groupsView,
                        Specialty = item.IdPracticeNavigation.Specialty,
                        Employee = employeeView
                    });
            }

            employeeView.PracticeSchedules = practiceSchedulesView;

            return employeeView;
        }
        //Из списка представлений для вывода в список бд моделей
        public static List<EmployeeView> ConvertToListEmployeeView(List<Employee> employees)
        {
            List<EmployeeView> employeeViews = new List<EmployeeView>();

            foreach (var employee in employees)
            {
                EmployeeView employeeView = new EmployeeView();
                employeeView.IsAdmin = employee.IsAdmin;
                employeeView.MiddleName = employee.Middlename;
                employeeView.FirstName = employee.Firstname;
                employeeView.LastName = employee.Lastname;
                employeeView.EmployeeID = Convert.ToInt32(employee.Id);
                employeeView.Login = employee.Login;



                var practiceSchedulesView = new List<PracticeScheduleView>();


                foreach (var item in employee.Practiceschedules)
                {
                    var studentsView = new List<StudentView>();

                    foreach (var st in item.IdGroupNavigation.Students)
                    {
                        var student = new StudentView
                        {

                            FirstName = st.Firstname,
                            StudentID = Convert.ToInt32(st.Id),
                            LastName = st.Lastname,
                            Login = st.Login,
                            MiddleName = st.Middlename,

                        };

                        studentsView.Add(student);
                    }

                    var groupsView = new GroupView
                    {
                        GroupID = Convert.ToInt32(item.IdGroupNavigation.Id),
                        GroupName = item.IdGroupNavigation.Name,
                        StudentsView = studentsView,
                    };

                    var attendancesView = new List<AttendanceView>();

                    foreach (var att in item.Attendances)
                    {
                        var attendance = new AttendanceView
                        {
                            AttendanceID = Convert.ToInt32(att.Id),
                            Date = att.Date.ToShortDateString(),
                            IsPresent = att.Ispresent,
                            Photo = att.Photo,
                            StudentView = groupsView.StudentsView.FirstOrDefault(b => b.StudentID == Convert.ToInt32(att.IdStudentNavigation.Id)),

                        };

                        attendancesView.Add(attendance);
                    }

                    practiceSchedulesView.Add(
                        new PracticeScheduleView
                        {
                            EndDate = item.Enddate.ToShortDateString(),
                            StartDate = item.Startdate.ToShortDateString(),
                            Abbreviation = item.IdPracticeNavigation.Abbreviation,
                            PracticeModule = item.IdPracticeNavigation.Practicemodule,
                            PracticeScheduleID = Convert.ToInt32(item.Id),
                            Attendances = attendancesView,
                            Group = groupsView,
                            Specialty = item.IdPracticeNavigation.Specialty,
                            Employee = employeeView
                        });
                }

                employeeView.PracticeSchedules = practiceSchedulesView;
                employeeViews.Add(employeeView);
            }

            return employeeViews;
        }
        //Из представления для обновления в бд модель
        public static Employee ConvertToEmployee(UpdateEmployeeView updateEmployee)
        {
            var employee = new Employee
            {
                Lastname = updateEmployee.LastName,
                Firstname = updateEmployee.FirstName,
                Middlename = updateEmployee.MiddleName,
                Login = updateEmployee.Login,
                IsAdmin = updateEmployee.IsAdmin
            };

            return employee;
        }
    }
}
