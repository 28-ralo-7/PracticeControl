using PracticeControl.WebAPI.Views.ViewCreate;

namespace PracticeControl.WebAPI.Views.ViewCreate
{
    public class CreateGroupView
    {
        public string GroupName { get; set; }
        public List<CreateStudentView> Students { get; set; }
    }
}
