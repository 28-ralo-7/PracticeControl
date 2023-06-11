using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Android;

namespace PracticeControl.XamarinClient.Droid
{
    [Activity(Label = "Контроль практики", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ManageExternalStorage) == Permission.Granted)
            {
                //Разрешение уже получено
            }
            else
            {
                // Запрос разрешения на управление внешним хранилищем
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ManageExternalStorage }, 200);
            }
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        
            
        }
        public override void OnBackPressed()
        {
            // Здесь можно добавить код для запрета кнопки "назад"
            // Например, показать пользователю сообщение об ошибке или перенаправить на другую страницу

            // Если нужно просто запретить использование кнопки "назад", можно оставить метод пустым
        }
    }
}