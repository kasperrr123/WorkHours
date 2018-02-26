using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkHours.CreateNewWorkPlace;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.HomePageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage, INotifyPropertyChanged
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

        public String WhatCompany { get; set; }

        public String WelcomeUser { get; set; }
        public HomePage()
        {
            BindingContext = this;
            ThreadStart timer = new ThreadStart(TimerFunction);
            Thread myThread = new Thread(timer);
            myThread.Start();
            WhatCompany = "Arbejdsplads: ";
            WelcomeUser = "Velkommen tilbage " + GetUser();
            InitializeComponent();
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
        private string GetCompany()
        {
            var database = App.Database;
            var companies = database.GetCompanies();

            return companies.ToString();
        }

        private void SettingsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OpretNy());

        }
    }
}