using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.CreatingNewMonth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GetWorkPlaceName : ContentPage
	{
        private NewMonthObj newMonthObj = NewMonthObj.Instance;

        public GetWorkPlaceName()
		{
			InitializeComponent ();
		}


        async void Button_Clicked(object sender, EventArgs e)
        {
            if (NameInput.Text == null || EmployeeNameInput.Text == null)
            {
               await DisplayAlert("Hov!", "Skriv venligst dit navn og navnet på din arbejdsplads", "Ok");
            }
            else
            {
                newMonthObj.CompanyName = NameInput.Text;
                newMonthObj.EmployeeName = EmployeeNameInput.Text;
                await Navigation.PushAsync(new GetMonthName());
            }
        }
    }
}