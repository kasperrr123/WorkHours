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

        public List<Tillæg> ListOfTillæg { get; set; }

        public WorkHoursDatabaseController database = App.Database;

        public FinalPage(List<Tillæg> listoftillæg)
        {
            BindingContext = this;
            this.ListOfTillæg = listoftillæg;
           
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
          
            var user = database.GetUser();
            // Tilføjer Arbejdsplads.
            database.AddCompany(new Company
            {
                CompanyName = obj.CompanyName,
                TimeLøn = obj.BasisTimeLøn,
                User = "Kasper Jørgensen",
                LønPeriode_FraDato = obj.LønPeriode_FraDato,
                LønPeriode_TilDato = obj.LønPeriode_TilDato,
                Color = obj.color,
                

            });
            // Tilføjer alle tillæg
            foreach (var tillæg in ListOfTillæg)
            {
                database.AddTillæg(tillæg);
            }
            database.Commit();

            if (database.GetVariables() == null)
            {
                database.AddVariable(new Variables
                {
                    CurrentCompany = obj.CompanyName
                });
            }

        }

        private void NoBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}