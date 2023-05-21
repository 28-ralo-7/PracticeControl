using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Model.ViewUpdate;
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

namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeModalWindow.xaml
    /// </summary>
    public partial class EmployeeModalWindow : Window
    {
        private readonly List<EmployeeView>? employeeViews;
        private readonly EmployeeView? employee;
        public EmployeeModalWindow(List<EmployeeView>? employeeViews)
        {
            InitializeComponent();
            create_Button.Visibility = Visibility.Visible;
            title_Label.Content = "Добавление пользователя";
            this.employeeViews = employeeViews;
        }
        public EmployeeModalWindow(List<EmployeeView>? employeeViews, EmployeeForm employeeForm)
        {
            InitializeComponent();

            edit_Button.Visibility = Visibility.Visible;
            title_Label.Content = "Изменение пользователя";
            this.employeeViews = employeeViews;

            employee = employeeViews.FirstOrDefault(employee => employee.Login == employeeForm.Login);

            if (employee != null)
            {
                lastname_TextBox.Text = employee.LastName;
                firstname_TextBox.Text = employee.FirstName;
                middlename_TextBox.Text = employee.MiddleName;
                login_TextBox.Text = employee.Login;
                isAdmin_CheckBox.IsChecked = employee.IsAdmin;
            }
        }

        private async void create_Button_Click(object sender, RoutedEventArgs e)
        {

            if (employeeViews.Any(employee => employee.Login == login_TextBox.Text))
            {
                MessageBox.Show("Такой сотрудник уже существует");
                return;
            }


            if (string.IsNullOrWhiteSpace(lastname_TextBox.Text) ||
                string.IsNullOrWhiteSpace(firstname_TextBox.Text) ||
                string.IsNullOrWhiteSpace(login_TextBox.Text) ||
                string.IsNullOrWhiteSpace(password_TextBox.Text)
                )
            {
                MessageBox.Show("Заполните все поля");
                return;
            }


            CreateEmployeeView employeeView = new CreateEmployeeView 
            {
                LastName = lastname_TextBox.Text,
                FirstName = firstname_TextBox.Text,
                MiddleName = middlename_TextBox.Text,
                Login = login_TextBox.Text,
                Password = password_TextBox.Text,
                IsAdmin = (bool)isAdmin_CheckBox.IsChecked
            };


            try
            {
                var response = await PostRequests.CreateEmoloyeesAsync(employeeView);

                if (response is not null)
                {
                    MessageBox.Show("Пользователь сохранен");
                    Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить пользователя");
                    return;
                }

            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }

        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var count = employeeViews
                .Where(employeeView => 
                employeeView.Login.ToLower() == login_TextBox.Text.ToLower() && 
                employeeView.Login.ToLower() != employee.Login.ToLower())
                .Count();

            if (count > 0)
            {
                MessageBox.Show("Этот логин занят");
                return;
            }

            if (string.IsNullOrWhiteSpace(lastname_TextBox.Text) ||
                string.IsNullOrWhiteSpace(firstname_TextBox.Text) ||
                string.IsNullOrWhiteSpace(login_TextBox.Text) 
                )
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            UpdateEmployeeView employeeView = new UpdateEmployeeView
            {
                LastName = lastname_TextBox.Text,
                FirstName = firstname_TextBox.Text,
                MiddleName = middlename_TextBox.Text,
                Login = login_TextBox.Text,
                Password = password_TextBox.Text,
                IsAdmin = (bool)isAdmin_CheckBox.IsChecked,
                LoginForSearch = employee.Login
            };

            try
            {
                var response = await UpdateRequests.UpdateEmployeeAsync(employeeView);

                if (response is not null)
                {
                    MessageBox.Show("Пользователь сохранен");
                    Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить пользователя");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }
    }
}
