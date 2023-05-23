using Microsoft.Win32;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Helpers;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    public partial class GroupModalWindow : Window
    {

        private List<GroupOut> Groups { get; set; }
        private List<StudentView> studentViews { get; set; }
        private List<CreateStudentView> Students { get; set; }

        private List<StudentView> AllStudents { get; set; }
        public GroupModalWindow(List<GroupOut> Groups)
        {
            this.Groups = Groups;

            InitializeComponent();
        }

        private async void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = new CreateGroupView();

            if (string.IsNullOrWhiteSpace(textBoxGroupName.Text))
            {
                MessageBox.Show("Введите название группы");
                return;
            }

            if (Groups.Any(x => x.GroupView.GroupName == textBoxGroupName.Text.Trim()))
            {
                MessageBox.Show("Группа с таким названием уже существует");
                return;
            }

            if (Students.Count == 0)
            {
                MessageBox.Show("Заполните студентов");
                return;
            }

            AllStudents = await GetRequests.GetAllStudentsAsync();

            int loginVerification = AllStudents
                .Where(employeeView => Students
                    .Select(x => x.Login.ToLower())
                    .Contains(employeeView.Login.ToLower()))
                .Count();

            if (loginVerification > 0)
            {
                MessageBox.Show("Этот логин занят");
                return;
            }


            newGroup.GroupName = textBoxGroupName.Text;
            newGroup.Students = Students;

            try
            {
                var createdGroup = await PostRequests.CreateGroupAsync(newGroup);

                if ((bool)createdGroup)
                {
                    MessageBox.Show("Группа успешно сохранена");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить группу");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
       
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;


                Students = ExcelParser.LoadFile(filePath);

                if (Students.Count != 0)
                {
                    DataGridStudentsData();
                }
            }
        }

        private async void DataGridStudentsData()
        {
            var StudentsOut = new List<StudentOut>();


            foreach (var item in Students)
            {
                StudentsOut.Add(new StudentOut
                {
                    StudentName = item.LastName + " " + item.FirstName + " " + item.MiddleName,
                    Login = item.Login,
                });
            }

            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = StudentsOut;
        }

        private void editStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            var studentOut = (StudentOut)dataGridStudents.SelectedItem;

            string lastName = studentOut.StudentName.Split(' ')[0];
            string firstName = studentOut.StudentName.Split(' ')[1];
            string middleName = studentOut.StudentName.Split(' ')[2];

            var studentEdit = Students.First(x => x.LastName == lastName
            && x.FirstName == firstName
            && x.MiddleName == middleName
            && x.Login == studentOut.Login);


            StudentEditModalWindow studentEditWindow = new StudentEditModalWindow(studentOut, true);
            studentEditWindow.ShowDialog();

            if (studentEditWindow.DialogResult.HasValue && studentEditWindow.DialogResult.Value)
            {
                studentEdit.LastName = studentEditWindow.lastName_TextBox.Text;
                studentEdit.FirstName = studentEditWindow.firstName_TextBox.Text;
                studentEdit.MiddleName = studentEditWindow.middleName_TextBox.Text;
                studentEdit.Login = studentEditWindow.login_TextBox.Text;

                MessageBox.Show("Изменения прошли успешно");
            }

            DataGridStudentsData();
        }

        private void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
