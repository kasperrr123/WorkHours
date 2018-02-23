using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetTillæg2 : ContentPage
    {

        SalarySettings salary = SalarySettings.Instance;


        public SetTillæg2()
        {
            InitializeComponent();
        }


        private void FærdigBtn_Clicked(object sender, EventArgs e)
        {
            // Checking if SøndagsTillægKr input is null or not. If not we insert the value into our Salary object.
            if (SøndagsTillægKr.Text != null)
            {
                
                salary.SøndagsTillæg = new SalarySettings.SøndagsTillægObj(SøndagTimePicker.Time, SøndagsTillægKr.Text, AllDaySwitch.IsToggled);

            }
            else{
                // Setting SøndagsTillæg to 0 kroner.
                salary.SøndagsTillæg = new SalarySettings.SøndagsTillægObj(SøndagTimePicker.Time, "0", AllDaySwitch.IsToggled);
            }
          

            Navigation.PushAsync(new FinalSettingsPage());
        }

        private void FlereTillægBtn_Clicked(object sender, EventArgs e)
        {
            // To be made. If the user has custom tillæg he/she wants to enter. Could be sick tillæg og morning tillæg.
            DisplayAlert("Hov!", "To be made", "Ok");
        }
    }
}