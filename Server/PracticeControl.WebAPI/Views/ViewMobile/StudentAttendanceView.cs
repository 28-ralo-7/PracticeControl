namespace PracticeControl.WebAPI.Views.ViewMobile
{
    public class StudentAttendanceView//Mobile update Attendance for Student
    {
        public StudentViewMobile Student { get; set; }
        public byte[] Photo { get; set; }
        public DateTime DateNow { get; set; }
    }
}
