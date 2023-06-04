using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using System.Security.Cryptography.X509Certificates;

namespace PracticeControl.WebAPI.Repositories
{
    public class PutRepository : IPutRepository
    {
        private readonly ProductionPracticeControlContext _context;
        private readonly IPostRepository _postRepository;
        public PutRepository(ProductionPracticeControlContext context, IPostRepository postRepository)
        {
            _context= context;
            _postRepository= postRepository;
        }
        //Сотрудники
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
        
        //Студенты
        public async Task<Student> UpdateStudent(Student studentForUpdate, string loginSearch)
        {
            Student? studentFromDb = await _context.Students
                .Include(student=> student.Attendances)
                .FirstOrDefaultAsync(student => student.Login == loginSearch);

            if (studentFromDb is null)
            {
                return null;
            }

            studentFromDb.Lastname = studentForUpdate.Lastname;
            studentFromDb.Firstname = studentForUpdate.Firstname;
            studentFromDb.Middlename = studentForUpdate.Middlename;
            studentFromDb.Login = studentForUpdate.Login;

            if (studentFromDb.IdGroup != studentForUpdate.IdGroup)
            {
                List<Attendance> att = studentFromDb.Attendances.ToList();
                _context.Attendances.RemoveRange(att);
                
                Practiceschedule? schedule = await _context.Practiceschedules
                    .FirstOrDefaultAsync(ps=>ps.IdGroup == studentForUpdate.IdGroup);

                _postRepository.CreateAttendance(schedule, studentFromDb.Id);
            }

            studentFromDb.IdGroup = studentForUpdate.IdGroup;

            if (!string.IsNullOrWhiteSpace(studentForUpdate.Passwordhash))
            {
                studentFromDb.Passwordhash = studentForUpdate.Passwordhash;
                studentFromDb.Passwordsalt = studentForUpdate.Passwordsalt;
            }

            await _context.SaveChangesAsync();

            return studentFromDb;
        }

        //Группы
        public async Task<Group> UpdateGroup(string oldName, string newName)
        {
            Group group = await _context.Groups.FirstOrDefaultAsync(group => group.Name == oldName);

            if (group is not null) 
            { 
                group.Name = newName;
                await _context.SaveChangesAsync();
                return group;
            }
            return null;
        }

        //Посещения отметка
        public bool UpdateAttendance(List<Attendance> attendances)
        {
            try
            {
                foreach (var attendance in attendances)
                {
                    Attendance? attendanceFromDb = _context.Attendances
                            .FirstOrDefault(a => a.Id ==attendance.Id);

                    attendanceFromDb.Ispresent = attendance.Ispresent;
                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Посещения фото
        public bool UpdateAttendance(Attendance attendanceForUpdate)
        {
            try
            {
                Attendance? attendance =  _context.Attendances
                    .FirstOrDefault(att => 
                            att.IdStudent == attendanceForUpdate.IdStudent &&
                            att.Date == attendanceForUpdate.Date
                            );

                attendance.Photo = attendanceForUpdate.Photo;

                _context.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Практика
        public bool UpdatePractice(Practice practice)
        {
            try
            {

                Practice? practiceFromDb = _context.Practices
                        .FirstOrDefault(a => a.Id == practice.Id);

                practiceFromDb.Abbreviation = practice.Abbreviation;
                practiceFromDb.Practicemodule = practice.Practicemodule;
                practiceFromDb.Specialty = practice.Specialty;

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
