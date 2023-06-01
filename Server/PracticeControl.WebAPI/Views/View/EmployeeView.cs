namespace PracticeControl.WebAPI.Views.View
{
    public class EmployeeView//Руководители практики/админы
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string?  MiddleName { get; set; }
        public string Login { get; set; }
        public bool IsAdmin { get; set; }
        public List<PracticeScheduleView>? PracticeSchedules { get; set; }
    }
}
