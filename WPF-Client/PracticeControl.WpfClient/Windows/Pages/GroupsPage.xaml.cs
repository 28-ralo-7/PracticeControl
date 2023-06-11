using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Helpers;
using PracticeControl.WpfClient.Model;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewOut;
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
                MessageBox.Show("Групп нет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        }//Готово

        //Открытие студентов группы
        private void dataGridGroups_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {


                    SelectedGroup = (GroupOut)dataGridGroups.SelectedItem;
                    if (SelectedGroup is not null)
                    {
                        gridGroup.Visibility = Visibility.Hidden;
                        gridStudents.Visibility = Visibility.Visible; 
                        StudentsData(SelectedGroup.GroupView);
                    }

                }
            }
            catch
            {

            }

        }//Готово

        //Добавление группы
        private void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            GroupModalWindow createGroup = new GroupModalWindow();
            createGroup.ShowDialog();

            GroupsData();
        }

        //Вывод студентов
        private async void StudentsData(GroupView group)
        {
            if (User.IsAdmin)
            {
                columnStudentLogin.Visibility = Visibility.Visible;
                contextMenuStudents.Visibility = Visibility.Visible;

            }
            else
            {
                contextMenuStudents.Visibility = Visibility.Collapsed;
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
        }//Готово

        //Возврат к списку групп
        private void bttnBackGroup_Click(object sender, RoutedEventArgs e)
        {
            gridGroup.Visibility = Visibility.Visible;
            gridStudents.Visibility = Visibility.Hidden;

            GroupsData();
        }//Готово

        //Изменение студента
        private async void editStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var studentOut = (StudentOut)dataGridStudents.SelectedItem;
                
                studentOut.Group = GroupView.FirstOrDefault(group => group.GroupName == SelectedGroup.GroupView.GroupName);

                

                StudentEditModalWindow studentEditWindow = new StudentEditModalWindow(studentOut, false);
                studentEditWindow.ShowDialog();

                if (studentEditWindow.DialogResult.HasValue && studentEditWindow.DialogResult.Value)
                {


                    MessageBox.Show("Изменение прошло успешно");
                }

                StudentsData(SelectedGroup.GroupView);
            }
            catch (Exception)
            {
                return;
            }
        }//Готово

        //Удаление студента
        private async void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteStudent = dataGridStudents.SelectedItem as StudentOut;
                    var deleteStudentLogin = deleteStudent.Login;

                    var response = await DeleteRequests.DeleteStudentAsync(deleteStudentLogin);

                    if (response is not null)
                    {
                        MessageBox.Show("Студент удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        StudentsData(SelectedGroup.GroupView);
                        return;
                    }
                    MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при удалении", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }//Готово

        //Изменение названия
        private async void editGroup_Button_Click(object sender, RoutedEventArgs e)
        {

            var group = dataGridGroups.SelectedItem as GroupOut;
            if (group is not null)
            {
                var groupName = group.GroupView.GroupName;
                GroupModalWindow groupModalWindow = new GroupModalWindow(groupName);
                groupModalWindow.ShowDialog();
                GroupsData();
            }

        }
        
        //Удаление группы
        private async void deleteGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteGroup = dataGridGroups.SelectedItem as GroupOut;
                    if(deleteGroup is not null)
                    {
                        var deleteGroupName = deleteGroup.GroupView.GroupName;
                        var response = await DeleteRequests.DeleteGroupAsync(deleteGroupName);

                        if (response is not null)
                        {
                            MessageBox.Show("Группа удалена");
                            GroupsData();
                            return;
                        }
                        MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при удалении", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }//Готово

        //Обновление при загрузке
        private void Groups_Page_Loaded(object sender, RoutedEventArgs e)
        {
            GroupsData();
        }
    }

}
