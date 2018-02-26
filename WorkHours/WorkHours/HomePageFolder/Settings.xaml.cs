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

namespace WorkHours.HomePageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {

        public List<String> ArbejdsPladser { get; set; }

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
            if (App.Database.GetCompanies() != null)
            {

                foreach (var item in App.Database.GetCompanies())
                {
                    listOfCompanies.Add(item.CompanyName);
                }

                return listOfCompanies;


            }
            return new List<string> { "Can't find any companies" };
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

        private void ResetAppBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.Database.DeleteDatabase();
                DisplayAlert("Succes", "Database has been deleted", "Return to home");
                Navigation.PushAsync(new FirstTimeUse());
            }
            catch (Exception x)
            {
                DisplayAlert("Error", "Error with deleting database: " + x.Message, "ok");

            }

        }
    }
}
