using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Views.blanksCreate
{
    public class CreateGroupView
    {
        public string GroupName { get; set; }
        public List<CreateStudentView> Students { get; set; }
    }
}
