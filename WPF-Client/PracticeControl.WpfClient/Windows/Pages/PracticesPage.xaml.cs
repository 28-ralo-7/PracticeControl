using DocumentFormat.OpenXml.Office2010.PowerPoint;
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

#endregion



        private async void AttendanceData() // вывод практик по дням
        {
            if (PracticeDates.Count == 0)
            {
                AttendanceDates(SelectPractice);
            }

            textBlockDayAttendance.Text = SelectDate.ToShortDateString().Replace(".2023", "");

            AttendanceRows = new List<AttendanceViewDaniil>();//OUT

            List<StudentView> students = SelectPractice.Group.StudentsView.ToList();

            CurrentAttendance = AttendanceAll.Where(x => Convert.ToDateTime(x.Date) == SelectDate).ToList();

            foreach (var student in students)
            {
                var currentAttendanceStudent = CurrentAttendance
                .FirstOrDefault(x => x.StudentView.StudentID == student.StudentID);

                if (currentAttendanceStudent is null)
                {
                    AttendanceRows.Add(new AttendanceViewDaniil
                    {
                        StudentID = student.StudentID,
                        StudentName = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                        Photo = null,
                        IsPresence = false,
                        Date = SelectDate.ToShortDateString(),
                        
                    }) ;
                }
                else
                {
                    AttendanceRows.Add(new AttendanceViewDaniil
                    {
                        StudentID = student.StudentID,
                        AttendanceID = currentAttendanceStudent.AttendanceID,
                        StudentName = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                        Photo = currentAttendanceStudent.Photo,
                        IsPresence = currentAttendanceStudent.IsPresent

                    });
                }
            }

            dataGridAttendance.ItemsSource = AttendanceRows;
        }

        private void AttendanceDates(PracticeScheduleView practice)
        {
            PracticeDates = new List<DateTime>();

            for (DateTime i = Convert.ToDateTime(practice.StartDate); i <= Convert.ToDateTime(practice.EndDate); i = i.AddDays(1))
            {
                PracticeDates.Add(i);
            }

            SelectDate = PracticeDates[0];
            date_TextBlock.Text = $"c {PracticeDates[0].ToShortDateString().Replace(".2023", "")} по {PracticeDates[PracticeDates.Count-1].ToShortDateString().Replace(".2023", "")}";
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selectStudentAttendance = (AttendanceViewDaniil)dataGridAttendance.SelectedItem;

            selectStudentAttendance.Photo = "fdfs";

            var checkBox = (CheckBox)sender;

            if (string.IsNullOrEmpty(selectStudentAttendance.Photo)) 
            {
                checkBox.IsChecked = false;
                MessageBox.Show("Для выставления присутствия необходимо фото");
                return;
            }

            if (UpdateAttendance.Any(n => n.AttendanceID == selectStudentAttendance.AttendanceID))
            {
                var updateStudentAttendance = UpdateAttendance.FirstOrDefault(x => x.AttendanceID == selectStudentAttendance.AttendanceID);

                if (updateStudentAttendance.IsPresence)
                {
                    updateStudentAttendance.IsPresence = false;
                    checkBox.IsChecked = false;
                }
                else
                {
                    updateStudentAttendance.IsPresence = true;
                    checkBox.IsChecked = true;
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

                UpdateAttendance.Add(updateStudentAttendance);
                checkBox.IsChecked = true;
            }
        }


        //КНОПКА СОХРАНИТЬ ВСЕ ИЗМЕНЕНИЯ
        //И ОТПРАВКА UpdateAttendance на сервер

        //Назад дата
        private void buttonBackDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime beginDate = PracticeDates[0];

            if (SelectDate>beginDate)
            {
                SelectDate = SelectDate.AddDays(-1);
                AttendanceData();
                return;
            }
        }
        //Вперед дата
        private void buttonNextDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime endDate = PracticeDates[PracticeDates.Count-1];

            if (SelectDate < endDate)
            {
                SelectDate = SelectDate.AddDays(1);
                AttendanceData();
                return;
            }
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



    public class UpdateAttendanceView
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int PracticeID { get; set; }
        public string Date { get; set; }
        public bool IsPresence { get; set; }
    }
}
