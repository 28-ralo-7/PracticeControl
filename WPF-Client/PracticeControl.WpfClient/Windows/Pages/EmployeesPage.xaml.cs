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
        
        //Обновление страницы
        private async void EmployeesData()
        {
            employees = await GetRequests.GetAllEmployeesAsync();

            if (employees is null)
            {
                MessageBox.Show("Список сотрудников пуст", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        //Добавить сотрудника
        private void addPracticeLead_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EmployeeModalWindow employeeModalWindow =  new EmployeeModalWindow(employees);
            employeeModalWindow.ShowDialog();
            EmployeesData();
        }
        
        //Изменить сотрудника
        private void edit_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var employee = dataGridEmployees.SelectedItem as EmployeeForm;
            if (employee is not null)
            {
                EmployeeModalWindow employeeModalWindow = new EmployeeModalWindow(employees, employee);
                employeeModalWindow.ShowDialog();
                EmployeesData();
            }
        }

        //Удалить сотрудника
        private async void delete_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var employee = dataGridEmployees.SelectedItem as EmployeeForm;
            if (employee is null)
            {
                MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (employee.Login == User.Login)
            {
                MessageBoxResult resultMyself = MessageBox.Show("Вы уверены, что хотите удалить себя же?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultMyself == MessageBoxResult.Yes)
                {
                    var response = await DeleteRequests.DeleteEmployeeAsync(employee.Login);

                    if ((bool)response)
                    {
                        MessageBox.Show("Сотрудник удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var response = await DeleteRequests.DeleteEmployeeAsync(employee.Login); 

                if ((bool)response)
                {
                    MessageBox.Show("Сотрудник удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    EmployeesData();
                    return;
                }
                MessageBox.Show("Не удалось удалить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        //Обновление при загрузке
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
