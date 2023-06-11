﻿using PracticeControl.WpfClient.API;
using PracticeControl.WpfClient.Model.View;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    /// Логика взаимодействия для PracticeModalWindow.xaml
    /// </summary>
    public partial class PracticeModalWindow : Window
    {
        private readonly List<PracticeView> practiceViews;
        private readonly PracticeView? practiceView;

        //Окно добавления
        public PracticeModalWindow(List<PracticeView> practiceViews)
        {
            InitializeComponent();

            create_Button.Visibility = Visibility.Visible;
            title_Label.Content = "Добавление практики";
            this.practiceViews = practiceViews;
        }

        //Окно редактирования
        public PracticeModalWindow(List<PracticeView> practiceViews, PracticeView practice)
        {
            InitializeComponent();

            edit_Button.Visibility = Visibility.Visible;
            title_Label.Content = "Изменение практики";
            this.practiceViews = practiceViews;

            practiceView = practiceViews
                .FirstOrDefault(p =>
                p.PracticeModule == practice.PracticeModule &&
                p.Abbreviation == practice.Abbreviation &&
                p.Specialty == practice.Specialty
                );

            if (practiceView is not null)
            {
                module_TextBox.Text = practiceView.Abbreviation;
                name_TextBox.Text = practiceView.PracticeModule;
                specialty_TextBox.Text = practiceView.Specialty;
            }
        }

        //Метод добавления
        private async void create_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(module_TextBox.Text) ||
                string.IsNullOrWhiteSpace(name_TextBox.Text) ||
                string.IsNullOrWhiteSpace(specialty_TextBox.Text)
                )
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            try
            {
                PracticeView practiceViewgorCreate = new PracticeView
                {
                    Abbreviation = module_TextBox.Text,
                    PracticeModule = name_TextBox.Text,
                    Specialty = specialty_TextBox.Text
                };

                var checkUnique = await PostRequests.CheckUnique(practiceViewgorCreate);

                if (checkUnique)
                {
                    MessageBox.Show("Такая практика уже существует");
                    return;
                }

                var response = await PostRequests.CreatePractice(practiceViewgorCreate);
                if (response)
                {
                    MessageBox.Show("Практика сохранена", "Уведомление", MessageBoxButton.OK);
                    this.Close();
                    return;
                }
                MessageBox.Show("Изменение не произошло", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private async void edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(module_TextBox.Text) ||
                string.IsNullOrWhiteSpace(name_TextBox.Text) ||
                string.IsNullOrWhiteSpace(specialty_TextBox.Text)
                )
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            PracticeView practiceViewforUpdate = new PracticeView
            {
                Id = practiceView.Id,
                Abbreviation = module_TextBox.Text,
                PracticeModule = name_TextBox.Text,
                Specialty = specialty_TextBox.Text
            };
            try
            {
                var checkUnique = await PostRequests.CheckUnique(practiceViewforUpdate);

                if (checkUnique)
                {
                    MessageBox.Show("Такая практика уже существует");
                    return;
                }

                var response = await UpdateRequests.UpdatePracticeAsync(practiceViewforUpdate);
                if (response)
                {
                    MessageBox.Show("Практика изменена", "Уведомление", MessageBoxButton.OK);
                    this.Close();
                    return;
                }
                MessageBox.Show("Изменение не произошло", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch
            {
                MessageBox.Show("Ошибка изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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