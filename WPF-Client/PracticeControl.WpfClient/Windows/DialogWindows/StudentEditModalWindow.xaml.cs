using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using System.Linq;
using System.Windows;

namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для StudentEditModalWindow.xaml
    /// </summary>
    public partial class StudentEditModalWindow : Window
    {
        private StudentOut Student { get; set; }

        public StudentEditModalWindow(StudentOut? student)
        {
            Student = student;

            InitializeComponent();

            StudentEditData();
        }

        private void StudentEditData()
        {
            textBoxLastName.Text = Student.StudentName.Split(' ')[0];
            textBoxFirstName.Text = Student.StudentName.Split(' ')[1];
            textBoxMiddleName.Text = Student.StudentName.Split(' ')[2];
            textBoxLogin.Text = Student.Login;
        }

        private async void buttonEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text) ||
                   string.IsNullOrWhiteSpace(textBoxFirstName.Text) ||
                   string.IsNullOrWhiteSpace(textBoxMiddleName.Text) ||
                   string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            //var allStudents = await GetRequests.GetAllStudentsAsync();

            //var count = allStudents
            //  .Where(x => x.Login.ToLower() == textBoxLogin.Text.ToLower())
            //  .Count();

            //if (count > 0)
            //{
            //    MessageBox.Show("Данный логин занят");
            //    return;
            //}

            DialogResult = true;
            this.Close();
        }
    }
}

