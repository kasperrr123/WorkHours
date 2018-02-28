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


    public partial class LønPeriode : ContentPage
    {

        private CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;


        public List<String> Days { get; set; } = new List<String> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32" };


        public LønPeriode()
        {
            BindingContext = this;
            InitializeComponent();
        }

        

        private void GåTilTillægBtn_Clicked(object sender, EventArgs e)
        {
            if (TimeFrom.SelectedItem != null && TimeTo.SelectedItem != null)
            {
                obj.LønPeriode_FraDato = TimeFrom.SelectedItem.ToString();
                obj.LønPeriode_TilDato = TimeTo.SelectedItem.ToString();
                Navigation.PushAsync(new SetTillæg1());

            }
            else
            {
                DisplayAlert("Hov!", "Du skal vælge en fra dato og en til dato", "Ok");
            }
        }
    }
}