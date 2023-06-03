namespace PracticeControl.WebAPI.Views.ViewUpdate
{
    public class UpdateAttendanceView//WPF update AttendancesPage
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int PracticeID { get; set; }
        public string Date { get; set; }
        public bool IsPresence { get; set; }
    }
}
