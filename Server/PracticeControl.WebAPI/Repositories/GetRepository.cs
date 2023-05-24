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
        //Список групп
        public async Task<List<Group>> GetGroups()
        {
            var groups = await _context.Groups
                .Where(group => group.Isdeleted == false)

                .Include(g => g.Students
                .Where(student => student.Isdeleted == false))

                .ToListAsync();

            return groups;
           
        }
        //Работние по ID лоя авторизации
        public async Task<Employee> GetEmployee(int id)
        {
            var practicesEmployee = await _context.Employees
                .Where(employee => employee.Isdeleted == false)

                .Include(b=>b.Practiceschedules)
                .ThenInclude(b=>b.IdGroupNavigation)
                .ThenInclude (b=>b.Students)

                .Include(b=>b.Practiceschedules)
                .ThenInclude(b=>b.IdPracticeNavigation)

                .Include(b => b.Practiceschedules)
                .ThenInclude(b => b.Attendances)
                .ThenInclude(b=>b.IdStudentNavigation)

                .FirstOrDefaultAsync(b=>b.Id == id);

            return practicesEmployee;

        }
        //Список сотрудников для админа
        public async Task<List<Employee>> GetEmployeeList()
        {
            var employees = await _context.Employees
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

                .ToListAsync();

            return employees;
        }
        //Список практик
        public async Task<List<Practiceschedule>> GetPracticeScheduleList()
        {
            var practiceSchedule = await _context.Practiceschedules
                .Where(schedule => schedule.Isdeleted == false)

                .Include(ps => ps.IdPracticeNavigation)

                .Include(ps => ps.IdGroupNavigation.Students)
                .Include(ps => ps.Attendances)
                .ThenInclude(a => a.IdStudentNavigation)

                .ToListAsync();

            return practiceSchedule;
        }
        //Группа по названию
        public async Task<Group> GetGroup(string name)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(group => group.Name == name);

            return group;
        }
        //Список студентов группы
        public async Task<List<Student>> GetStudentsGroup(string groupName)
        {
            var studentsGroup = await _context.Students
                .Include(student => student.IdGroupNavigation)
                .Where(student => student.IdGroupNavigation.Name == groupName && student.Isdeleted == false)
                .ToListAsync();

            return studentsGroup;
        }
        //Список всех студентов
        public async Task<List<Student>> GetStudents()
        {
            var students = await _context.Students
                .Where(student => student.Isdeleted != true)
                .Include(student => student.IdGroupNavigation)
                .ToListAsync();

            return students;
        }
    }
}
