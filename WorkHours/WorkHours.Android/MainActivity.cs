using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Newtonsoft.Json;

namespace WorkHours.Droid
{
    [Activity(Label = "WorkHours", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SQLitePCL.Batteries.Init();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


        }
        protected override void OnResume()
        {
            base.OnResume();
            DeserializeGlobalVariablesJson();
        }

        // WTF
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
    }
}

