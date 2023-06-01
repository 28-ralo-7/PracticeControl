using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Helpers;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewCreate;
using PracticeControl.WebAPI.Views.ViewUpdate;
using static PracticeControl.WebAPI.Converters.StudentConverter;

namespace PracticeControl.WebAPI.Converters
{
    public static class GroupConverter
    {
        private static byte[] PasswordSalt { get; set; }

        //Из бд во View
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

        //Из View в бд
        public static Group ConvertToGroup(GroupView group)
        {
            return new Group
            {
                Id = group.GroupID,
                Name = group.GroupName,
                
            };
        }

        //Из лист бд в лист View
        public static List<GroupView>? ConvertToListGroupView(List<Database.Group> groups)
        {
            return groups.Select(g => ConvertToGroupView(g)).ToList();
        }

        //Из ViewCreate в бд 
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

        //Из ViewUpdate в бд
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

        //Генерация соли пароля
        private static byte[] GetSalt()
        {
            PasswordSalt = PasswordHelper.GetSalt();
            return PasswordSalt;
        }
    }
}
