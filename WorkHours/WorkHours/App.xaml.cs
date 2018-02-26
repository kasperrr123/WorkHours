using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkHours.Data;
using WorkHours.HomePageFolder;
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



            if (Database.GetUser() == null)
            {
                MainPage = new NavigationPage(new FirstTimeUse());

            }
            else
            {
                MainPage = new NavigationPage(new TabbedPage1());

            }




        }

        protected override void OnStart()
        {

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
