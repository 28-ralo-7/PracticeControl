using PracticeControl.XamarinClient.API;
using PracticeControl.XamarinClient.Helpers;
using PracticeControl.XamarinClient.Models;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeControl.XamarinClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainContentPage : ContentPage
    {
        //СЕРВЕР ДОДЕЛАТЬ - SHIT
        //СДЕЛАТЬ ЗАПРОС НА ПОЛУЧЕНИЕ ВСЕХ ДАННЫХ О ПРАКТИКЕ (CurrentPracticeView)
        //СДЕЛАТЬ PUT ОБНОВЛЕНИЕ ПОСЕЩЕНИЯ (StudentAttendanceView)
        private StudentViewMobile Student { get; set; }
        public MainContentPage()
        {
            InitializeComponent();
        }

        public MainContentPage(StudentViewMobile user)
        {
            Student = user;
            InitializeComponent();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                buttonUpdateAttendance.Text = "Сегодня выходной";
                buttonUpdateAttendance.IsEnabled = false;
                //return;
            }

            GetDataGroup();
        }


        private async void GetDataGroup()
        {
            var practice = await APIService.GetPracticeInfoAsync(Student.Group);

            if (practice is null)
            {
                stackLayoutNotPractice.IsVisible = true;
                stackLayoutUpdateAttendance.IsVisible = false;
                return;
            }
            labelUserName.Text = "Студент: " + Student.LastName + " " + Student.FirstName[0] + "." + Student.MiddleName[0]+".";
            labelDateNow.Text = "Сегодня: " + DateTime.Now.ToShortDateString();
            labelPracticeDate.Text = "Расписание: с " +practice.DateStart.Replace(".2023", "") + " по " + practice.DateEnd.Replace(".2023", "");
            labelPracticeName.Text = practice.PracticeName;
        }

        private async void buttonUpdateAttendance_Clicked(object sender, EventArgs e)
        {
            try
            {
                var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (cameraStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                    if (cameraStatus != PermissionStatus.Granted)
                    {
                        // Отказано в разрешении камеры
                        return;
                    }
                }
                var storageWriteStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                if (storageWriteStatus != PermissionStatus.Granted)
                {
                    storageWriteStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
                    if (storageWriteStatus != PermissionStatus.Granted)
                    {
                        // Отказано в разрешении записи
                        return;
                    }
                }

                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                if (photo is null)
                {
                    await DisplayAlert("Ошибка", "Сделайте фото", "ОК");
                    return;
                }

                var photoBytes = ImageConvert.ImageToByteArray(photo);

                if (photoBytes.Length == 0)
                {
                    await DisplayAlert("Ошибка", "Ошибка", "Ошибка");
                    return;
                }

                imageAttendance.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(photoBytes);
                });


                var updateAttendance = new StudentAttendanceView
                {
                    Student = Student,
                    DateNow = DateTime.Now,
                    Photo = photoBytes
                };

                var response = await APIService.UpdateAttendanceAsync(updateAttendance);

                if (!response)
                {
                    await DisplayAlert("Ошибка", "Не удалось сохранить", "ОК");
                    return;
                }

                await DisplayAlert("Успешно", "Вы успешно отметились", "ОК");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}