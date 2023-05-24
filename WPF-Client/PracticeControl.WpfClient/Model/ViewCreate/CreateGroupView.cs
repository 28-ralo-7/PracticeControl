
using System.Collections.Generic;

namespace PracticeControl.WpfClient.Model.ViewCreate
{
    public class CreateGroupView
    {
        public string GroupName { get; set; }
        public List<CreateStudentView> Students { get; set; }
    }
}
