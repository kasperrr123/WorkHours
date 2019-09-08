using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpretNyLønPeriode : ContentPage
    {
        DateTime current = System.DateTime.Now;
        public Company company;
        public WorkHoursDatabaseController database { get; set; }
        public String FromDate { get; set; }
        public String ToDate { get; set; }
        public OpretNyLønPeriode(string company)
        {
            BindingContext = this;
            this.database = App.Database;
            this.company = database.GetCompany(company);
            TilføjLønPeriode();
            InitializeComponent();
        }

        private void TilføjLønPeriode()
        {
            LønPeriode lønPeriode;
            // Hvis løn perioden starter på dagen eller først er om et par dage. 
            // Opret en løn periode bagud.
            if (company.LønPeriode_FraDato >= current.Day)
            {
                lønPeriode = new LønPeriode
                {
                    From = new DateTime(current.Year, current.Month - 1, company.LønPeriode_FraDato),
                    To = new DateTime(current.Year, (current.Month), company.LønPeriode_TilDato),
                    Year = current.Year,
                    CompanyName = company.CompanyName,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };

            }
            else
            {
                lønPeriode = new LønPeriode
                {
                    From = new DateTime(current.Year, current.Month, company.LønPeriode_FraDato),
                    To = new DateTime(current.Year, (current.Month + 1), company.LønPeriode_TilDato),
                    Year = current.Year,
                    CompanyName = company.CompanyName,
                    Periode = current.ToString("MMMM") + " - " + current.AddMonths(1).ToString("MMMM"),
                };
                ;
            }
            // Tilføjer lønperiode
            try
            {
                FromDate = company.LønPeriode_FraDato + "/" + (current.Month - 1) + "/" + current.Year;
                ToDate = company.LønPeriode_TilDato + "/" + current.Month + "/" + current.Year;
                App.Database.TilføjLønPeriode(lønPeriode);
                App.Database.Commit();
            }
            catch (Exception)
            {
                DisplayAlert("ERROR", "Der er sket en fejl ved oprettelse af din lønperiode, prøv venligst igen", "Ok");
            }
        }

        private void DismissButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbedPage1());
        }
    }
}