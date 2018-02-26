using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.HomePageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpretNy : ContentPage
    {

        public List<String> ArbejdsPladser { get; set; }

        public OpretNy()
        {
            ArbejdsPladser = new List<String>();
            ArbejdsPladser = GetCompanies();
            BindingContext = this;
            InitializeComponent();
        }

        private List<String> GetCompanies()
        {

            List<String> listOfCompanies = new List<String>();
            if (App.Database.GetUser().Companies != null)
            {
                {
                    foreach (var item in App.Database.GetUser().Companies)
                    {
                        listOfCompanies.Add(item.CompanyName);
                    }

                    return listOfCompanies;
                }
        
            }
            return null;
        }

        private void OpretNyArbejdspladsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetWorkPlaceName());
        }
    }
}
