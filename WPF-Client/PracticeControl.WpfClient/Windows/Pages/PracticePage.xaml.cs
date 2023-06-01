using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Windows.DialogWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeControl.WpfClient.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для PracticePage.xaml
    /// </summary>
    public partial class PracticePage : Page
    {
        private List<PracticeView> PracticeViews { get; set; }
        public PracticePage()
        {
            InitializeComponent();
            PracticeData();
        }

        private async void PracticeData()
        {
            PracticeViews = await GetRequests.GetAllPracticeAsync();

            if (PracticeViews is null)
            {
                MessageBox.Show("Список практик пуст", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            practice_DataGrid.ItemsSource = null;
            practice_DataGrid.ItemsSource = PracticeViews;
        }

        private void addPractice_Button_Click(object sender, RoutedEventArgs e)
        {
            PracticeModalWindow modalWindow = new PracticeModalWindow(PracticeViews);
            modalWindow.ShowDialog();
            PracticeData();
        }

        private void editPractice_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedPractice = practice_DataGrid.SelectedItem as PracticeView;
            if (selectedPractice != null)
            {
                PracticeModalWindow modalWindow = new PracticeModalWindow(PracticeViews, selectedPractice);
                modalWindow.ShowDialog();
                PracticeData();
            }
        }

        private void deletePractice_Button_Click(object sender, RoutedEventArgs e)
        {
            PracticeData();
        }
    }
}
