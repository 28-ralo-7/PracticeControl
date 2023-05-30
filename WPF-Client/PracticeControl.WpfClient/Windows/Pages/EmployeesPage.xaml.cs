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
        private EmployeeView User { get; set; }

        public EmployeesPage(EmployeeView User)
        {
            InitializeComponent();
            this.User = User;
            EmployeesData();
        }

        private async void EmployeesData()
        {
            employees = await GetRequests.GetAllEmployeesAsync();

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

            if (employee.Login == User.Login)
            {
                MessageBoxResult resultMyself = MessageBox.Show("Вы уверены, что хотите удалить себя же?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultMyself == MessageBoxResult.Yes)
                {
                    var response = await DeleteRequests.DeleteEmployeeAsync(employee.Login);

                    if ((bool)response)
                    {
                        MessageBox.Show("Сотрудник удален");
                        EmployeesData();
                        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                        authorizationWindow.Show();
                        Window window = Window.GetWindow(this);
                        if (window != null)
                        {
                            window.Close();
                        }
                        return;
                    }
                    MessageBox.Show("Не удалось удалить");

                }
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var response = await DeleteRequests.DeleteEmployeeAsync(employee.Login); 

                if ((bool)response)
                {
                    MessageBox.Show("Сотрудник удален");
                    EmployeesData();
                    return;
                }
                MessageBox.Show("Не удалось удалить");

            }
        }

        private void Employees_Page_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeesData();

        }

        
    }

    public class EmployeeForm
    {
        public string EmployeeName { get; set; }
        public string Login { get; set; }
        public string IsAdmin { get; set; }

    }
}
