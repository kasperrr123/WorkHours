using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Data;
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
            if (App.Database.GetCompanies().Count > 0)
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
            if (alert == true)
            {
                try
                {
                    // Deleting json file.
                    var sqliteFileName = "GlobalVariables.txt";
                    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    var path = Path.Combine(documentsPath, sqliteFileName);
                    File.Delete(path);
                    Console.WriteLine("JSON File deleted");
                    // Deleting database
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

        private void ExportToPdfBtn_Clicked(object sender, EventArgs e)
        {
          
            var sqliteFileName = "Backup.html";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine("HellooooO");
                tw.Flush();
            }
            try
            {
             
                Device.OpenUri(new Uri(path));

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Source);
            }


        }
    }
}
