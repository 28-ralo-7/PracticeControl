using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging.Abstractions;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using System.Reflection.Metadata.Ecma335;

namespace PracticeControl.WebAPI.Repositories
{
    public class DeleteRepository : IDeleteRepository
    {
        private readonly ProductionPracticeControlContext _context;
        private readonly IGetRepository _getRepository;

        public DeleteRepository(ProductionPracticeControlContext context, IGetRepository getRepository)
        {
            _context = context;
            _getRepository = getRepository;
        }

        //Сотрудники
        public async Task<Employee> DeleteEmployee(string login)
        {
            Employee? employee = await _context.Employees
                .Include(employee=> employee.Practiceschedules)
                .ThenInclude(schedule => schedule.Attendances)
                .FirstOrDefaultAsync(employee => employee.Login == login);

            if (employee is null) 
            {
                return null;
            }

            List<Practiceschedule> practiceschedules = await _context.Practiceschedules.Where(schedule => schedule.IdEmployee == employee.Id).ToListAsync();
            List<Attendance> attendances = new List<Attendance>();

            foreach (var schedule in practiceschedules)
            {
                attendances.AddRange(schedule.Attendances);
            }

            _context.RemoveRange(attendances);

            _context.RemoveRange(practiceschedules);

            _context.Remove(employee);

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


            List<Attendance> attendances = await _context.Attendances.Where(att => att.IdStudent == student.Id).ToListAsync();

            _context.RemoveRange(attendances);
            _context.Remove(student);

            await _context.SaveChangesAsync();

            return student;
        }

        //Группы
        public async Task<Group> DeleteGroup(string name)
        {
            Group? group = await _context.Groups
                .Include(group => group.Students)
                .ThenInclude(student=> student.Attendances)
                .Include(group => group.Practiceschedules)
                .FirstOrDefaultAsync(group => group.Name == name);

            if (group is null)
                return null;


            List<Attendance> attendances = new List<Attendance>();

            foreach(var student in group.Students)
            {
                attendances.AddRange(student.Attendances);
            }

            _context.RemoveRange(attendances);

            _context.RemoveRange(group.Students);

            _context.RemoveRange(group.Practiceschedules);

            _context.Remove(group);

            await _context.SaveChangesAsync();
            
            return group;
        }

        //Практики
        public async Task<bool> DeletePracice(int id)
        {
            Practice? practice = await _context.Practices.FirstOrDefaultAsync(p => p.Id == id);

            if (practice is null)
                return false;
            try
            {
                List<Practiceschedule> schedules = await _context.Practiceschedules
                    .Where(ps => ps.IdPractice == practice.Id)
                    .Include(ps=>ps.Attendances)
                    .ToListAsync();
                List<Attendance> attendances = new List<Attendance>();

                foreach (var schedule in schedules)
                {
                    attendances.AddRange(schedule.Attendances);
                }

                _context.RemoveRange(attendances);
                _context.RemoveRange(schedules);
                _context.Remove(practice);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        //Расписания
        public async Task<bool> DeletePracticeSchedule(int id)
        {
            Practiceschedule? practicescheduleFromDb = await _context.Practiceschedules
                .Include(schedule => schedule.Attendances)
                .FirstOrDefaultAsync(ps=> ps.Id == id);

            try
            {
                List<Attendance> attendances = practicescheduleFromDb.Attendances.ToList();

                _context.RemoveRange(attendances);
                _context.Remove(practicescheduleFromDb);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
