using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;

namespace PracticeControl.WebAPI.Converters
{
    public static class GroupConverter
    {
        private static byte[] PasswordSalt { get; set; }
        public static GroupView ConvertToGroupView(Group group, List<StudentView> studentView)
        {
            return null;
        }

        public static List<GroupView>? ConvertToListGroupView(List<Database.Group> groups)
        {

            return groups.Select(g => new GroupView
            {
                GroupID = Convert.ToInt32(g.Id),
                GroupName = g.Name,
                StudentsView = g.Students.Select(s => new StudentView
                {
                    StudentID = Convert.ToInt32(s.Id),
                    FirstName = s.Firstname,
                    LastName = s.Lastname,
                    MiddleName = s.Middlename,
                    Login = s.Login
                }).ToList()
            }).ToList();
        }

        public static Group? ConvertToGroup(CreateGroupView group)
        {
            var newGroup = new Group
            {
                Name = group.GroupName,
            };

            var students = group.Students.Select(x => new Student
            {
                Firstname = x.FirstName,
                Lastname = x.LastName,
                Login = x.Login,
                Middlename = x.MiddleName,
                IdGroupNavigation = newGroup,
                Passwordsalt = GetSalt(),
                Passwordhash = PasswordHelper.GetHash(PasswordSalt, x.Password),
            }).ToList();

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
