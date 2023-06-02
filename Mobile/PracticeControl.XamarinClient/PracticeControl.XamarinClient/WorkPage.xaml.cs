using PracticeControl.XamarinClient.API;
using PracticeControl.XamarinClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeControl.XamarinClient
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkPage : ContentPage
	{
        private AuthResponseMobile User { get; set; }
        public WorkPage (AuthResponseMobile User)
		{
            this.User = User;

			InitializeComponent();
		}



        private async void GetDataGroup()
        {
            var practice = await APIService.GetPracticeGroupAsync(User.user.Group);

            if (practice is null)
            {
                stackLayoutNotPractice.IsVisible = true;
            }
        }

        private void buttonUpdateAttendance_Clicked(object sender, EventArgs e)
        {

        }
    }
}