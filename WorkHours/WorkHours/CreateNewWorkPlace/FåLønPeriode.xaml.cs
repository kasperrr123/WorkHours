using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.CreateNewWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class FåLønPeriode : ContentPage
    {

        CreateNewWorkPlaceObj createNewWorkPlace = CreateNewWorkPlaceObj.Instance;


        public List<String> Days { get; set; }


        public FåLønPeriode()
        {
            BindingContext = this;
            Days = GetMonthDays();
            InitializeComponent();
        }

        private List<string> GetMonthDays()
        {
            var counter = 1;
            List<String> list = new List<string>();
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            while (counter <= daysInMonth)
            {
                list.Add(counter.ToString());
                counter++;
            }

            return list;
        }

        private void GåTilTillægBtn_Clicked(object sender, EventArgs e)
        {
            if (TimeFrom.SelectedItem != null && TimeTo.SelectedItem != null)
            {

                createNewWorkPlace.LønPeriode_FraDato = int.Parse(TimeFrom.SelectedItem.ToString());
                createNewWorkPlace.LønPeriode_TilDato = int.Parse(TimeTo.SelectedItem.ToString());
                // Sætter den valgte periode ind til global variabler.

                Navigation.PushAsync(new SetTillæg());




            }
            else
            {
                DisplayAlert("Hov!", "Du skal vælge en fra dato og en til dato", "Ok");
            }
        }
    }
}