using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Windows.DialogWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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

        //Обновление практик
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

        //Добавление практики
        private void addPractice_Button_Click(object sender, RoutedEventArgs e)
        {
            PracticeModalWindow modalWindow = new PracticeModalWindow(PracticeViews);
            modalWindow.ShowDialog();
            PracticeData();
        }

        //Изменение практик
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

        //Удаление практики
        private async void deletePractice_Button_Click(object sender, RoutedEventArgs e)
        {
            var practice = practice_DataGrid.SelectedItem as PracticeView;

            if (practice is null)
                return;

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;
            try
            {
                var isDeleted = await DeleteRequests.DeletePracticeAsync(practice.Id);

                if (isDeleted)
                {
                    MessageBox.Show("Практика удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    PracticeData();
                    return;
                }

                MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                PracticeData();
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
