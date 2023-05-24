using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;
using PracticeControl.WebAPI.Views.blanksUpdate;
using static PracticeControl.WebAPI.Converters.StudentConverter;

namespace PracticeControl.WebAPI.Converters
{
    public static class GroupConverter
    {
        private static byte[] PasswordSalt { get; set; }
        public static GroupView ConvertToGroupView(Group group)
        {
            return new GroupView
            {
                GroupID = Convert.ToInt32(group.Id),
                GroupName = group.Name,
                StudentsView = group.Students.Select(s => new StudentView
                {
                    StudentID = Convert.ToInt32(s.Id),
                    FirstName = s.Firstname,
                    LastName = s.Lastname,
                    MiddleName = s.Middlename,
                    Login = s.Login
                }).ToList()
            };
        }

        public static List<GroupView>? ConvertToListGroupView(List<Database.Group> groups)
        {
            return groups.Select(g => ConvertToGroupView(g)).ToList();
        }

        public static Group? ConvertToGroup(CreateGroupView group)
        {
            var newGroup = new Group
            {
                Name = group.GroupName,
            };

            var students = group.Students.Select(student => new Student
            {
                Lastname = student.LastName,
                Firstname = student.FirstName,
                Login = student.Login,
                Middlename = student.MiddleName,
                IdGroupNavigation = newGroup,
                Passwordsalt = GetSalt(),
                Passwordhash = PasswordHelper.GetHash(PasswordSalt, student.Password)
            }).ToList();

            newGroup.Students = students;

            return newGroup;
        }

        public static Group? ConvertToGroup(UpdateGroupView group)
        {
            var newGroup = new Group
            {
                Name = group.GroupName,
            };

            var students = group.StudentsView.Select(student => new Student
            {
                Firstname = student.FirstName,
                Lastname = student.LastName,
                Login = student.Login,
                Middlename = student.MiddleName,
                IdGroupNavigation = newGroup,
               
            })
            .ToList();



            newGroup.Students = students;

            return newGroup;
        }

        private static byte[] GetSalt()
        {
            PasswordSalt = PasswordHelper.GetSalt();
            return PasswordSalt;
        }
    }
}
