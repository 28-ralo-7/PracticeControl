namespace PracticeControl.WebAPI.Views.View
{
    public class GroupView
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public List<StudentView> StudentsView { get; set; }

    }
}
