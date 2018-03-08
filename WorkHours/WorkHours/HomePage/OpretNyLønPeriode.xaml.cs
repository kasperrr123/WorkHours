using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpretNyLønPeriode : ContentPage
    {
        GlobalVariables globalVariables = GlobalVariables.Instance;
        DateTime current = System.DateTime.Now;
        public String FromDate { get; set; }
        public String ToDate { get; set; }
        public OpretNyLønPeriode()
        {
            BindingContext = this;
            if (globalVariables.LønPeriode_GårFraDag >= current.Day)
            {
                FromDate = globalVariables.LønPeriode_GårFraDag + "/" + (current.Month - 1) + "/" + current.Year;
                ToDate = globalVariables.LønPeriode_GårTilDag + "/" + current.Month + "/" + current.Year;
            }
            else
            {
                FromDate = globalVariables.LønPeriode_GårFraDag + "/" + current.Month + "/" + current.Year;
                ToDate = globalVariables.LønPeriode_GårTilDag + "/" + (current.Month + 1) + "/" + current.Year;
            }

            LønPeriode lønPeriode;
            // Hvis løn perioden starter på dagen eller først er om et par dage. 
            // Opret en løn periode bagud.
            if (globalVariables.LønPeriode_GårFraDag >= current.Day)
            {
                lønPeriode = new LønPeriode
                {
                    From = new DateTime(current.Year, current.Month - 1, globalVariables.LønPeriode_GårFraDag),
                    To = new DateTime(current.Year, (current.Month), globalVariables.LønPeriode_GårTilDag),
                    Year = current.Year,
                    CompanyName = globalVariables.ChosenCompany,
                    WhenToGetSalary = System.DateTime.DaysInMonth(current.Year, (current.Month + 1)).ToString() + "/" + (current.Month + 1) + "/" + current.Year,
                };
            }
            else
            {
                lønPeriode = new LønPeriode
                {
                    From = new DateTime(current.Year, current.Month, globalVariables.LønPeriode_GårFraDag),
                    To = new DateTime(current.Year, (current.Month + 1), globalVariables.LønPeriode_GårTilDag),
                    Year = current.Year,
                    CompanyName = globalVariables.ChosenCompany,
                    WhenToGetSalary = System.DateTime.DaysInMonth(current.Year, (current.Month + 1)).ToString() + "/" + (current.Month + 1) + "/" + current.Year,
                };
            }
            // Tilføjer lønperiode

            try
            {

                App.Database.TilføjLønPeriode(lønPeriode);
                App.Database.Commit();
                globalVariables.ValgteLønPeriode = lønPeriode;

            }
            catch (Exception)
            {

                DisplayAlert("ERROR", "Der er sket en fejl ved oprettelse af din lønperiode, prøv venligst igen", "Ok");
            }



            InitializeComponent();
        }

        private void DismissButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbedPage1());
        }
    }
}