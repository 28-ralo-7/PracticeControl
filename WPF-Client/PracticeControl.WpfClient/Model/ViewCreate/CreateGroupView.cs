using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model.ViewCreate
{
    public class CreateGroupView
    {
        public string GroupName { get; set; }
        public List<CreateStudentView> Students { get; set; }
    }
}
