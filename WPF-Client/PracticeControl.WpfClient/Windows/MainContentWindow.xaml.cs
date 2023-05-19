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
            tbItemPractices.IsSelected = true;

            frameGroups.Content = new GroupsPage(User);
            framePractices.Content = new PracticesPage(User);

            if (User.IsAdmin)
            {
                frameEmployees.Content = new EmployeesPage();
                tbItemEmployees.Visibility = Visibility.Visible;
            }
            else
            {
                tbItemEmployees.Visibility = Visibility.Collapsed;
            }

            
        }

        public MainContentWindow()
        {
            InitializeComponent();
        }

    }
}
