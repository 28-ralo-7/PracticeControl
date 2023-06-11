using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Model.ViewUpdate
{
    public class UpdateAttendanceView
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int PracticeID { get; set; }
        public string Date { get; set; }
        public bool IsPresence { get; set; }
    }
}
