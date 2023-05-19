using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Helpers;
using PracticeControl.WpfClient.Model.ViewCreate;
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
        public GroupModalWindow()
        {
            InitializeComponent();
        }

        private void buttonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = new CreateGroupView();

            var students = (List<CreateStudentView>)dataGridStudents.ItemsSource;

            if (string.IsNullOrWhiteSpace(textBoxGroupName.Text))
            {
                MessageBox.Show("Введите название группы");
                return;
            }

            if (students.Count == 0)
            {
                MessageBox.Show("Заполните студентов");
                return;
            }

            foreach (var item in students)
            {
                item.Group = newGroup;
            }

            newGroup.GroupName = textBoxGroupName.Text;
            newGroup.Students = students;

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

                var students = ExcelParser.LoadFile(filePath);

                if (students != null)
                {
                    DataGridStudentsData(students);
                }
            }
        }

        private void DataGridStudentsData(List<CreateStudentView> students)
        {
            var Students = new List<StudentOut>();

            foreach (var item in students)
            {
                Students.Add(new StudentOut
                {
                    StudentName = item.LastName + " " + item.FirstName + " " + item.LastName,
                    Login = item.Login,
                });
            }

            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = Students;
        }
    }

    class StudentOut
    {
        public string StudentName { get; set; }
        public string Login { get; set; }
    }
}
