using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeControl.XamarinClient.Models
{
    public class StudentAttendanceView
    {
        public StudentViewMobile Student { get; set; }
        public byte[] Photo { get; set; }
        public DateTime DateNow { get; set; }
    }
}
