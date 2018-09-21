using Android.Content;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.Arkiv
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeOldRecords : ContentPage
    {
        private GlobalVariables globalVariables = GlobalVariables.Instance;
        public WorkHoursDatabaseController database = App.Database;

        public List<RecordView> Records { get; set; }

        public String LønPeriode { get; set; }


        public SeeOldRecords(String selectedItem)
        {
            BindingContext = this;
            this.LønPeriode = selectedItem;
            Records = GetRecords();
            InitializeComponent();
        }

        private List<RecordView> GetRecords()
        {
            List<RecordView> list = new List<RecordView>();
            var a = database.FåLønPerioderForArbejdsplads(globalVariables.ChosenCompany).Where(n=>n.Periode==LønPeriode).First();
            foreach (var item in App.Database.FåRecords(GlobalVariables.Instance.ChosenCompany, a))
            {
                list.Add(new RecordView(item.LoggedDate, item.StartTime, item.EndTime));
            }

            return list;

        }

        private void ListOfRecords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cell = (Xamarin.Forms.ListView)sender;
            ListOfRecords.Unfocus();
            Navigation.PushModalAsync(new ViewLogModal(cell));
           
        }

        public struct RecordView
        {
            public DateTime Date { get; set; }

            public String OnlyDate { get; set; }
            public TimeSpan From { get; set; }
            public TimeSpan To { get; set; }

            public string FromToString { get; set; }

            public RecordView(DateTime date, TimeSpan from, TimeSpan to)
            {
                this.Date = date;
                this.From = from;
                this.To = to;
                this.OnlyDate = date.ToString("dd/MM/yy");
                this.FromToString = From.Hours + "." + From.Minutes + "-" + To.Hours + "." + To.Minutes;

            }

            public override string ToString()
            {
                return Date.DayOfWeek + " " + Date;
            }

        }

        private void ExportToPdfBtn_Clicked(object sender, EventArgs e)
        {

#if __ANDROID__


            FileHandling file = new FileHandling();
            file.WriteSpecifikLønPeriode(globalVariables.ValgteLønPeriode, globalVariables.ChosenCompany);



            // Trying to open the created txt file, but android seems to have bunch of errors.
            /*
             
            OpenFile(filePath);

            */
#endif


        }





        private void OpenFile(string filePath)
        {

            var bytes = File.ReadAllBytes(filePath);

            //Copy the private file's data to the EXTERNAL PUBLIC location
            string externalStorageState = global::Android.OS.Environment.ExternalStorageState;
            string application = "";

            string extension = System.IO.Path.GetExtension(filePath);

            switch (extension.ToLower())
            {
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
                case ".txt":
                    application = "text/plain";
                    break;
                default:
                    application = "*/*";
                    break;
            }
            var externalPath = filePath;
            File.WriteAllBytes(externalPath, bytes);

            Java.IO.File file = new Java.IO.File(externalPath);
            file.SetReadable(true);
            //Android.Net.Uri uri = Android.Net.Uri.Parse("file://" + filePath);
            Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, application);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

            try
            {
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, ex.Message, ToastLength.Short).Show();
            }
        }




    }
}