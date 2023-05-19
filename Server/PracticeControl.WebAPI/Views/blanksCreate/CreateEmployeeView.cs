namespace PracticeControl.WebAPI.Views.blanksCreate
{
    public class CreateEmployeeView //Для создания нового сотрудника
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
