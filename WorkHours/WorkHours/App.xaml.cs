
using Newtonsoft.Json;
using System;
using System.IO;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Data;
using WorkHours.HomePage;
using WorkHours.Models;
using Xamarin.Forms;

namespace WorkHours
{
    public partial class App : Application
    {

        static WorkHoursDatabaseController userDatabase;

        public App()
        {

            MainPage = new TabbedPage1();

            InitializeComponent();
        }



        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }




        public static WorkHoursDatabaseController Database
        {
            get
            {
                if (userDatabase == null)
                {

                    userDatabase = new WorkHoursDatabaseController();
                }
                return userDatabase;
            }
        }



    }
}
