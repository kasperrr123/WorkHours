using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.Arkiv
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewLogModal : ContentPage
    {
        // Database instance
        private WorkHoursDatabaseController database = App.Database;
        public String Oprettet { get; set; }
        public TimeSpan Fra { get; set; }
        public TimeSpan Til { get; set; }
        public String Pause { get; set; }
        public ListView SelectedListView { get; set; }
        private GlobalVariables global = GlobalVariables.Instance;

        private Record SelectedRecord;


        public ViewLogModal(ListView listview)
        {
            BindingContext = this;
            this.SelectedListView = listview;
            GetDataOnSpecificRecord();
            InitializeComponent();
        }

        private void GetDataOnSpecificRecord()
        {

            Oprettet = SelectedListView.SelectedItem.ToString();


            DateTime a = DateTime.Parse(Oprettet);
            Console.WriteLine(global.ValgteLønPeriode.From);
            Console.WriteLine(global.ValgteLønPeriode.To);
            Console.WriteLine(global.ValgteLønPeriode.CompanyName);

            // WTF is going on here!!! Should be so fucking simple. jesus fuck.
            SelectedRecord = database.FåRecordByLoggedDate(a, global.ValgteLønPeriode);
            Fra = SelectedRecord.StartTime;
            Til = SelectedRecord.EndTime;
            Pause = SelectedRecord.Pause;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        async private void ÆndreBtn_Clicked(object sender, EventArgs e)
        {
            if (FraPicker.Time != Fra || TilPicker.Time != Til || PauseField.Text != Pause)
            {
                SelectedRecord.StartTime = FraPicker.Time;
                SelectedRecord.EndTime = TilPicker.Time;
                SelectedRecord.Pause = PauseField.Text;
                try
                {
                    App.Database.ÆndreRecord(SelectedRecord);
                    await DisplayAlert("Success", "Din vagt er blevet ændret", "Ok");
                    OnDismissButtonClicked(sender, e);

                }
                catch (Exception)
                {
                    await DisplayAlert("Fejl", "Der var en fejl under ændring af din vagt. Prøv venligst igen", "Ok");

                }
            }
            else
            {
                await DisplayAlert("Hov!", "Ingen ændringer fundet", "Ok");

            }
        }

        async private void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.Database.SletRecord(SelectedRecord);
                await DisplayAlert("Success", "Din vagt er blevet slettet", "Ok");
                OnDismissButtonClicked(sender, e);

            }
            catch (Exception)
            {
                await DisplayAlert("Fejl", "Der var en fejl under sletningen af din vagt. Prøv venligst igen", "Ok");

            }
        }
    }
}