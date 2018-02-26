using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkHours.Data;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


namespace WorkHours
{
    public partial class App : Application
    {

        static WorkHoursDatabaseController userDatabase;

        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new FirstTimeUse());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
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
