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
        private LønPeriode CurrentPeriode;
        private string CurrentCompany;
        public String Oprettet { get; set; }
        public TimeSpan Fra { get; set; }
        public TimeSpan Til { get; set; }
        public String Pause { get; set; }
        public RecordView Record { get; set; }

        private Record SelectedRecord;


        public ViewLogModal(RecordView record)
        {
            CurrentCompany = database.GetVariables().CurrentCompany;
            CurrentPeriode = database.GetCompany(CurrentCompany).HasCurrentPeriode();
            BindingContext = this;
            this.Record = record; 
            GetDataOnSpecificRecord();
            InitializeComponent();
        }

        private void GetDataOnSpecificRecord()
        {
            SelectedRecord = database.FåRecordByLoggedDate(Record.LoggedDate, CurrentPeriode);
            Fra = SelectedRecord.StartTime;
            Til = SelectedRecord.EndTime;
            Pause = SelectedRecord.Pause;
            Oprettet = SelectedRecord.LoggedDate.ToString();
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