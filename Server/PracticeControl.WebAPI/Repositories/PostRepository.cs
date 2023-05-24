using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;

namespace PracticeControl.WebAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ProductionPracticeControlContext _context;
        public PostRepository(ProductionPracticeControlContext context)
        {
            _context = context;
        }

        public Employee CreateEmployee(Employee employee)
        {
            if (employee is not null)
            {
                _context.Add(employee);
                _context.SaveChanges();
                return employee;
            }
            return null;
        }

        public Group CreateGroup(Group group)
        {
            if (group is not null)
            {
                _context.Add(group);
                _context.SaveChanges();
                return group;
            }
            return null;
        }

        public Student CreateStudent(Student student)
        {
            if (student is not null)
            {      
                _context.Add(student);
                _context.SaveChanges();
                return student;
            }
            return null;
        }
    }
}
