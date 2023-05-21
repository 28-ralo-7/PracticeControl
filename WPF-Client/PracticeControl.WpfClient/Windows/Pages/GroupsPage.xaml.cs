using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Helpers;
using PracticeControl.WpfClient.Model;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewUpdate;
using PracticeControl.WpfClient.Windows.DialogWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeControl.WpfClient.Windows.Pages
{
    public partial class GroupsPage : Page
    {
        private EmployeeView User { get; set; }

        private GroupOut SelectedGroup { get; set; }
 
        public GroupsPage(EmployeeView User)
        {
            this.User = User;

            InitializeComponent();

            GroupsData();
        }

        private async void GroupsData()
        {

            if (User.IsAdmin)
            { 
                stackPanelFuncAdminGroup.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanelFuncAdminGroup.Visibility = Visibility.Hidden;
            }

            var allGroups = await GetRequests.GetGroupsAsync();

            if (allGroups is null)
            {
                MessageBox.Show("Групп нет");
                return;
            }


            var Groups = new List<GroupOut>();

            foreach (var item in allGroups)
            {
                var group = new GroupOut
                {
                    GroupView = item,
                    CountStudents = item.StudentsView.Count,
                };

                Groups.Add(group);
            }


            dataGridGroups.ItemsSource = null;
            dataGridGroups.ItemsSource = Groups;
        }

        private async void StudentsData(GroupView group)
        {
            if (User.IsAdmin)
            {
                columnStudentLogin.Visibility = Visibility.Visible;
            }
            else
            {
                columnStudentLogin.Visibility = Visibility.Collapsed;
                columnStudentName.Width = new DataGridLength(1040);
            }

            textBoxGroupName.Text = group.GroupName;

            //var studentsGroup = await GetRequests.GetStudentsGroupAsync(group.GroupName);

            //if (studentsGroup == null)
            //{
            //    return;
            //}

            var Students = new List<StudentOut>();

            foreach (var item in group.StudentsView)
            {
                var student = new StudentOut
                {
                    StudentName = $"{item.LastName} {item.FirstName} {item.MiddleName}",
                    Login = item.Login
                };

                Students.Add(student);
            }

            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = Students.OrderBy(b=>b.StudentName);
        }

        private void dataGridGroups_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridGroup.Visibility = Visibility.Hidden;
            gridStudents.Visibility = Visibility.Visible;

            SelectedGroup = (GroupOut)dataGridGroups.SelectedItem;

            StudentsData(SelectedGroup.GroupView);
        }

        private void bttnBackGroup_Click(object sender, RoutedEventArgs e)
        {
            gridGroup.Visibility = Visibility.Visible;
            gridStudents.Visibility = Visibility.Hidden;

            GroupsData();
        }

        private void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            var groups = (List<GroupOut>)dataGridGroups.ItemsSource;

            GroupModalWindow createGroup = new GroupModalWindow(groups);
            createGroup.ShowDialog();

            GroupsData();
        }

        private async void editStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            var studentOut = (StudentOut)dataGridStudents.SelectedItem;

            string lastName = studentOut.StudentName.Split(' ')[0];
            string firstName = studentOut.StudentName.Split(' ')[1];
            string middleName = studentOut.StudentName.Split(' ')[2];

            var studentEdit = SelectedGroup.GroupView.StudentsView.First(x => x.LastName == lastName
            && x.FirstName == firstName
            && x.MiddleName == middleName
            && x.Login == studentOut.Login);

            var updateStudent = new UpdateStudentView();

            StudentEditModalWindow studentEditWindow = new StudentEditModalWindow(studentOut);
            studentEditWindow.ShowDialog();

            if (studentEditWindow.DialogResult.HasValue && studentEditWindow.DialogResult.Value)
            {
                updateStudent.LoginForSearch = studentEdit.Login;
                updateStudent.LastName = studentEditWindow.textBoxLastName.Text;
                updateStudent.FirstName = studentEditWindow.textBoxFirstName.Text;
                updateStudent.MiddleName = studentEditWindow.textBoxMiddleName.Text;
                updateStudent.Login = studentEditWindow.textBoxLogin.Text;
                updateStudent.GroupName = SelectedGroup.GroupView.GroupName;

                //var response = await UpdateRequests.UpdateStudentAsync(updateStudent);

                //if (response == null)
                //{
                //    MessageBox.Show("Изменение не удалось");
                //    return;
                //}

                MessageBox.Show("Изменение прошло успешно");
            }

            StudentsData(SelectedGroup.GroupView);
        }

        private void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
