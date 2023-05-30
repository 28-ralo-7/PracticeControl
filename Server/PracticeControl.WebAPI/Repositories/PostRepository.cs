using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PracticeControl.WebAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ProductionPracticeControlContext _context;
        public PostRepository(ProductionPracticeControlContext context)
        {
            _context = context;
        }

        public Employee CreateEmployee(Employee employee)
        {
            if (employee is not null)
            {
                _context.Add(employee);
                _context.SaveChanges();
                return employee;
            }
            return null;
        }

        public Group CreateGroup(Group group)
        {
            if (group is not null)
            {
                _context.Add(group);
                _context.SaveChanges();
                return group;
            }
            return null;
        }

        public Student CreateStudent(Student student)
        {
            if (student is not null)
            {      
                _context.Add(student);
                _context.SaveChanges();
                return student;
            }
            return null;
        }

        public bool CreatePracticeSchedule(Practiceschedule schedule)
        {
            if (schedule is not null)
            {
                _context.Add(schedule);

                var students = _context.Groups.
                    Include(group => group.Students)
                    .FirstOrDefault(group => group.Id == schedule.IdGroup)
                    .Students.ToList();

                List<Attendance> attendances = new List<Attendance>();

                foreach (var student in students)
                {
                    for (DateTime date = Convert.ToDateTime(schedule.Startdate.ToShortDateString()); date <= Convert.ToDateTime(schedule.Enddate.ToShortDateString()); date = date.AddDays(1))
                    {

                        if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                        {
                            continue;
                        }

                        attendances.Add(new Attendance
                        {
                            IdPracticeNavigation = schedule,
                            IdStudent = student.Id,
                            Date = DateOnly.Parse(date.ToShortDateString()),
                            Ispresent = false,
                            Photo = null,
                            IdPractice = schedule.IdPractice,
                            CompanyName = string.Empty,
                        });
                    }
                }

                

                _context.AddRange(attendances);

                _context.SaveChanges();
                
                return true;
            }

            return false;
        }
    }
}
