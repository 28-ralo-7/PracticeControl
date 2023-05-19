using Microsoft.EntityFrameworkCore;
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

        public async Task<Employee> DeleteEmployee(string login)
        {

            Employee? employee = await _context.Employees
                .FirstOrDefaultAsync(employee => employee.Login == login);

            if (employee is null) 
            {
                return null;
            }

            employee.Isdeleted = true;
            employee.Practiceschedules
                .Where(schedule => schedule.IdEmployee == employee.Id)
                .Select(schedule => schedule.Isdeleted = true);

            employee.Practiceschedules
                .Where(schedule => schedule.IdEmployee == employee.Id)
                .Select(schedule => schedule.Attendances
                .Select(attendance=> attendance.Isdeleted == true));

            await _context.SaveChangesAsync();

            return employee;
        }
    }
}
