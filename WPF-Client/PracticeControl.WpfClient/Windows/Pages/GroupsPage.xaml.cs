using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model;
using PracticeControl.WpfClient.Model.View;
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
        {//вилка на админа

            var allGroups = await Requests.GetGroupsAsync();

            if (allGroups is null)
            {
                MessageBox.Show("Групп нет");
                return;
            }

            dataGridGroups.ItemsSource = null;
            dataGridGroups.ItemsSource = allGroups;
        }

        private void StudentsData(GroupView group)
        {//вилка на админа

            tbGroupName.Text = group.GroupName;

            var Students = new List<Student>();

            foreach (var item in group.StudentsView)
            {
                var student = new Student
                {
                    StudentName = $"{item.LastName} {item.FirstName} {item.MiddleName}"
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

            var selectedGroup = (GroupView)dataGridGroups.SelectedItem;

            StudentsData(selectedGroup);
        }

        private void bttnBackGroup_Click(object sender, RoutedEventArgs e)
        {
            gridGroup.Visibility = Visibility.Visible;
            gridStudents.Visibility = Visibility.Hidden;

            GroupsData();
        }
    }

    class Student
    {
        public string StudentName { get; set; }
    }
    
}
