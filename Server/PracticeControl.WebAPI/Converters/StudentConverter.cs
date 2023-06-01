using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewCreate;
using PracticeControl.WebAPI.Views.ViewUpdate;

namespace PracticeControl.WebAPI.Converters
{
    public static class StudentConverter
    {
        private static byte[] PasswordSalt { get; set; }

        //Из представления для обновления в бд модель
        public static Student ConvertToStudent(UpdateStudentView updateStudent)
        {
            var student = new Student
            {
                Lastname = updateStudent.LastName,
                Firstname = updateStudent.FirstName,
                Middlename = updateStudent.MiddleName,
                Login = updateStudent.Login
            };

            return student;
        }

        //Из бд модели в представление для обновления
        public static StudentView ConvertToView(Student student)
        {
            var studentView = new StudentView
            {
                LastName = student.Lastname,
                FirstName = student.Firstname,
                MiddleName = student.Middlename, 
                Login = student.Login,
                Group = new GroupView
                {
                    GroupID = Convert.ToInt32(student.IdGroup),
                    GroupName = student.IdGroupNavigation.Name,
                    
                }

            };

            return studentView;
        }

        //Из create модели в бд модель
        public static Student ConvertToStudent(CreateStudentView student)
        {
            var studentView = new Student
            {
                Lastname = student.LastName,
                Firstname = student.FirstName,
                Middlename = student.MiddleName,
                Login = student.Login,
                Passwordsalt = GetSalt(),
                Passwordhash = PasswordHelper.GetHash(PasswordSalt, student.Password)
                
            };
            
            return studentView;
        }

        //Из бд в ViewCreate
        public static CreateStudentView ConvertToCreateView(Student student)
        {
            var studentCreated = new CreateStudentView
            {
                LastName = student.Lastname,
                FirstName = student.Firstname,
                MiddleName = student.Middlename,
                Login = student.Login,
            };

            return studentCreated;
            
        }

        //Из View модели в бд
        public static Student ConvertToStudent(StudentView studentView)
        {
            return new Student
            {
                Id = studentView.StudentID,
                Lastname = studentView.LastName,
                Firstname = studentView.FirstName,
                Middlename = studentView.MiddleName,
                Login = studentView.Login,
                IdGroup = studentView.Group.GroupID
            };
        }

        //Соль для пароля
       private static byte[] GetSalt()
       {
            PasswordSalt = PasswordHelper.GetSalt();
            return PasswordSalt;
       }

       
    }
}
