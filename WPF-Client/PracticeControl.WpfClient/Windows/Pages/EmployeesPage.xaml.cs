using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Windows.DialogWindows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PracticeControl.WpfClient.Windows.Pages
{
    public partial class EmployeesPage : Page
    {
        private List<EmployeeView>? employees;
        public EmployeesPage()
        {
            InitializeComponent();

            EmployeesData();
        }

        private async void EmployeesData()
        {
            employees = await Requests.GetAllEmployeesAsync();

            if (employees is null)
            {
                return;
            }

            var employee = new List<EmployeeForm>();

            foreach (var item in employees)
            {
                employee.Add(new EmployeeForm
                {
                    EmployeeName = item.LastName + " " + item.FirstName + " " + item.MiddleName,
                    Login = item.Login,
                    IsAdmin = item.IsAdmin ? "Администратор" : "Руководитель" 
                });
            }

            dataGridEmployees.ItemsSource = null;
            dataGridEmployees.ItemsSource = employee;

        }

        private void addPracticeLead_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EmployeeModalWindow employeeModalWindow =  new EmployeeModalWindow(employees);
            employeeModalWindow.ShowDialog();
            EmployeesData();
        }

        private void edit_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var employee = dataGridEmployees.SelectedItem as EmployeeForm;
            EmployeeModalWindow employeeModalWindow = new EmployeeModalWindow(employees, employee);
            employeeModalWindow.ShowDialog();
            EmployeesData();

        }

        private async void delete_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var employee = dataGridEmployees.SelectedItem as EmployeeForm;
            if (employee is null)
            {
                MessageBox.Show("Не удалось удалить");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var response = await Requests.DeleteEmployeeAsync(employee.Login); 

                if (response is not null)
                {
                    MessageBox.Show("Сотрудник удален");
                    EmployeesData();
                    return;
                }
                MessageBox.Show("Не удалось удалить");

            }
        }
    }

    public class EmployeeForm
    {
        public string EmployeeName { get; set; }
        public string Login { get; set; }
        public string IsAdmin { get; set; }

    }
}
