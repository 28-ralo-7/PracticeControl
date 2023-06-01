namespace PracticeControl.WebAPI.Views.View
{
    public class StudentView
    {
        public int StudentID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Login { get; set; }
        public GroupView Group { get; set; }
    }
}
