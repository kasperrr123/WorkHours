using Android.Icu.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        private Color labelColor;
        public Color LabelColor
        {
            get { return labelColor; }
            set
            {
                labelColor = value;
                INotifyPropertyChanged();
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

        public string CurrentCompany { get; set; }
        public LønPeriode CurrentLønPeriode { get; set; }

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

        public bool recordAlleredeOprettet { get; set; }
        public bool RecordAlleredeOprettet
        {
            get { return recordAlleredeOprettet; }
            set
            {
                recordAlleredeOprettet = value;
                INotifyPropertyChanged();
            }
        }

        public bool ingenLønPeriodeForNuværendeMånedFundet { get; set; }
        public bool IngenLønPeriodeForNuværendeMånedFundet
        {
            get { return ingenLønPeriodeForNuværendeMånedFundet; }
            set
            {
                ingenLønPeriodeForNuværendeMånedFundet = value;
                INotifyPropertyChanged();
            }
        }
        // Database instance
        private WorkHoursDatabaseController database = App.Database;
        // Global Variable instance
        private GlobalVariables globalVariables = GlobalVariables.Instance;
        private TimeSpan CurrentTimeSpan { get; set; }








        // CONSTRUCTOR
        public Home()
        {
            BindingContext = this;
            LabelColor = GetColor();
            UpdateAppTime();
            WelcomeUserLabel = database.GetUser().FullName;
            SetChooseWorkPlacePickerValues();
            HvilketPanelSkalVises();
            SetTimePickersToNow();
            InitializeComponent();
        }






        private void SetTimePickersToNow()
        {

            String h = DateTime.Now.Hour.ToString();
            String m = DateTime.Now.Minute.ToString();
            CurrentTimeSpan = TimeSpan.Parse(h + ":" + m);
        }

        private void UpdateAppTime()
        {
            ThreadStart timer = new ThreadStart(TimerFunction);
            Thread myThread = new Thread(timer);
            myThread.Start();
        }

        private Color GetColor()
        {
            if (database.GetCompany(globalVariables.ChosenCompany) != null)
            {
                switch (database.GetCompany(globalVariables.ChosenCompany).Color)
                {
                    case "Red":
                        return Color.FromRgb(255, 0, 0);

                    case "Green":
                        return Color.FromRgb(0, 255, 0);

                    case "Blue":
                        return Color.FromRgb(0, 0, 255);
                    case "Standard":
                        return Color.CadetBlue;
                    default:
                        break;
                }

            }
            return Color.CadetBlue;

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



            if (database.GetVariables().CurrentCompany != null)
            {
                CurrentCompany = database.GetVariables().CurrentCompany;
                WhatCompanyLabel = CurrentCompany;
                if (database.GetCompany(CurrentCompany).HasCurrentPeriode() != null)
                {
                    CurrentLønPeriode = database.GetCompany(CurrentCompany).HasCurrentPeriode();
                    LønPeriodeLabel = "Fra d. " + CurrentLønPeriode.From.ToString("dd/MM/yyyy") + " til d. " + CurrentLønPeriode.To.ToString("dd/MM/yyyy");

                    if (CurrentLønPeriode.HasRecordsForToday(CurrentLønPeriode))
                    {
                        LønPeriodeForNuværendeMånedFundet = false;
                        RecordAlleredeOprettet = true;
                    }
                    else
                    {
                        RecordAlleredeOprettet = false;
                        LønPeriodeForNuværendeMånedFundet = true;
                    }
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





            //Tjek om der er blevet oprettet en arbejdsplads.
            //if (FindesDerArbejdsplads())
            //{
            //    WhatCompanyLabel = database.GetVariables().CurrentCompany;
            //    // Tjek om der er oprettet en løn periode.
            //    if (FindesDerEnAktivLønPeriodeForArbejdsplads())
            //    {
            //        var lønPerioder = database.FåLønPerioderForArbejdsplads(CurrentCompany);
            //        CurrentLønPeriode = lønPerioder.Where(n => n.To.Ticks > DateTime.Now.Ticks).First();
            //        LønPeriodeLabel = "Fra d. " + CurrentLønPeriode.From.ToString("dd/MM/yyyy") + " til d. " + CurrentLønPeriode.To.ToString("dd/MM/yyyy");
            //        database.UpdateVariables(new Variables
            //        {
            //            ID = 1,
            //            CurrentCompany = CurrentCompany,

            //        });

            //        LønPeriodeForNuværendeMånedFundet = true;
            //        // Tjekker om der er allerede er blevet registreret en vagt
            //        if (FindesDerAlleredeEnRecordForIDag())
            //        {
            //            LønPeriodeForNuværendeMånedFundet = false;
            //            RecordAlleredeOprettet = true;
            //        }
            //        else
            //        {
            //            RecordAlleredeOprettet = false;
            //        }
            //    }
            //    else
            //    {
            //        LønPeriodeForNuværendeMånedFundet = false;
            //        IngenLønPeriodeForNuværendeMånedFundet = true;
            //    }
            //}
            //else
            //{
            //    IngenArbejdsPladsOprettet = true;
            //}


        }

        private bool FindesDerAlleredeEnRecordForIDag()
        {
            if (database.FindesDerRecordForDagsDato(globalVariables.ValgteLønPeriode, DateTime.Now) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool FindesDerEnAktivLønPeriodeForArbejdsplads()
        {

            if (database.GetLønPerioder().Count > 0)
            {
                foreach (var item in database.GetLønPerioder().Where(n => n.Year == System.DateTime.Now.Year).Where(n => n.CompanyName == database.GetVariables().CurrentCompany))
                {
                    DateTime date = DateTime.Today;
                    DateTime lønPeriodeFra = item.From;
                    DateTime lønPeriodeTil = item.To;
                    if (date >= lønPeriodeFra && date <= lønPeriodeTil)
                    {
                        return true;
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
                return database.FåLønPerioderForArbejdsplads(globalVariables.ChosenCompany).Last();
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

                String dag = FormatToDA.GetDayOfWeekInDA(System.DateTime.Now.DayOfWeek.ToString());
                String date = System.DateTime.Now.Day.ToString();
                String måned = System.DateTime.Now.Month.ToString();
                String år = System.DateTime.Now.Year.ToString();
                String dateString = dag + " d. " + date + "-" + måned + "-" + år;
                TodaysDate = dateString;
                Thread.Sleep(100);

            }

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
                StartTime = TimeFrom.Time,
                EndTime = TimeTo.Time,
                LoggedDate = DateTime.Now,
                Pause = inputPause.Text,
                LønPeriodeID = CurrentLønPeriode.LønPeriodeID,

            };
            try
            {
                App.Database.AddRecord(record);
                App.Database.Commit();

                DisplayAlert("Success", "Din arbejdsdag er blevet gemt under " + CurrentCompany, "Ok");


            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "Der var en fejl ved gemning af din arbejdsdag: Fejlkode: " + ex.Message, "Ok");
            }



        }

        private void ChooseOtherWorkPlacePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var company = ChooseOtherWorkPlacePicker.SelectedItem.ToString();
            WhatCompanyLabel = company;

            if (FåFatILønPeriodeForArbejdsplads() != null)
            {
                LønPeriodeLabel = "Fra d. " + globalVariables.ValgteLønPeriode.From + " til d. " + globalVariables.ValgteLønPeriode.To;
                LønPeriodeForNuværendeMånedFundet = false;
                IngenLønPeriodeForNuværendeMånedFundet = true;
            }

            database.UpdateVariables(new Variables
            {
                ID = 1,
                CurrentCompany = company,

            });


            Navigation.PushAsync(new TabbedPage1());

        }

        private void OpretLønPeriodeBtn_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new OpretNyLønPeriode());
        }

        // Getting JSON
        private void DeserializeGlobalVariablesJson()
        {
            GlobalVariables variables = GlobalVariables.Instance;
            try
            {
                var sqliteFileName = "GlobalVariables.txt";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFileName);
                using (StreamReader sr = new StreamReader(path))
                {

                    var obj = JsonConvert.DeserializeObject<GlobalVariables>(sr.ReadLine());

                    variables.ChosenCompany = obj.ChosenCompany;
                    variables.LønPeriode_GårFraDag = obj.LønPeriode_GårFraDag;
                    variables.LønPeriode_GårTilDag = obj.LønPeriode_GårTilDag;
                    variables.ValgteLønPeriode = obj.ValgteLønPeriode;

                    Console.WriteLine("JSON FILE deserialized");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No file found");
            }
        }

        protected override void OnAppearing()
        {
            HvilketPanelSkalVises();
        }

    }

}