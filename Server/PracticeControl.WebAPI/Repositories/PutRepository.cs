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

            if (employeeFromDb is null)
            {
                return null;
            }

            employeeFromDb.Lastname = employeeForUpdate.Lastname;
            employeeFromDb.Firstname = employeeForUpdate.Firstname;
            employeeFromDb.Middlename = employeeForUpdate.Middlename;
            employeeFromDb.Login = employeeForUpdate.Login;
            employeeFromDb.IsAdmin = employeeForUpdate.IsAdmin;

            if (!string.IsNullOrWhiteSpace(employeeForUpdate.Passwordhash))
            {
                employeeFromDb.Passwordhash = employeeForUpdate.Passwordhash;
                employeeFromDb.Passwordsalt = employeeForUpdate.Passwordsalt;
            }

            await _context.SaveChangesAsync();

            return employeeFromDb;
        }

        public async Task<Student> UpdateStudent(Student studentForUpdate, string loginSearch)
        {
            Student studentFromDb = await _context.Students.FirstOrDefaultAsync(student => student.Login == loginSearch);

            if (studentFromDb is null)
            {
                return null;
            }

            studentFromDb.Lastname = studentForUpdate.Lastname;
            studentFromDb.Firstname = studentForUpdate.Firstname;
            studentFromDb.Middlename = studentForUpdate.Middlename;
            studentFromDb.Login = studentForUpdate.Login;
            studentFromDb.IdGroup = studentForUpdate.IdGroup;

            if (!string.IsNullOrWhiteSpace(studentForUpdate.Passwordhash))
            {
                studentFromDb.Passwordhash = studentForUpdate.Passwordhash;
                studentFromDb.Passwordsalt = studentForUpdate.Passwordsalt;
            }

            await _context.SaveChangesAsync();

            return studentFromDb;
        }
    }
}
