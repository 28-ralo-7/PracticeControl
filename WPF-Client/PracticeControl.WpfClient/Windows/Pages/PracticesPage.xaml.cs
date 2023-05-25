using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticeControl.WpfClient.Windows.Pages
{

    public partial class PracticesPage : Page
    {
        private EmployeeView User { get; set; }

        private List<DateTime> PracticeDates { get; set; } = new List<DateTime>();
        private DateTime SelectDate { get;set; }
        private PracticeScheduleView SelectPractice { get;set; }
        public PracticesPage(EmployeeView User)
        {
            this.User = User;

            

            InitializeComponent();

            PracticesData();
        }

        #region
        private async void PracticesData()
        {
            if (User.IsAdmin)
            {
                columnPracticeLead.Visibility = Visibility.Visible;
                stPanFuncAdminPractices.Visibility = Visibility.Visible;

                var Practices = await GetRequests.GetAllPracticesAsync();

                dataGridPractices.ItemsSource = Practices;
            }
            else
            {
                columnPracticeLead.Visibility = Visibility.Hidden;
                stPanFuncAdminPractices.Visibility = Visibility.Hidden;

                var Practices = User.PracticeSchedules;

                dataGridPractices.ItemsSource = Practices;
            }
        }

       

        private void dataGridPractices_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridPractices.Visibility = Visibility.Hidden;
            gridAttendance.Visibility = Visibility.Visible;

            var practiceSchedule = (PracticeScheduleView)dataGridPractices.SelectedItem;

            AttendanceData(practiceSchedule);
        }

        private void backPractices_Click(object sender, RoutedEventArgs e)
        {
            gridPractices.Visibility = Visibility.Visible;
            gridAttendance.Visibility = Visibility.Hidden;

            PracticesData();
        }

        private void PracticesPage_GotFocus(object sender, RoutedEventArgs e)
        {
            PracticesData();
        }

#endregion



        private void AttendanceData(PracticeScheduleView practiceSchedule)
        {
            SelectPractice = practiceSchedule;

            if (PracticeDates.Count == 0)
            {
                AttendanceDates(SelectPractice);
            }


            textBlockDayAttendance.Text = SelectDate.ToShortDateString().Replace(".2023", "");

            var attendances = new List<AttendanceView>();

            foreach (var item in SelectPractice.Attendances.Where(x=>Convert.ToDateTime(x.Date) == SelectDate))
            {
                string path = Environment.CurrentDirectory + item.Photo;

                attendances.Add(new AttendanceView
                {
                    Date = Convert.ToDateTime(item.Date),
                    AttendanceID = item.AttendanceID,
                    StudentName = item.StudentView.LastName + " " + item.StudentView.FirstName + " " + item.StudentView.MiddleName,
                    IsPresence = item.IsPresent,
                    Photo = path,
                });
            }

            dataGridAttendance.ItemsSource = attendances;
        }

        private void AttendanceDates(PracticeScheduleView practice)
        {
            PracticeDates = new List<DateTime>();

            for (DateTime i = Convert.ToDateTime(practice.StartDate); i <= Convert.ToDateTime(practice.EndDate); i = i.AddDays(1))
            {
                PracticeDates.Add(i);
            }

            SelectDate = PracticeDates[0];
            textBlockDatePractice.Text = $"c {PracticeDates[0].ToShortDateString().Replace(".2023", "")} по {PracticeDates[PracticeDates.Count-1].ToShortDateString().Replace(".2023", "")}";
        }

        private void Presence_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonBackDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime beginDate = PracticeDates[0];

            if (SelectDate>=beginDate)
            {
                SelectDate = SelectDate.AddDays(-1);
                AttendanceData(SelectPractice);
                return;
            }
        }

        private void buttonNextDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime endDate = PracticeDates[PracticeDates.Count-1];

            if (SelectDate < endDate)
            {
                SelectDate = SelectDate.AddDays(1);
                AttendanceData(SelectPractice);
                return;
            }
        }
    }

    public class AttendanceView
    {
        public int AttendanceID { get; set; }
        public string StudentName { get; set; }
        public string Photo { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresence { get; set; }
    }
}
