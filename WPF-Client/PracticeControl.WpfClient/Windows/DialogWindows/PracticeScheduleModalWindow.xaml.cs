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
        public PracticeScheduleModalWindow()
        {
            InitializeComponent();

            var listPractice = new List<PracticeOut>
            {
                new PracticeOut
                {
                    PracticeID = 1,
                    Description = "УП.01 | Практика"
                },

                 new PracticeOut
                {
                    PracticeID = 2,
                    Description = "УП.01 | Практика"
                },
            };


            combo.ItemsSource = null;
            combo.ItemsSource = listPractice;
            combo.DisplayMemberPath = "Description";
            combo.SelectedValuePath = "Description";

        }



        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPractice = combo.SelectedItem as PracticeOut;
        }
    }

    class PracticeOut
    {
        public int PracticeID { get; set; }
        public string Description { get; set; }
    }

    class CreatePracticeView
    {
        public int PracticeID { get; set; }
        public int GroupID { get; set; }
        public int EmployeeId{ get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate{ get; set; }

    }
}
