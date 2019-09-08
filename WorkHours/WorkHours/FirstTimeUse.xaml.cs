using System;
using WorkHours.HomePage;
using WorkHours.Models;
using Xamarin.Forms;
using WorkHours.Arkiv;

namespace WorkHours
{
    public partial class FirstTimeUse : ContentPage
    {

        public string LabelText { get; set; }


        public FirstTimeUse()
        {
            this.LabelText = "Velkommen til appen!" + "\n" + "Da det er første gang du bruger appen, skal vi have lavet en database til dig. Det gør du" +
                "ved at skrive dit dit navn nede i feltet og trykker på knappen opret.";
            BindingContext = this;
            InitializeComponent();
        }

        private void OpretDatabaseBtn_Clicked(object sender, EventArgs e)
        {
            var database = App.Database;
            if (FuldeNavn.Text != null)
            {
                var newUser = new User
                {
                    FullName = FuldeNavn.Text,
                };
                try
                {
                    database.AddUser(newUser);
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "ok");
                }

                //TestData testData = new TestData(newUser);

                database.AddVariable(new Variables
                {
                    ID = 1,
                    CurrentCompany = "",
                });

                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Hov!", "Du skal angive dit navn i feltet", "Ok");
            }

        }
    }
}
