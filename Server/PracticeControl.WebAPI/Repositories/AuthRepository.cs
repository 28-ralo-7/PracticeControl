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
            var employee = _context.Employees.FirstOrDefault(x => x.Login == login);
            return employee;
        }
    }
}
