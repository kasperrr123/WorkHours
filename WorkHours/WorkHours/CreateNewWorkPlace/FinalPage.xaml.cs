using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHours.Models;
using WorkHours.HomePageFolder;

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

        public FinalPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void YesBtn_Clicked(object sender, EventArgs e)
        {

            var database = App.Database;
            var user = database.GetUser();
            // Inserting new workplace for the user.
            Company company = new Company
            {
                CompanyName = obj.CompanyName,
                FullName = user.FullName,
            };
            database.AddCompany(company);
            // Inserting salary for the company.
            //var tillæg = new Tillæg
            //{


            //};
            //database.GetUser().AddTillæg(tillæg);


            Navigation.PushAsync(new HomePage());


        }

        private void NoBtn_Clicked(object sender, EventArgs e)
        {
            List<String> data = new List<string>();
            foreach (var item in App.Database.GetUsers())
            {
                DisplayAlert("YES", item.FullName + " " + item.Companies, "DAMN");
            }
        }
    }
}