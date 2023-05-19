using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticeControl.WpfClient.Windows.Pages
{

    public partial class PracticesPage : Page
    {
        private EmployeeView User { get; set; }

        public PracticesPage(EmployeeView User)
        {
            this.User = User;

            InitializeComponent();

            PracticesData();
        }


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

        private void AttendanceData(PracticeScheduleView practiceSchedule)
        {

            //for (DateTime i = Convert.ToDateTime(practiceSchedule.StartDate); i <= Convert.ToDateTime(practiceSchedule.EndDate); i.AddDays(1))
            //{
            //    DataGridTextColumn datagridColumn = new DataGridTextColumn();
            //    datagridColumn.Header = i.ToShortDateString();
            //    datagridColumn.HeaderStyle = (Style)FindResource("columnDataGrid");

               

            //    datagridColumn.Binding = ;
            //    dataGridAttendance.Columns.Add(datagridColumn);
            //}
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
    }
}
