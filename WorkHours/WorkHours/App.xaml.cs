using Android;
using Android.Content.PM;
using Android.OS;
using Newtonsoft.Json;
using System;
using System.IO;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Data;
using WorkHours.HomePage;
using Xamarin.Forms;

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
            // Here i'm initializing the JsonSerializer.
            JsonSerializer serializer = new JsonSerializer();
            // Using a StreamWriter to create the txt file i want.
            var sqliteFileName = "GlobalVariables.txt";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);
            using (StreamWriter sw = new StreamWriter(path))
            // Creating the JsonWriter and giving it the specificed textWriter i want, which here is the StreamWriter (sw). 
            // Then i serialize my object and prints it to a txt file.
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, GlobalVariables.Instance);
            }
            
            Console.WriteLine("JSON file created");
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
