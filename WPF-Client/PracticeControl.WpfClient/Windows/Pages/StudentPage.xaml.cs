using PracticeControl.WpfClient.API;
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
    /// <summary>
    /// Логика взаимодействия для StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {

        private List<StudentView> StudentViews { get; set; }
        public StudentPage()
        {
            InitializeComponent();
            StudentData();
        }

        private async void StudentData()
        {
            StudentViews = await GetRequests.GetAllStudentsAsync();

            if (StudentViews is null)
            {
                return;
            }

            var students = new List<StudentForm>();

            foreach (var studentView in StudentViews)
            {
                students.Add(new StudentForm
                {
                    StudentName = studentView.LastName + " " + studentView.FirstName + " " + studentView.MiddleName,
                    Login = studentView.Login,
                    GroupName = studentView.Group.GroupName
                });
            }

            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = students;
        }

        private void bttnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentEditModalWindow studentEditModalWindow = new StudentEditModalWindow(StudentViews);
            studentEditModalWindow.ShowDialog();
            StudentData();
        }

        private async void editStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var studentForm = (StudentForm)dataGridStudents.SelectedItem;
                if (studentForm is not null)
                {
                    StudentEditModalWindow studentEditWindow = new StudentEditModalWindow(studentForm);
                    studentEditWindow.ShowDialog();

                    StudentData();
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        //Удаление студента
        private async void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteStudent = dataGridStudents.SelectedItem as StudentForm;
                    var deleteStudentLogin = deleteStudent.Login;

                    var response = await DeleteRequests.DeleteStudentAsync(deleteStudentLogin);

                    if (response is not null)
                    {
                        MessageBox.Show("Студент удален");
                        StudentData();
                        return;
                    }
                    MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при удалении", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }//Готово

        private void StudentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            StudentData();
        }
    }
    public class StudentForm
    {
        public string StudentName { get; set; }
        public string Login { get; set; }
        public string GroupName { get; set; }

    }
}
