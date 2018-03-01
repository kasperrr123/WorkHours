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
        public bool LønPeriodeForNuværendeMånedFundet { get; set; }
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
                if (FindesDerEnAktivLønPeriode())
                {
                    LønPeriodeLabel = "Fra d. " + globalVariables.ValgteLønPeriode.From + " til d. " + globalVariables.ValgteLønPeriode.To;
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

        private bool FindesDerEnAktivLønPeriode()
        {
            if (database.GetLønPerioder().Count > 0)
            {
                String todaysDate = System.DateTime.Now.ToString("dd/MM/yyyy");
                foreach (var item in database.GetLønPerioder().Where(n => n.Year == System.DateTime.Now.Year).Where(n=>n.CompanyName==globalVariables.ChosenCompany))
                {
                    if (DateTime.Parse(todaysDate) >= DateTime.Parse(item.From) && DateTime.Parse(todaysDate) <= DateTime.Parse(item.To))
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
            var record = new Record
            {
                EndTime = TimeFrom.Time.ToString(),
                StartTime = TimeTo.Time.ToString(),
                LoggedDate = DateTime.Now.ToString(),
                Pause = inputPause.Text.ToString(),
                LønPeriodeID = globalVariables.ValgteLønPeriode.LønPeriodeID,

            };
            App.Database.AddRecord(record);



        }

        private void ChooseOtherWorkPlacePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            String company = ChooseOtherWorkPlacePicker.SelectedItem.ToString();
            globalVariables.ChosenCompany = company;
            WhatCompanyLabel = company;
            // TO BE MADE - WHEN CHANGING COMPANY I NEED TO GET THE CORRECT LØN PERIODE ON THE HOME SCREEN. HAVE TO MAKE SOME KIND OF CHECK IF TODAYS DATE IS IN BETWEEN ONE OF
            // THE LØN PERIODER IN THE DATABASE.
            LønPeriodeLabel = "Fra d. " + globalVariables.ValgteLønPeriode.From + " til d. " + globalVariables.ValgteLønPeriode.To;


        }

        private void OpretLønPeriodeBtn_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new OpretNyLønPeriode());
        }
    }

}