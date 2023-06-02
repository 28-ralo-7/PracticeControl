using PracticeControl.XamarinClient.API;
using PracticeControl.XamarinClient.Helpers;
using PracticeControl.XamarinClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeControl.XamarinClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainContentPage : ContentPage
    {

        private StudentViewMobile Student { get; set; }
        public MainContentPage()
        {
            InitializeComponent();
        }

        public MainContentPage(StudentViewMobile user)
        {
            Student = user;
            InitializeComponent();

            //GetDataGroup();
        }

        private async void GetDataGroup()
        {
            var practice = await APIService.GetPracticeGroupAsync(Student.Group);

            if (practice is null)
            {
                stackLayoutNotPractice.IsVisible = true;
                stackLayoutUpdateAttendance.IsVisible = false;
                return;
            }

            labelDateNow.Text = DateTime.Now.ToShortDateString();
            labelPracticeDate.Text = practice.DateStart.Replace(".2023", "") + " по " + practice.DateEnd.Replace(".2023", "");
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
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                {
                    RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
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