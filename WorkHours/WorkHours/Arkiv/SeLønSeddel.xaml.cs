using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Arkiv;
using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeLønSeddel : ContentPage
    {

        public string arbejdsplads { get; set; }
        public string periode { get; set; }
        public string antalVagter { get; set; }
        public string antalTimer { get; set; }
        public string pauseIMinutter { get; set; }

       private Calculations cal = new Calculations();
        public LønPeriode LønPeriodeObj { get; set; }

        private WorkHoursDatabaseController database = App.Database;

        public SeLønSeddel(LønPeriode lønPeriode)
        {
            BindingContext = this;
            arbejdsplads = "Arbejdsplads: " + lønPeriode.CompanyName;
            periode = "Periode: " + lønPeriode.Periode;
            antalVagter = "Antal vagter: " + database.FåRecordsByPeriode(lønPeriode).Count.ToString();
            var hours = cal.GetTotalHours(lønPeriode.CompanyName, lønPeriode);
            var minutes = cal.GetTotalBreak(lønPeriode.CompanyName, lønPeriode);
            antalTimer = "Antal timer: " + hours[0].ToString() + "t " + hours[1].ToString() + "m";
            pauseIMinutter = "Pause i minutter: " + minutes[0].ToString() + "t " + minutes[1].ToString() + "m";
            InitializeComponent();
        }

        private void TilbageBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}