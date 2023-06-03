namespace PracticeControl.WpfClient.Model.View
{
    public class AttendanceView
    {
        public int AttendanceID { get; set; }
        public string Date { get; set; }
        public bool IsPresent { get; set; }
        public byte[] Photo { get; set; }

        public StudentView StudentView { get; set; }

        public PracticeScheduleView PracticeSchedule { get; set; }
    }
}


