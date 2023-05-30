using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Windows.Pages;
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

namespace PracticeControl.WpfClient.Windows
{
    public partial class MainContentWindow : Window
    {
        private EmployeeView User { get; set; }

        public MainContentWindow(EmployeeView user)
        {
            this.User = user;

            InitializeComponent();

            tbItemPracticeSchedules.IsSelected = true;

            frameGroups.Content = new GroupsPage(User);
            framePracticeSchedules.Content = new PracticesPage(User);

            if (User.IsAdmin)
            {
                frameEmployees.Content = new EmployeesPage(User);
                tbItemEmployees.Visibility = Visibility.Visible;

                frameStudents.Content = new StudentPage();
                tbItemStudents.Visibility = Visibility.Visible;                
                
                framePractices.Content = new PracticePage();
                tbItemPractices.Visibility = Visibility.Visible;
            }
            else
            {
                tbItemEmployees.Visibility = Visibility.Collapsed;
                tbItemStudents.Visibility = Visibility.Collapsed;
                tbItemPractices.Visibility = Visibility.Collapsed;
            }

            
        }

        public MainContentWindow()
        {
            InitializeComponent();
        }

        private void exit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show();
                this.Close();
            }
        }
    }
}
