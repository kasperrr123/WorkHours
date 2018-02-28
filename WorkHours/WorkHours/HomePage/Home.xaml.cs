using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage, INotifyPropertyChanged
    {
        private String date;

        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public String Date
        {
            get { return date; }
            set
            {
                date = value;
                INotifyPropertyChanged();
            }
        }

        public List<String> Companies { get; set; }

        public String WhatCompany { get; set; }

        public String WelcomeUser { get; set; }

        public bool ActiveMonth { get; set; }
        public bool NoActiveMonth { get; set; }


        public Home()
        {
            BindingContext = this;
            ThreadStart timer = new ThreadStart(TimerFunction);
            Thread myThread = new Thread(timer);
            myThread.Start();
            WelcomeUser = GetUser();
            WhatCompany = "Der er ingen arbejdsplads oprettet endnu.";
            CheckIfAvtiveCompany();
            Companies = GetCompanyNames();
            InitializeComponent();
        }



        private void CheckIfAvtiveCompany()
        {
            var database = App.Database;
            if (database.GetCompanies().Count == 0)
            {
                NoActiveMonth = true;
                ActiveMonth = false;
            }
            else
            {
                WhatCompany = "Arbejdsplads: " + App.Database.GetCompanies().First().CompanyName;

                NoActiveMonth = false;
                ActiveMonth = true;
            }

        }
        public List<String> GetCompanyNames()
        {
            List<String> a = new List<string>();
            foreach (var item in App.Database.GetCompanies())
            {
                a.Add(item.CompanyName);
            }

            return a;
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
                Date = dateString;
                Thread.Sleep(100);

            }

        }

        private string GetUser()
        {
            var database = App.Database;
            return database.GetUser().FullName;
        }


        private void SettingsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());

        }

        private void NoAvtiveMonthCreateWorkPlaceBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetWorkPlaceName());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void GemBtn_Clicked(object sender, EventArgs e)
        {
            var record = new Models.Record
            {
                EndTime = TimeFrom.Time.ToString(),
                StartTime = TimeTo.Time.ToString(),
                LoggedDate = System.DateTime.Now.ToString(),
                Pause = inputPause.Text.ToString(),

            };
            App.Database.AddRecord(record);



        }

        private void ChooseOtherWorkPlaceBtn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}