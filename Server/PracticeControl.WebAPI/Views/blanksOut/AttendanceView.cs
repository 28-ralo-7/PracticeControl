namespace PracticeControl.WebAPI.Views.blanks
{
    public class AttendanceView
    {
        public int AttendanceID { get; set; }
        public string Date { get; set; }
        public bool IsPresent { get; set; }
        public string Photo { get; set; }
        public StudentView StudentView { get; set; }
        public PracticeScheduleView PracticeScheduleView { get; set; }
    }
}


