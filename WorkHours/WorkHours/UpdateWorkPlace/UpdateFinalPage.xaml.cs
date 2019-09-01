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

namespace WorkHours.UpdateWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateFinalPage : ContentPage
    {
        private static CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;

        public String AftenTillæg { get; set; } = obj.AftenTillæg.ToString();
        public String LørdagTillæg { get; set; } = obj.LørdagsTillæg.ToString();

        public String BasisLøn { get; set; } = "Timeløn: " + obj.BasisTimeLøn;

        public String SøndagTillæg { get; set; } = obj.SøndagsTillæg.ToString();

        public UpdateFinalPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void YesBtn_Clicked(object sender, EventArgs e)
        {

            var database = App.Database;
            var user = database.GetUser();
            // Inserting new workplace for the user.
            database.AddCompany(new Company
            {
                CompanyName = obj.CompanyName,
            });
            database.AddTillæg(new Tillæg
            {
                TypeOfTillæg = obj.AftenTillæg.Day,
                From = obj.AftenTillæg.Time,
                TillægKr = obj.AftenTillæg.Løn,
            });


            Navigation.PushAsync(new Home());


        }

        private void NoBtn_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}