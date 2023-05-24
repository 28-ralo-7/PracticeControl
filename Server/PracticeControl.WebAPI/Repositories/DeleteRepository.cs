using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;

namespace PracticeControl.WebAPI.Repositories
{
    public class DeleteRepository : IDeleteRepository
    {
        private readonly ProductionPracticeControlContext _context;
        public DeleteRepository(ProductionPracticeControlContext context)
        {
            _context = context;
        }

        //Сотрудники
        public async Task<Employee> DeleteEmployee(string login)
        {
            Employee? employee = await _context.Employees
                .Include(employee=> employee.Practiceschedules)
                .FirstOrDefaultAsync(employee => employee.Login == login);

            if (employee is null) 
            {
                return null;
            }

            employee.Isdeleted = true;
            employee.Practiceschedules.Select(practice => practice.Isdeleted = true);

            employee.Practiceschedules.ToList().ForEach(practice => practice.Isdeleted = true);

            List<Practiceschedule> practiceschedules = await _context.Practiceschedules.Where(schedule => schedule.IdEmployee == employee.Id).ToListAsync();

            practiceschedules.ForEach(schedule => schedule.Isdeleted = true);

            await _context.SaveChangesAsync();

            return employee;
        }

        //Студенты
        public async Task<Student> DeleteStudent(string login)
        {
            Student? student = await _context.Students
                .Include(student => student.IdGroupNavigation)
                .Include(student => student.Attendances)
                .FirstOrDefaultAsync(student => student.Login.ToLower() == login.ToLower());
                

            if (student is null)
                return null;

            student.Isdeleted = true;
            student.Attendances.Select(attendance => attendance.Isdeleted = true);

            await _context.SaveChangesAsync();

            return student;
        }

        //Группы
        public async Task<Group> DeleteGroup(string name)
        {
            Group? group = await _context.Groups
                .Include(group => group.Students)
                .Include(group => group.Practiceschedules)
                .FirstOrDefaultAsync(group => group.Name == name);

            if (group is null)
                return null;

            group.Isdeleted = true;

            group.Practiceschedules.Select(schedule => schedule.Isdeleted = true);

            var schedules = group.Practiceschedules.ToList();
            schedules.ForEach(schedules => schedules.Isdeleted = true);
            group.Practiceschedules = schedules;

            var students = group.Students.ToList();
            students.ForEach(student => student.Isdeleted = true);
            group.Students = students;

            await _context.SaveChangesAsync();
            
            return group;
        }
    }
}
