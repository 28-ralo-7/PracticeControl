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
using static PracticeControl.WpfClient.Helpers.ValidationTools;

namespace PracticeControl.WpfClient.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeModalWindow.xaml
    /// </summary>
    public partial class EmployeeModalWindow : Window
    {
        private readonly List<EmployeeView>? employeeViews;
        private readonly EmployeeView? employee;

        //Окно добавления
        public EmployeeModalWindow(List<EmployeeView>? employeeViews)
        {
            InitializeComponent();
            create_Button.Visibility = Visibility.Visible;
            title_Label.Content = "Добавление пользователя";
            this.employeeViews = employeeViews;
        }
        
        //окно редактирования
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

        //Метод добавления
        private async void create_Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(lastname_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Фамилия");
                return;
            }            
            
            if (string.IsNullOrWhiteSpace(firstname_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Имя");
                return;
            }            
            
            if (string.IsNullOrWhiteSpace(login_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Логин");
                return;
            }            
            
            if (string.IsNullOrWhiteSpace(password_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Пароль");
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

            var checkUnique = await PostRequests.CheckUnique(login_TextBox.Text);

            if (checkUnique)
            {
                MessageBox.Show("Такой логин занят!");
                return;
            }

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

        //Отмена
        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Метод изменения
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

            if (string.IsNullOrWhiteSpace(lastname_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Фамилия");
                return;
            }

            if (string.IsNullOrWhiteSpace(firstname_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Имя");
                return;
            }

            if (string.IsNullOrWhiteSpace(login_TextBox.Text))
            {
                MessageBox.Show("Заполните поле: Логин");
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
        
        //Пропуск для букв
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            AllowOnlyCharacter(e);
        }

        //Очистка пробелов
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearWhiteSpace(sender);
        }
    }
}
