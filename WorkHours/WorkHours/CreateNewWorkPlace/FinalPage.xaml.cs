using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHours.Models;
using WorkHours.HomePage;

namespace WorkHours.CreateNewWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalPage : ContentPage
    {
        private static CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;

        public String AftenTillæg { get; set; } = obj.AftenTillæg.ToString();
        public String LørdagTillæg { get; set; } = obj.LørdagsTillæg.ToString();

        public String BasisLøn { get; set; } = "Timeløn: " + obj.BasisTimeLøn;

        public String SøndagTillæg { get; set; } = obj.SøndagsTillæg.ToString();
        public String LønPeriode { get; set; }

        public FinalPage()
        {
            BindingContext = this;
            LønPeriode = "Løn perioden løber fra d. " + obj.LønPeriode_FraDato + " til og med d. " + obj.LønPeriode_TilDato + " den efterfølgende måned.";
            InitializeComponent();
        }

        private void YesBtn_Clicked(object sender, EventArgs e)
        {
            // inserting data into database.
            InsertIntoDatabase();




            // Go to homePage
            Navigation.PushAsync(new TabbedPage1());


        }

        private void InsertIntoDatabase()
        {
            var database = App.Database;
            var user = database.GetUser();
            // Tilføjer Arbejdsplads.
            database.AddCompany(new Company
            {
                CompanyName = obj.CompanyName,
                TimeLøn = obj.BasisTimeLøn,
                User = "Kasper Jørgensen",
                LønPeriode_FraDato = obj.LønPeriode_FraDato,
                LønPeriode_TilDato = obj.LønPeriode_TilDato,

            });
            // Tilføjer første tillæg
            database.AddTillæg(new Tillæg
            {
                CompanyName = obj.CompanyName,
                TypeOfTillæg = obj.AftenTillæg.Day,
                From = obj.AftenTillæg.Time.ToString(),
                TillægKr = obj.AftenTillæg.Løn,

            });
            // Tilføjer andet tillæg
            database.AddTillæg(new Tillæg
            {
                CompanyName = obj.CompanyName,
                TypeOfTillæg = obj.LørdagsTillæg.Day,
                From = obj.LørdagsTillæg.Time.ToString(),
                TillægKr = obj.LørdagsTillæg.Løn,
            });
            // Tilføjer tredje tillæg
            database.AddTillæg(new Tillæg
            {
                CompanyName = obj.CompanyName,
                TypeOfTillæg = obj.SøndagsTillæg.Day,
                From = obj.SøndagsTillæg.Time.ToString(),
                TillægKr = obj.SøndagsTillæg.Løn,
            });

            database.Commit();


            // Sætter globale Variabler.
            var variable = GlobalVariables.Instance;
            variable.ChosenCompany = obj.CompanyName;
            variable.LønPeriode_GårFraDag = obj.LønPeriode_FraDato;
            variable.LønPeriode_GårTilDag = obj.LønPeriode_TilDato;
        }

        private void NoBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}