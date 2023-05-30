using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Windows.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model.ViewOut
{
    public class StudentOut
    {
        public GroupView Group { get; set; }
        public string StudentName { get; set; }
        public string Login { get; set; }
    }
}
