using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Views.blanksUpdate
{
    public class UpdateGroupView
    {
        public string GroupName { get; set; }
        public List<UpdateStudentView> StudentsView { get; set; }
        public string NameForSearch{ get; set; }
    }
}
