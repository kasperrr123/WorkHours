using Android;
using Android.Content;
using Android.Support.V4.Content;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using WorkHours.CreateNewWorkPlace;
using WorkHours.Data;
using WorkHours.Models;
using WorkHours.UpdateWorkPlace;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {

        public List<String> ArbejdsPladser { get; set; }

        public String CantFindAnyCompany { get; set; }

        public Settings()
        {
            BindingContext = this;
            ArbejdsPladser = new List<String>();
            ArbejdsPladser = GetCompanies();
            InitializeComponent();


        }

        private List<String> GetCompanies()
        {

            List<String> listOfCompanies = new List<String>();
            if (App.Database.GetCompanies().Count > 0)
            {

                foreach (var item in App.Database.GetCompanies())
                {
                    listOfCompanies.Add(item.CompanyName);
                }

                return listOfCompanies;


            }
            return new List<string> { "Ingen arbejdspladser er oprettet endnu" };
        }

        private void OpretNyArbejdspladsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetWorkPlaceName());
        }

        private void ÆndreArbejdspladsBtn_Clicked(object sender, EventArgs e)
        {
            if (WorkPlacePicker.SelectedItem != null)
            {
                Navigation.PushAsync(new UpdateWorkPlaceNameAndTimeLøn(WorkPlacePicker.SelectedItem.ToString()));
            }
        }

        private async Task ResetAppBtn_ClickedAsync(object sender, EventArgs e)
        {
            var alert = await DisplayAlert("Vigtigt!", "Hvis du trykker ok vil din app blive nulstillet og du mister ALT dit data", "Ok", "Gå tilbage");
            if (alert == true)
            {
                try
                {
                    // Deleting json file.
                    var sqliteFileName = "GlobalVariables.txt";
                    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    var path = Path.Combine(documentsPath, sqliteFileName);
                    File.Delete(path);
                    Console.WriteLine("JSON File deleted");
                    // Deleting database
                    App.Database.DeleteDatabase();
                    await DisplayAlert("Succes", "Database has been deleted", "Return to home");
                    await Navigation.PushAsync(new FirstTimeUse());
                }
                catch (Exception x)
                {
                    await DisplayAlert("Error", "Error with deleting database: " + x.Message, "ok");

                }
            }


        }

        private void ExportToPdfBtn_Clicked(object sender, EventArgs e)
        {
#if __ANDROID__


           
            string documentsPath = global::Android.OS.Environment.ExternalStorageDirectory.Path + "/" + global::Android.OS.Environment.DirectoryDownloads +"/" + "Backup.txt";
            using (var tw = new StreamWriter(documentsPath, true))
            {
                tw.WriteLine("HellooooO");
                tw.Flush();
            };
            


            Java.IO.File file = new Java.IO.File(documentsPath);
            file.SetReadable(true);
            string application = "";
            string extension = Path.GetExtension(documentsPath);

   
            switch (extension.ToLower())
            {
                case ".txt":
                    application = "text/plain";
                    break;
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }
            try
            {
                Intent intent = new Intent(Intent.ActionView);
               
                Android.Net.Uri uri = FileProvider.GetUriForFile(Android.App.Application.Context, "", file);
                intent.SetDataAndType(uri, application);
                Android.App.Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                // no Activity to handle this kind of files
            }
            //Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
            //Intent intent = new Intent(Intent.ActionView);
            //intent.SetDataAndType(uri, application);
            //intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
            //try
            //{
            //    Android.App.Application.Context.StartActivity(intent);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.Source);
            //}

#endif




        }

    }
}
