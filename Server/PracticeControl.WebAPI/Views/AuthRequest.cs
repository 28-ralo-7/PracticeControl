namespace PracticeControl.WebAPI.Views
{
    public class AuthRequest
    {
        public string Login { get; set; } = null!;
        public string PasswordString { get; set; } = null!;

        public AuthRequest(string Login, string PasswordString)
        {
            this.Login = Login;
            this.PasswordString = PasswordString;
        }
    }
}
