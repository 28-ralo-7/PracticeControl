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
    /// Логика взаимодействия для AttendancePhotoWindow.xaml
    /// </summary>
    public partial class AttendancePhotoWindow : Window
    {
        public AttendancePhotoWindow(Image imageAttendance)
        {
            InitializeComponent();

            imageOut.Source = imageAttendance.Source;
        }

        private void exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
