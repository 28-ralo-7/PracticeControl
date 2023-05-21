using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Helpers;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Windows.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    public partial class GroupModalWindow : Window
    {

        private List<GroupOut> Groups { get; set; }
        private List<CreateStudentView> Students { get; set; }
        public GroupModalWindow(List<GroupOut> Groups)
        {
            this.Groups = Groups;

            InitializeComponent();
        }

        private void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
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

            foreach (var item in Students)
            {
                item.Group = newGroup;
            }

            newGroup.GroupName = textBoxGroupName.Text;
            newGroup.Students = Students;

            try
            {
                var createdGroup = PostRequests.CreateGroupAsync(newGroup);

                if (createdGroup is not null)
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

        private void DataGridStudentsData()
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


            StudentEditModalWindow studentEditWindow = new StudentEditModalWindow(studentOut);
            studentEditWindow.ShowDialog();

            if (studentEditWindow.DialogResult.HasValue && studentEditWindow.DialogResult.Value)
            {
                studentEdit.LastName = studentEditWindow.textBoxLastName.Text;
                studentEdit.FirstName = studentEditWindow.textBoxFirstName.Text;
                studentEdit.MiddleName = studentEditWindow.textBoxMiddleName.Text;
                studentEdit.Login = studentEditWindow.textBoxLogin.Text;

                MessageBox.Show("Изменения прошли успешно");
            }

            DataGridStudentsData();
        }

        private void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
