
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.Arkiv
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeOldRecords : ContentPage
    {
        public WorkHoursDatabaseController database = App.Database;

        public List<RecordView> Records { get; set; }
        public string Company { get; set; }
        public Models.LønPeriode LønPeriode { get; set; }
        public string LønPeriodeName { get; set; }
        public List<RecordView> ListOfRecords { get; set; }
        public string CurrentCompany { get; set; }
        public LønPeriode CurrentPeriode { get; set; }
        public string PeriodeLabel { get; set; }
        public String TotalTimer { get; set; }
        public String TotalPause { get; set; }
        public string PeriodeFraLabel { get; set; }
        public string PeriodeTilLabel { get; set; }

        public SeeOldRecords(LønPeriode selectedItem)
        {
            BindingContext = this;
            this.LønPeriode = selectedItem;
            PeriodeLabel = LønPeriode.Periode + ", " + this.LønPeriode.Year;
            PeriodeFraLabel = this.LønPeriode.From.ToString("dd-MM-yyyy");
            PeriodeTilLabel = this.LønPeriode.To.ToString("dd-MM-yyyy");
            this.LønPeriodeName = selectedItem.Periode;
            this.ListOfRecords = GetRecords();
            SetTotalHoursAndBreaks();
            InitializeComponent();
        }



        private List<RecordView> GetRecords()
        {
            List<RecordView> list = new List<RecordView>();
            foreach (var record in App.Database.FåRecordsByPeriode(LønPeriode))
            {
                if (record.LoggedDate.DayOfWeek.Equals(DayOfWeek.Saturday) || record.LoggedDate.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    list.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime, record.LoggedDate, "Red"));
                }
                else
                {
                    list.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime, record.LoggedDate, "LightBlue"));
                }
            }

            return list.OrderBy(n => n.LoggedDate).ToList();

        }

        private void SetTotalHoursAndBreaks()
        {
            Calculations cal = new Calculations();
            var hours = cal.GetTotalHours(LønPeriode);
            var minutes = cal.GetTotalBreak(LønPeriode);
            TotalTimer = hours[0].ToString() + "t " + hours[1].ToString() + "m";
            TotalPause = minutes[0].ToString() + "t " + minutes[1].ToString() + "m";

        }

        private void ExportToPdfBtn_Clicked(object sender, EventArgs e)
        {
            string filename = "TEST.txt";
            FileHandling file = new FileHandling();
            file.WriteSpecifikLønPeriode(LønPeriode, App.Database.GetVariables().CurrentCompany);
        }

        private void ListOfRecords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = (ListView)sender;
            RecordView record = (RecordView)view.SelectedItem;
            ListViewRecords.Unfocus();
            Navigation.PushModalAsync(new ViewLogModal(record));
        }


        private void OpenFile(string filePath)
        {

            var bytes = File.ReadAllBytes(filePath);

            //Copy the private file's data to the EXTERNAL PUBLIC location

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



            //Android.Net.Uri uri = Android.Net.Uri.Parse("file://" + filePath);




        }

        private void SeLønSeddelBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SeLønSeddel(database.GetLønPeriode(this.LønPeriode.LønPeriodeID)));
        }


    }
}