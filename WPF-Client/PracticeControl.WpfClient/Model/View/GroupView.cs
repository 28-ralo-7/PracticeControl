using System.Collections.Generic;

namespace PracticeControl.WpfClient.Model.View
{
    public class GroupView
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public List<StudentView> StudentsView { get; set; }

    }
}
