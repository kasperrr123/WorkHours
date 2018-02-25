using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.CreatingNewMonth;
using WorkHours.HomePageFolder;
using WorkHours.Models;
using Xamarin.Forms;

namespace WorkHours
{
    public partial class FirstTimeUse : ContentPage
    {

        public string LabelText { get; set; }


        public FirstTimeUse()
        {
            this.LabelText = "Velkommen til appen!" + "\n" + "Da det er første gang du skal bruge appen skal vi have lavet en database til dig. Det gør vi" +
                "ved at du skrivet dit fulde navn nede i feltet neden under og trykker på knappen opret.";
            BindingContext = this;
            InitializeComponent();
        }

        private void OpretDatabaseBtn_Clicked(object sender, EventArgs e)
        {
            var database = App.UserDatabase;
            if (FuldeNavn.Text != null)
            {
                var newUser = new User
                {
                    FullName = FuldeNavn.Text,
                };
                database.AddUser(newUser);
                
                Navigation.PushAsync(new TabbedPage1());
            }
            else
            {
                DisplayAlert("Hov!", "Du skal angive dit navn i feltet", "Ok");
            }

        }
    }
}
