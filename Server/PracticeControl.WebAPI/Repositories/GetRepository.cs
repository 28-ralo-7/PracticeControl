using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using PracticeControl.WebAPI.Controllers;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Repositories
{


    public class GetRepository : IGetRepository
    {
        private readonly ProductionPracticeControlContext _context;
        public GetRepository(ProductionPracticeControlContext context)
        {
            _context = context;
        }

        public List<Group>? GetGroups()
        {
            var groups = _context.Groups
                .Where(group => group.Isdeleted == false)

                .Include(g => g.Students
                .Where(student => student.Isdeleted == false))

                .ToList();

            return groups;
           
        }

        public Employee? GetEmployee(int id)
        {
            var practicesEmployee = _context.Employees
                .Where(employee => employee.Isdeleted == false)

                .Include(b=>b.Practiceschedules)
                .ThenInclude(b=>b.IdGroupNavigation)
                .ThenInclude (b=>b.Students)

                .Include(b=>b.Practiceschedules)
                .ThenInclude(b=>b.IdPracticeNavigation)

                .Include(b => b.Practiceschedules)
                .ThenInclude(b => b.Attendances)
                .ThenInclude(b=>b.IdStudentNavigation)

                .FirstOrDefault(b=>b.Id == id);

            return practicesEmployee;

        }

        public List<Employee> GetEmployeeList()
        {
            var employees = _context.Employees
                .Where(employee => employee.Isdeleted == false)

                .Include(b => b.Practiceschedules
                .Where(schedule => schedule.Isdeleted == false))

                .ThenInclude(b => b.IdGroupNavigation)
                .ThenInclude(b => b.Students
                .Where(student => student.Isdeleted == false))

                .Include(b => b.Practiceschedules
                .Where(schedule => schedule.Isdeleted == false))
                .ThenInclude(b => b.IdPracticeNavigation)

                .Include(b => b.Practiceschedules
                .Where(schedule => schedule.Isdeleted == false))

                .ThenInclude(b => b.Attendances
                .Where(attendance => attendance.Isdeleted == false))
                .ThenInclude(b => b.IdStudentNavigation)

                .ToList();

            return employees;
        }

        public List<Practiceschedule> GetPracticeScheduleList()
        {
            var practiceSchedule = _context.Practiceschedules
                .Where(schedule => schedule.Isdeleted == false)

                .Include(ps => ps.IdPracticeNavigation)

                .Include(ps => ps.IdGroupNavigation.Students)
                .Include(ps => ps.Attendances)
                .ThenInclude(a => a.IdStudentNavigation)

                .ToList();

            return practiceSchedule;
        }
    }
}
