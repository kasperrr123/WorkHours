using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.CreateNewWorkPlace
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GetWorkPlaceName : ContentPage
	{
        private CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;

        public GetWorkPlaceName()
		{
			InitializeComponent ();
		}


        async void Button_Clicked(object sender, EventArgs e)
        {
            if (NameInput.Text == null)
            {
               await DisplayAlert("Hov!", "Skriv venligst navnet på din arbejdsplads", "Ok");
            }
            else
            {
                obj.CompanyName = NameInput.Text;
                await Navigation.PushAsync(new SetTimeLøn());
            }
        }
    }
}