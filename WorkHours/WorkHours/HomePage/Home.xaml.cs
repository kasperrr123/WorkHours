using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private String todaysDate;
        public String TodaysDate
        {
            get { return todaysDate; }
            set
            {
                todaysDate = value;
                INotifyPropertyChanged();
            }
        }
        private String whatCompanyLabel;
        public String WhatCompanyLabel
        {
            get { return whatCompanyLabel; }
            set
            {
                whatCompanyLabel = "Arbejdsplads: " + value;
                INotifyPropertyChanged();
            }
        }
        public List<String> Companies { get; set; }
        public String WelcomeUserLabel { get; set; }
        public bool IngenArbejdsPladsOprettet { get; set; }
        private bool lønPeriodeForNuværendeMånedFundet;

        public bool LønPeriodeForNuværendeMånedFundet
        {
            get { return lønPeriodeForNuværendeMånedFundet; }
            set
            {
                lønPeriodeForNuværendeMånedFundet = value;
                INotifyPropertyChanged();
            }
        }

        private String lønPeriodeLabel;
        public String LønPeriodeLabel
        {
            get { return lønPeriodeLabel; }
            set
            {
                lønPeriodeLabel = "Løn periode: " + value;
                INotifyPropertyChanged();
            }
        }
        public bool IngenLønPeriodeForNuværendeMånedFundet { get; set; }
        // Database instance
        private WorkHoursDatabaseController database = App.Database;
        // Global Variable instance
        private GlobalVariables globalVariables = GlobalVariables.Instance;
        private TimeSpan currentTimeSpan = new TimeSpan();

        // CONSTRUCTOR
        public Home()
        {
            BindingContext = this;
            ThreadStart timer = new ThreadStart(TimerFunction);
            Thread myThread = new Thread(timer);
            myThread.Start();
            WelcomeUserLabel = GetUser();
            HvilketPanelSkalVises();
            SetChooseWorkPlacePickerValues();
            String h = DateTime.Now.Hour.ToString();
            String m = DateTime.Now.Minute.ToString();
            currentTimeSpan = TimeSpan.Parse(h + ":" + m);


            InitializeComponent();
        }

        private void SetChooseWorkPlacePickerValues()
        {

            List<String> ListOfCompanyNames = new List<string>();
            foreach (var item in App.Database.GetCompanies())
            {
                ListOfCompanyNames.Add(item.CompanyName);
            }
            Companies = ListOfCompanyNames;
        }

        private void HvilketPanelSkalVises()
        {

            // Tjek om der er blevet oprettet en arbejdsplads.
            if (FindesDerArbejdsplads())
            {
                // Sætter label til arbejdsplads.
                WhatCompanyLabel = globalVariables.ChosenCompany;
                // Tjek om der er oprettet en løn periode.
                if (FindesDerEnAktivLønPeriodeForArbejdsplads())
                {
                    LønPeriodeLabel = "Fra d. " + globalVariables.ValgteLønPeriode.From.ToString("dd/MM/yyyy") + " til d. " + globalVariables.ValgteLønPeriode.To.ToString("dd/MM/yyyy");
                    IngenLønPeriodeForNuværendeMånedFundet = false;
                    LønPeriodeForNuværendeMånedFundet = true;

                }
                else
                {
                    LønPeriodeForNuværendeMånedFundet = false;
                    IngenLønPeriodeForNuværendeMånedFundet = true;
                }
            }
            else
            {
                IngenArbejdsPladsOprettet = true;
            }
        }

        private bool FindesDerEnAktivLønPeriodeForArbejdsplads()
        {

            if (database.GetLønPerioder().Count > 0)
            {
                foreach (var item in database.GetLønPerioder().Where(n => n.Year == System.DateTime.Now.Year).Where(n => n.CompanyName == globalVariables.ChosenCompany))
                {
                    DateTime date = DateTime.Today;
                    DateTime lønPeriodeFra = item.From;
                    DateTime lønPeriodeTil = item.To;
                    if (date >= lønPeriodeFra && date <= lønPeriodeTil)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {

                return false;
            }
            return false;
        }

        private LønPeriode FåFatILønPeriodeForArbejdsplads()
        {
            try
            {
                return database.FåLønPeriodeForArbejdsplads(globalVariables.ChosenCompany).First();

            }
            catch (Exception)
            {

                return null;
            }

        }


        private bool FindesDerArbejdsplads()
        {
            if (database.GetCompanies().Count > 0)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        public void TimerFunction()
        {
            while (true)
            {
                DateTime d = System.DateTime.Now;
                String dag = System.DateTime.Now.DayOfWeek.ToString();
                String date = System.DateTime.Now.Day.ToString();
                String måned = System.DateTime.Now.Month.ToString();
                String år = System.DateTime.Now.Year.ToString();
                String dateString = dag + " d. " + date + "-" + måned + "-" + år;
                TodaysDate = dateString;
                Thread.Sleep(100);

            }

        }

        private string GetUser()
        {
            var database = App.Database;
            return database.GetUser().FullName;
        }


        // EVENT HANDLERS

        private void SettingsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());

        }

        private void OpretArbejdsPlads_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetWorkPlaceName());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void GemBtn_Clicked(object sender, EventArgs e)
        {
            if (inputPause.Text == null)
            {
                inputPause.Text = "0";
            }
            var record = new Record
            {
                EndTime = TimeFrom.Time,
                StartTime = TimeTo.Time,
                LoggedDate = DateTime.Now,
                Pause = inputPause.Text,
                LønPeriodeID = globalVariables.ValgteLønPeriode.LønPeriodeID,

            };
            try
            {
                App.Database.AddRecord(record);
                DisplayAlert("Success", "Din arbejdsdag er blevet gemt under " + globalVariables.ChosenCompany, "Ok");

            }
            catch (Exception)
            {

                DisplayAlert("ERROR", "Der var en fejl ved gemning af din arbejdsdag", "Ok");
            }



        }

        private void ChooseOtherWorkPlacePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            String company = ChooseOtherWorkPlacePicker.SelectedItem.ToString();
            globalVariables.ChosenCompany = company;
            if (FåFatILønPeriodeForArbejdsplads() != null)
            {
                globalVariables.ValgteLønPeriode = FåFatILønPeriodeForArbejdsplads();
                LønPeriodeLabel = "Fra d. " + globalVariables.ValgteLønPeriode.From + " til d. " + globalVariables.ValgteLønPeriode.To;
                LønPeriodeForNuværendeMånedFundet = false;
                IngenLønPeriodeForNuværendeMånedFundet = true;

            }
            WhatCompanyLabel = company;
            Navigation.PushAsync(new TabbedPage1());

        }

        private void OpretLønPeriodeBtn_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new OpretNyLønPeriode());
        }
    }

}