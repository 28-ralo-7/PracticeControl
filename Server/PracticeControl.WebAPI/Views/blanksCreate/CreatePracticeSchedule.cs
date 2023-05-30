namespace PracticeControl.WebAPI.Views.blanksCreate
{
    public class CreatePracticeSchedule
    {
            public int PracticeID { get; set; }
            public int GroupID { get; set; }
            public int EmployeeId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

    }
}
