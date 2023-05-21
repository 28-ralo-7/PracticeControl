using Microsoft.EntityFrameworkCore;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using System.Security.Cryptography.X509Certificates;

namespace PracticeControl.WebAPI.Repositories
{
    public class PutRepository : IPutRepository
    {
        private readonly ProductionPracticeControlContext _context;
        public PutRepository(ProductionPracticeControlContext context)
        {
            _context= context;
        }
        public async Task<Employee> UpdateEmployee(Employee employeeForUpdate, string loginSearch)
        {
            Employee? employeeFromDb = await _context.Employees.FirstOrDefaultAsync(employee => employee.Login == loginSearch);

            if (employeeFromDb == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(employeeForUpdate.Passwordhash))
            {
                employeeFromDb.Lastname = employeeForUpdate.Lastname;
                employeeFromDb.Firstname = employeeForUpdate.Firstname;
                employeeFromDb.Middlename = employeeForUpdate.Middlename;
                employeeFromDb.Login = employeeForUpdate.Login;
                employeeFromDb.IsAdmin = employeeForUpdate.IsAdmin;

                employeeFromDb.Passwordhash = employeeForUpdate.Passwordhash;
                employeeFromDb.Passwordsalt = employeeForUpdate.Passwordsalt;

                await _context.SaveChangesAsync();

                return employeeFromDb;
            }

            employeeFromDb.Lastname = employeeForUpdate.Lastname;
            employeeFromDb.Firstname = employeeForUpdate.Firstname;
            employeeFromDb.Middlename = employeeForUpdate.Middlename;
            employeeFromDb.Login = employeeForUpdate.Login;
            employeeFromDb.IsAdmin = employeeForUpdate.IsAdmin;

            await _context.SaveChangesAsync();

            return employeeFromDb;

        }
    }
}
