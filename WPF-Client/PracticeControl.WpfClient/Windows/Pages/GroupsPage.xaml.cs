using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model;
using PracticeControl.WpfClient.Model.View;
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


            var Groups = new List<Group>();

            foreach (var item in allGroups)
            {
                var group = new Group
                {
                    GroupView = item,
                    CountStudents = item.StudentsView.Count,
                };

                Groups.Add(group);
            }


            dataGridGroups.ItemsSource = null;
            dataGridGroups.ItemsSource = Groups;
        }

        private void StudentsData(GroupView group)
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

            var Students = new List<Student>();

            foreach (var item in group.StudentsView)
            {
                var student = new Student
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

            var selectedGroup = (Group)dataGridGroups.SelectedItem;

            StudentsData(selectedGroup.GroupView);
        }

        private void bttnBackGroup_Click(object sender, RoutedEventArgs e)
        {
            gridGroup.Visibility = Visibility.Visible;
            gridStudents.Visibility = Visibility.Hidden;

            GroupsData();
        }

        private void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            GroupModalWindow createGroup = new GroupModalWindow();
            createGroup.ShowDialog();

            GroupsData();
        }
    }


    class Group
    {
        public GroupView GroupView { get; set; }
        public int CountStudents { get; set; }
    }

    class Student
    {
        public string StudentName { get; set; }
        public string Login { get; set; }
    }
    
}
