namespace PracticeControl.WebAPI.Views.View
{
    public class AttendanceView
    {
        public int AttendanceID { get; set; }
        public string Date { get; set; }
        public bool IsPresent { get; set; }
        public byte[]? Photo { get; set; }
        public string Comment { get; set; }
        public StudentView StudentView { get; set; }
        public PracticeScheduleView PracticeScheduleView { get; set; }
    }
}


