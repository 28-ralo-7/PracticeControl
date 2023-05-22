using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewUpdate;
using System.Collections.Generic;
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

        private List<StudentView> AllStudents { get; set; }

        private bool IsExcelStudent { get; set; } = false;

        public StudentEditModalWindow(StudentOut? student, bool isExcelStudent)
        {
            Student = student;

            IsExcelStudent = isExcelStudent;

            

            InitializeComponent();

            StudentEditData();
        }

        private void StudentEditData()
        {
            lastName_TextBox.Text = Student.StudentName.Split(' ')[0];
            firstName_TextBox.Text = Student.StudentName.Split(' ')[1];
            middleName_TextBox.Text = Student.StudentName.Split(' ')[2];
            login_TextBox.Text = Student.Login;
        }

        private async void EditStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastName_TextBox.Text) ||
                   string.IsNullOrWhiteSpace(firstName_TextBox.Text) ||
                   string.IsNullOrWhiteSpace(middleName_TextBox.Text) ||
                   string.IsNullOrWhiteSpace(login_TextBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            AllStudents = await GetRequests.GetAllStudentsAsync();

            int loginVerification = AllStudents
                    .Where(employeeView =>
                    employeeView.Login.ToLower() == login_TextBox.Text.ToLower() &&
                    employeeView.Login.ToLower() != Student.Login.ToLower())
                    .Count();

            if (loginVerification > 0)
            {
                MessageBox.Show("Этот логин занят");
                return;
            }

            if (IsExcelStudent) 
            {
                DialogResult = true;
                this.Close();
                return;
            }

            UpdateStudentView studentView = new UpdateStudentView
            {
                LastName = lastName_TextBox.Text,
                FirstName = firstName_TextBox.Text,
                MiddleName = middleName_TextBox.Text,
                Login = login_TextBox.Text,
                Password = password_TextBox.Text,
                GroupName = Student.Group.GroupName,
                LoginForSearch = Student.Login
            };
            try
            {
                var response = await UpdateRequests.UpdateStudentAsync(studentView);
                if (response is not null)
                {
                    MessageBox.Show("Студент сохранен");
                    Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить студента");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }
    }
}

