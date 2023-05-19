namespace PracticeControl.WebAPI.Views.blanks
{
    public class PracticeScheduleView
    {
        public int PracticeScheduleID { get; set; }
        public string Abbreviation { get; set; }
        public string PracticeModule { get; set; }
        public string Specialty { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public EmployeeView Employee { get; set; }
        public GroupView Group { get; set; }

        public List<AttendanceView> Attendances { get; set; }

    }
}
