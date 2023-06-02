using Microsoft.EntityFrameworkCore;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;

namespace PracticeControl.WebAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ProductionPracticeControlContext _context;
        public AuthRepository(ProductionPracticeControlContext context)
        {
            _context = context;
        }

        public Employee? GetEmployee(string login)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Login == login && x.Isdeleted == false);
            return employee;
        }

        public Student? GetStudent(string login)
        {
            var student = _context.Students
                .Include(student => student.IdGroupNavigation)
                .FirstOrDefault(s => s.Login == login && s.Isdeleted == false);
            return student;
        }
    }
}
