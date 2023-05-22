using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksUpdate;

namespace PracticeControl.WebAPI.Converters
{
    public static class StudentConverter
    {
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

            };

            return studentView;
        }
        
    }
}
