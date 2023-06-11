using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.Win32;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Helpers;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Model.ViewOut;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static PracticeControl.WpfClient.Helpers.ValidationTools;


namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    public partial class GroupModalWindow : Window
    {

        private List<GroupView> Groups { get; set; }
        private List<StudentView> studentViews { get; set; }
        private List<CreateStudentView> Students { get; set; }
        private string oldName { get; set; }
        private List<StudentView> AllStudents { get; set; }

        //Создание новой группы из Excel
        public GroupModalWindow()
        {
            InitializeComponent();
            this.Groups = GetRequests.GetGroupsAsync().Result;
            buttonCreateNewGroupWithExcel.Visibility = Visibility.Visible;


        }

        //Переименование группы
        public GroupModalWindow(string name)
        {
            InitializeComponent();

            this.Groups = GetRequests.GetGroupsAsync().Result;

            textBoxGroupName.Text = name;
            oldName = name;
            stackPanelForExcel.Visibility = Visibility.Collapsed;
            buttonRenameGroup.Visibility = Visibility.Visible;
            this.Height = 250;

        }

        //Файловый диалог
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

        //Обновление таблицы студентов
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

        //Изменение студента в группе
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

        //Удаление студента группы #Дописать
        private void deleteStudent_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //Добавление с Excel
        private async void buttonCreateNewGroupWithExcel_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = new CreateGroupView();

            if (string.IsNullOrWhiteSpace(textBoxGroupName.Text))
            {
                MessageBox.Show("Введите название группы");
                return;
            }

            var checkUnique = await PostRequests.CheckUniqueGroup(textBoxGroupName.Text);

            if (checkUnique)
            {
                MessageBox.Show("Группа с таким названием уже существует");
                return;
            }

            AllStudents = await GetRequests.GetAllStudentsAsync();

            int loginVerification = 0;
            if (dataGridStudents.ItemsSource is not null)
            {
                loginVerification = AllStudents
                .Where(employeeView => Students
                    .Select(x => x.Login.ToLower())
                    .Contains(employeeView.Login.ToLower()))
                .Count();

                if (loginVerification > 0)
                {
                    MessageBox.Show("Этот логин занят");
                    return;
                }

                newGroup.Students = Students;
            }
            
            newGroup.GroupName = textBoxGroupName.Text;

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

        //Переименование группы
        private async void buttonRenameGroup_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupName.Text))
            {
                MessageBox.Show("Введите название группы");
                return;
            }

            if (Groups.Any(group => group.GroupName == textBoxGroupName.Text.Trim()))
            {
                MessageBox.Show("Группа с таким названием уже существует");
                return;
            }

            try
            {
                var response = await UpdateRequests.RenameGroup(textBoxGroupName.Text, oldName);

                if (response is not null)
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

        //Отчистка пробелов
        private void textBoxGroupName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ClearWhiteSpace(sender);
        }

        //Пропуск только цифры
        private void textBoxGroupName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            AllowOnlyNumber(e);
        }
    }
}
