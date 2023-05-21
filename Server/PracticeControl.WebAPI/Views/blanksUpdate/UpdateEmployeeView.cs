namespace PracticeControl.WebAPI.Views.blanksUpdate
{
    public class UpdateEmployeeView
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string LoginForSearch { get; set; }
    }
}
