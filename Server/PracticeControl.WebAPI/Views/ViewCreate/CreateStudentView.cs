namespace PracticeControl.WebAPI.Views.ViewCreate
{
    public class CreateStudentView
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? GroupName { get; set; }
    }
}
