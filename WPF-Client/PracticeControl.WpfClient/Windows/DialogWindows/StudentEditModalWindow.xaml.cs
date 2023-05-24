using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Model.ViewUpdate;
using PracticeControl.WpfClient.Windows.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для StudentEditModalWindow.xaml
    /// </summary>
    public partial class StudentEditModalWindow : Window
    {
        private StudentOut Student { get; set; }
        private StudentForm StudentForm { get; set; }

        private List<StudentView> AllStudents { get; set; }

        private bool IsExcelStudent { get; set; } = false;

        public StudentEditModalWindow(StudentOut? student, bool isExcelStudent)
        {
            Student = student;

            IsExcelStudent = isExcelStudent;

            InitializeComponent();

            editStudent_Button.Visibility = Visibility.Visible;
            this.Height = 600;
            group_ComboBox.Visibility = Visibility.Collapsed;
            addStudent_Button.Visibility = Visibility.Collapsed;
            group_TextBlock.Visibility = Visibility.Collapsed;
            title_Label.Content = "Изменение студента";

            StudentEditData();
        }
        public StudentEditModalWindow(StudentForm student)
        {
            StudentForm = student;

            InitializeComponent();

            editStudent_Button.Visibility = Visibility.Visible;
            this.Height = 600;
            group_ComboBox.Visibility = Visibility.Collapsed;
            addStudent_Button.Visibility = Visibility.Collapsed;
            group_TextBlock.Visibility = Visibility.Collapsed;
            title_Label.Content = "Изменение студента";

            StudentEditData();
        }
        public StudentEditModalWindow(List<StudentView> student)
        {
            AllStudents = student;       
            
            InitializeComponent();

            gridStudentModal.RowDefinitions.Insert(7, new RowDefinition());
            this.Height = 700;
            group_ComboBox.Visibility = Visibility.Visible;
            addStudent_Button.Visibility = Visibility.Visible;
            group_TextBlock.Visibility = Visibility.Visible;
            title_Label.Content = "Добавление студента";

            var groups = GetRequests.GetGroupsAsync().Result.ToList();

            var groupsName = groups.Select(group => group.GroupName).ToList();

            group_ComboBox.ItemsSource = groupsName;
            group_ComboBox.SelectedIndex = 0;

        }

        private void StudentEditData()
        {
            if (StudentForm is null)
            {
                lastName_TextBox.Text = Student.StudentName.Split(' ')[0];
                firstName_TextBox.Text = Student.StudentName.Split(' ')[1];
                middleName_TextBox.Text = Student.StudentName.Split(' ')[2];
                login_TextBox.Text = Student.Login;
            }
            else
            {
                lastName_TextBox.Text = StudentForm.StudentName.Split(' ')[0];
                firstName_TextBox.Text = StudentForm.StudentName.Split(' ')[1];
                middleName_TextBox.Text = StudentForm.StudentName.Split(' ')[2];
                login_TextBox.Text = StudentForm.Login;
            }
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
            UpdateStudentView studentView = new UpdateStudentView();

            //Для обновления в группе
            if (StudentForm is null)
            {
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

                 studentView = new UpdateStudentView
                {
                    LastName = lastName_TextBox.Text,
                    FirstName = firstName_TextBox.Text,
                    MiddleName = middleName_TextBox.Text,
                    Login = login_TextBox.Text,
                    Password = password_TextBox.Text,
                    GroupName = Student.Group.GroupName,
                    LoginForSearch = Student.Login
                };
            }
            //Для обновления со станицы Студенты
            else
            {
                int loginVerification = AllStudents
                    .Where(employeeView =>
                    employeeView.Login.ToLower() == login_TextBox.Text.ToLower() &&
                    employeeView.Login.ToLower() != StudentForm.Login.ToLower())
                    .Count();

                if (loginVerification > 0)
                {
                    MessageBox.Show("Этот логин занят");
                    return;
                }

                studentView = new UpdateStudentView
                {
                    LastName = lastName_TextBox.Text,
                    FirstName = firstName_TextBox.Text,
                    MiddleName = middleName_TextBox.Text,
                    Login = login_TextBox.Text,
                    Password = password_TextBox.Text,
                    GroupName = StudentForm.GroupName,
                    LoginForSearch = StudentForm.Login
                };
            }

            // Общий блок для запроса к API
            try
            {
                var response = await UpdateRequests.UpdateStudentAsync(studentView);
                if ((bool)response)
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

        private async void addStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastName_TextBox.Text) ||
               string.IsNullOrWhiteSpace(firstName_TextBox.Text) ||
               string.IsNullOrWhiteSpace(middleName_TextBox.Text) ||
               string.IsNullOrWhiteSpace(login_TextBox.Text) ||
               string.IsNullOrWhiteSpace(password_TextBox.Text)
               )
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

            CreateStudentView studentView = new CreateStudentView
            {
                LastName = lastName_TextBox.Text,
                FirstName = firstName_TextBox.Text,
                MiddleName = middleName_TextBox.Text,
                Login = login_TextBox.Text,
                Password = password_TextBox.Text,
                GroupName = group_ComboBox.SelectedItem.ToString()
                
            };

            try
            {
                var response = await PostRequests.CreateStudentAsync(studentView);
                if ((bool)response)
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

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

