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
        private List<GroupView> GroupView { get; set; }
 
        public GroupsPage(EmployeeView User)
        {
            this.User = User;
            InitializeComponent();
            GroupsData();
        }

        //Вывод групп
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

            GroupView = await GetRequests.GetGroupsAsync();

            if (GroupView is null)
            {
                MessageBox.Show("Групп нет");
                return;
            }


            var Groups = new List<GroupOut>();

            foreach (var item in GroupView)
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
        //Открытие студентов группы
        private void dataGridGroups_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridGroup.Visibility = Visibility.Hidden;
            gridStudents.Visibility = Visibility.Visible;

            SelectedGroup = (GroupOut)dataGridGroups.SelectedItem;

            StudentsData(SelectedGroup.GroupView);
        }
        //Добавление группы
        private void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            var groups = (List<GroupOut>)dataGridGroups.ItemsSource;

            GroupModalWindow createGroup = new GroupModalWindow(groups);
            createGroup.ShowDialog();

            GroupsData();
        }
        //Вывод студентов
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

            var studentsGroup = await GetRequests.GetStudentsGroupAsync(group.GroupName);

            if (studentsGroup == null)
            {
                return;
            }

            textBoxGroupName.Text = group.GroupName;

            List<StudentOut> studentsList = new List<StudentOut>();

            foreach (var item in studentsGroup)
            {
                var student = new StudentOut
                {
                    StudentName = $"{item.LastName} {item.FirstName} {item.MiddleName}",
                    Login = item.Login,

                };

                studentsList.Add(student);
            }

            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = studentsList.OrderBy(b => b.StudentName);
        }
        //Возврат к списку групп
        private void bttnBackGroup_Click(object sender, RoutedEventArgs e)
        {
            gridGroup.Visibility = Visibility.Visible;
            gridStudents.Visibility = Visibility.Hidden;

            GroupsData();
        }
        //Изменение студента
        private async void editStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            var studentOut = (StudentOut)dataGridStudents.SelectedItem;

            studentOut.Group = GroupView.FirstOrDefault(group => group.GroupName == SelectedGroup.GroupView.GroupName);

            var updateStudent = new UpdateStudentView();

            StudentEditModalWindow studentEditWindow = new StudentEditModalWindow(studentOut, false);
            studentEditWindow.ShowDialog();

            if (studentEditWindow.DialogResult.HasValue && studentEditWindow.DialogResult.Value)
            {


                MessageBox.Show("Изменение прошло успешно");
            }

            StudentsData(SelectedGroup.GroupView);
        }
        //Удаление студента
        private void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
