using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
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
    /// <summary>
    /// Логика взаимодействия для PracticeScheduleModalWindow.xaml
    /// </summary>
    public partial class PracticeScheduleModalWindow : Window
    {
        private List<PracticeView> practices;
        private List<EmployeeView> employees;
        private List<GroupView> groups;
        private PracticeScheduleView practiceScheduleView;

        //Создание расписания
        public PracticeScheduleModalWindow()
        {
            InitializeComponent();

            create_Button.Visibility = Visibility.Visible;

            PracticeData();
        }

        public PracticeScheduleModalWindow(PracticeScheduleView practiceView)
        {
            InitializeComponent();

            this.practiceScheduleView = practiceView;
            edit_Button.Visibility = Visibility.Visible;

            PracticeData();
        }

        public async void PracticeData()
        {
            practices = await GetRequests.GetAllPracticeAsync();
            employees = await GetRequests.GetAllEmployeesAsync();
            groups = await GetRequests.GetGroupsAsync();

            //Для создания
            if (practiceScheduleView is null)
            {
                //Группа
                group_ComboBox.ItemsSource = groups.Select(group => group.GroupName).ToList();
                group_ComboBox.SelectedIndex = 0;

                //Практика
                practice_ComboBox.ItemsSource = practices
                    .Select(practice => practice.Abbreviation + " " + practice.PracticeModule + " " + $"({practice.Specialty})");
                practice_ComboBox.SelectedIndex = 0;

                //Сотрудник
                employee_ComboBox.ItemsSource = employees
                    .Select(employee => employee.LastName + " " + employee.FirstName + " " + employee.MiddleName).ToList();
                employee_ComboBox.SelectedIndex = 0;

                //Даты
                dateStart_ComboBox.Text = DateTime.Now.ToShortDateString();
                dateEnd_ComboBox.Text = DateTime.Now.ToShortDateString();
            }

            //Для изменения
            else
            { 
                //Группа
                group_ComboBox.ItemsSource = groups.Select(group => group.GroupName).ToList();
                group_ComboBox.SelectedItem = practiceScheduleView.Group.GroupName;

                //Практика
                practice_ComboBox.ItemsSource = practices
                    .Select(practice => practice.Abbreviation + " | " + practice.PracticeModule + " " + $"({practice.Specialty})")
                    .ToList();
                
                practice_ComboBox.SelectedItem = practiceScheduleView.Abbreviation + " | " + 
                                                practiceScheduleView.PracticeModule + " " + 
                                                $"({practiceScheduleView.Specialty})";
                
                //Сотрудник
                employee_ComboBox.ItemsSource = employees
                    .Select(employee => employee.LastName + " " + employee.FirstName[0] + "." + employee.MiddleName[0] + ".")
                    .ToList();
                
                employee_ComboBox.SelectedItem = practiceScheduleView.Employee.LastName + " " + 
                                                practiceScheduleView.Employee.FirstName[0] + "." +
                                                practiceScheduleView.Employee.MiddleName[0] + ".";

                //Даты
                dateStart_ComboBox.Text = practiceScheduleView.StartDate;
                dateEnd_ComboBox.Text = practiceScheduleView.EndDate;
            }
        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void create_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DateOnly.Parse(dateStart_ComboBox.Text) >= DateOnly.Parse(dateEnd_ComboBox.Text))
            {
                MessageBox.Show("Дата конца практики не иожет быть раньше даты начала", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CreatePracticeView createPracticeView = new CreatePracticeView
            {
                PracticeName = practice_ComboBox.SelectedItem.ToString(),
                PracticeLead = employee_ComboBox.SelectedItem.ToString(),
                GroupName = group_ComboBox.SelectedItem.ToString(),
                DateStart = dateStart_ComboBox.Text,
                DateEnd = dateEnd_ComboBox.Text
            };

            var isValidDate = await PostRequests.CheckValidDateForPractice(createPracticeView);

            try
            {
                if (isValidDate)
                {
                    var response = await PostRequests.CreatePracticeSchedule(createPracticeView);

                    if (response)
                    {
                        MessageBox.Show("Расписание практики сохранено", "Успешное сохранение", MessageBoxButton.OK);
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сохранить расписание практики", "Сохранение не удалось",MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                MessageBox.Show("У группы не может быть более одной практики одновременно", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("У группы не может быть более одной практики одновременно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



        }

        private void edit_Button_Click(object sender, RoutedEventArgs e)
        {

        }


    }

}
