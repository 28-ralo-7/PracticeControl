using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Converters
{
    public static class GroupConverter
    {
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
    }
}
