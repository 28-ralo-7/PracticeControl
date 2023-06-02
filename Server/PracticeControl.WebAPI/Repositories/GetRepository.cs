using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using PracticeControl.WebAPI.Controllers;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Views.View;

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

        //Работние по ID для авторизации
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

        //Список расписаний
        public async Task<List<Practiceschedule>> GetPracticeScheduleList()
        {
            var practiceSchedule = await _context.Practiceschedules
                .Where(schedule => schedule.Isdeleted == false)

                .Include(schedule => schedule.IdEmployeeNavigation)

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

        //Практика по названию
        public async Task<Practice> GetPractice(string name)
        {
            string[] parts = name.Split(' ');

            string abbreviation = parts[0];
            string module = "";

            for (int i = 1; i < parts.Length - 1; i++)
            {
                if (i != 1)
                {
                    module += " ";
                }

                module += parts[i];
            }

            string specialty = parts[parts.Length - 1];
            specialty = specialty.Substring(1, specialty.Length - 2);

            var practice = await _context.Practices.FirstOrDefaultAsync(p => 
                                                p.Abbreviation == abbreviation && 
                                                p.Practicemodule == module &&
                                                p.Specialty == specialty
                                                );
            
            return practice;
        }

        //Сотрудник по имени
        public async Task<Employee> GetEmployee(string name)
        {
            string[] parts = name.Split(' ');

            string lastname = parts[0];
            string firstname = parts[1];
            string middlename = parts[2];

            var employee = await _context.Employees.FirstOrDefaultAsync(em =>
                                                em.Lastname == lastname &&
                                                em.Firstname == firstname &&
                                                em.Middlename == middlename
                                                );
            return employee;
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

        //Список всех студентов
        public async Task<List<Practice>> GetPracticeList()
        {
            var practice = await _context.Practices
                .Where(practice => practice.Isdeleted != true)
                .ToListAsync();

            return practice;
        }


    }
}
