using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Models;
using WorkHours.UpdateWorkPlace;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {

        public List<String> ArbejdsPladser { get; set; }

        public String CantFindAnyCompany { get; set; }

        public Settings()
        {
            BindingContext = this;
            ArbejdsPladser = new List<String>();
            ArbejdsPladser = GetCompanies();
            InitializeComponent();
                 

        }

        private List<String> GetCompanies()
        {

            List<String> listOfCompanies = new List<String>();
            if (App.Database.GetCompanies().Count>0)
            {

                foreach (var item in App.Database.GetCompanies())
                {
                    listOfCompanies.Add(item.CompanyName);
                }

                return listOfCompanies;


            }
            return new List<string> { "Ingen arbejdspladser er oprettet endnu" };
        }

        private void OpretNyArbejdspladsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetWorkPlaceName());
        }

        private void ÆndreArbejdspladsBtn_Clicked(object sender, EventArgs e)
        {
            if (WorkPlacePicker.SelectedItem != null)
            {
                Navigation.PushAsync(new UpdateWorkPlaceNameAndTimeLøn(WorkPlacePicker.SelectedItem.ToString()));
            }
        }

        private async Task ResetAppBtn_ClickedAsync(object sender, EventArgs e)
        {
          var alert = await DisplayAlert("Vigtigt!", "Hvis du trykker ok vil din app blive nulstillet og du mister ALT dit data", "Ok", "Gå tilbage");
            if (alert==true)
            {
                try
                {
                    App.Database.DeleteDatabase();
                    await DisplayAlert("Succes", "Database has been deleted", "Return to home");
                    await Navigation.PushAsync(new FirstTimeUse());
                }
                catch (Exception x)
                {
                    await DisplayAlert("Error", "Error with deleting database: " + x.Message, "ok");

                }
            }
           

        }
    }
}
