using PracticeControl.WebAPI.Views.ViewUpdate;

namespace PracticeControl.WebAPI.Views.ViewUpdate
{
    public class UpdateGroupView
    {
        public string GroupName { get; set; }
        public List<UpdateStudentView> StudentsView { get; set; }
        public string NameForSearch{ get; set; }
    }
}
