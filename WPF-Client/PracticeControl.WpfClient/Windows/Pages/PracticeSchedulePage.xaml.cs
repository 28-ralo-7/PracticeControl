using DocumentFormat.OpenXml.Wordprocessing;
using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewUpdate;
using PracticeControl.WpfClient.Windows.DialogWindows;
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
        private DateTime SelectDate { get; set; }
        private PracticeScheduleView SelectPractice { get; set; }
        private List<AttendanceViewDaniil> AttendanceRows { get; set; }

        private List<AttendanceView> AttendanceAll { get; set; }//главная
        private List<AttendanceView> CurrentAttendance { get; set; }//используемая страница
        private List<UpdateAttendanceView> UpdateAttendance { get; set; } = new List<UpdateAttendanceView>();
        public PracticesPage(EmployeeView User)
        {
            this.User = User;

            InitializeComponent();

            PracticesData();
        }

        #region Расписание практик
        private async void PracticesData()
        {
            if (User.IsAdmin)
            {
                columnPracticeLead.Visibility = Visibility.Visible;
                stPanFuncAdminPractices.Visibility = Visibility.Visible;

                var Practices = await GetRequests.GetAllPracticeSchedulesAsync();

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

            PracticeDates.Clear();
            SelectPractice = (PracticeScheduleView)dataGridPractices.SelectedItem;
            AttendanceAll = SelectPractice.Attendances;

            AttendanceData();
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

        private void buttonAddPracitce_Click(object sender, RoutedEventArgs e)
        {
            PracticeScheduleModalWindow createPractice = new PracticeScheduleModalWindow();
            createPractice.ShowDialog();
            PracticesData();
        }


        #endregion



        private async void AttendanceData() // вывод практик по дням
        {
            if (PracticeDates.Count == 0)
            {
                AttendanceDates(SelectPractice);
            }


            group_TextBlock.Text = "Группа: " + SelectPractice.Group.GroupName;
            textBlockDayAttendance.Text = SelectDate.ToShortDateString().Replace(".2023", "");

            AttendanceRows = new List<AttendanceViewDaniil>();//OUT

            List<StudentView> students = SelectPractice.Group.StudentsView.ToList();

            CurrentAttendance = AttendanceAll.Where(x => Convert.ToDateTime(x.Date) == SelectDate).ToList();

            foreach (var student in students)
            {
                var currentAttendanceStudent = CurrentAttendance
                .FirstOrDefault(x => x.StudentView.StudentID == student.StudentID);

                AttendanceRows.Add(new AttendanceViewDaniil
                {
                    StudentID = student.StudentID,
                    AttendanceID = currentAttendanceStudent.AttendanceID,
                    StudentName = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                    Photo = currentAttendanceStudent.Photo,
                    IsPresence = currentAttendanceStudent.IsPresent

                });

            }

            dataGridAttendance.ItemsSource = AttendanceRows;
        }

        private void AttendanceDates(PracticeScheduleView practice)
        {
            PracticeDates = new List<DateTime>();

            for (DateTime i = Convert.ToDateTime(practice.StartDate); i <= Convert.ToDateTime(practice.EndDate); i = i.AddDays(1))
            {
                if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday)
                {
                    continue;
                }

                PracticeDates.Add(i);
            }

            SelectDate = PracticeDates[0];
            date_TextBlock.Text = $"c {PracticeDates[0].ToShortDateString().Replace(".2023", "")} по {PracticeDates[PracticeDates.Count - 1].ToShortDateString().Replace(".2023", "")}";
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selectStudentAttendance = (AttendanceViewDaniil)dataGridAttendance.SelectedItem;


            if (selectStudentAttendance is null)
            {
                return;
            }

            var selectAttendance = AttendanceAll.FirstOrDefault(x => x.AttendanceID == selectStudentAttendance.AttendanceID);

            if (selectAttendance is null)
            {
                return;
            }

            if (UpdateAttendance.Any(n => n.AttendanceID == selectStudentAttendance.AttendanceID))
            {
                var updateStudentAttendance = UpdateAttendance.FirstOrDefault(x => x.AttendanceID == selectStudentAttendance.AttendanceID);

                if (updateStudentAttendance.IsPresence)
                {
                    updateStudentAttendance.IsPresence = false;
                    selectAttendance.IsPresent = false;

                    var obj = AttendanceAll;

                    return;
                }
                else
                {
                    updateStudentAttendance.IsPresence = true;
                    selectAttendance.IsPresent = true;

                    var obj = AttendanceAll;

                    return;
                }
            }
            else
            {
                var updateStudentAttendance = new UpdateAttendanceView
                {
                    AttendanceID = selectStudentAttendance.AttendanceID,
                    Date = SelectDate.ToShortDateString(),
                    PracticeID = SelectPractice.PracticeScheduleID,
                    StudentID = selectStudentAttendance.StudentID,
                    IsPresence = selectStudentAttendance.IsPresence
                };

                selectAttendance.IsPresent = selectStudentAttendance.IsPresence;

                var obj = AttendanceAll;

                UpdateAttendance.Add(updateStudentAttendance);
                return;
            }
        }

        //Назад дата
        private void buttonBackDay_Click(object sender, RoutedEventArgs e)
        {
                DateTime startDate = PracticeDates[0];

                DateTime endDate = PracticeDates[PracticeDates.Count - 1];

            if (SelectDate <= endDate && SelectDate > startDate)
            {

                while (SelectDate.DayOfWeek == DayOfWeek.Sunday || SelectDate.DayOfWeek == DayOfWeek.Monday)
                {
                    SelectDate = SelectDate.AddDays(-1);

                }

                SelectDate = SelectDate.AddDays(-1);
                AttendanceData();
                return;
            }
        }

        //Вперед дата
        private void buttonNextDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = PracticeDates[0];

            DateTime endDate = PracticeDates[PracticeDates.Count - 1];

            if (SelectDate < endDate && SelectDate >= startDate)
            {

                while (SelectDate.DayOfWeek == DayOfWeek.Friday || SelectDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    SelectDate = SelectDate.AddDays(1);

                }

                SelectDate = SelectDate.AddDays(1);
                AttendanceData();
                return;
            }
        }

        private async void buttonSaveAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateAttendance.Count > 0)
            {
                var response = await UpdateRequests.UpdateAttendanceAsync(UpdateAttendance);

                if (response)
                {
                    MessageBox.Show("Изменения прошли успешно");

                    var practices = await GetRequests.GetAllPracticeSchedulesAsync();

                    SelectPractice = practices.FirstOrDefault(x => x.PracticeScheduleID == SelectPractice.PracticeScheduleID);

                    AttendanceAll = SelectPractice.Attendances;
                    PracticeDates.Clear();

                    AttendanceData();

                    return;
                }

                MessageBox.Show("Изменить не удалось", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Изменений нет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    public class AttendanceViewDaniil
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Date { get; set; }
        public string Photo { get; set; }
        public bool IsPresence { get; set; }
    }

}
