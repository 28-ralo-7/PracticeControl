using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model.ViewCreate
{
    public class CreatePracticeSchedule
    {
        public string Practice { get; set; }
        public string Group { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
