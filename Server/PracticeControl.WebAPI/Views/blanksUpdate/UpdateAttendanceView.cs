namespace PracticeControl.WebAPI.Views.blanksUpdate
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
