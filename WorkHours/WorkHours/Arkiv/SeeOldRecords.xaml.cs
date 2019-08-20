
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using WorkHours.Data;
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

        public Models.LønPeriode LønPeriode { get; set; }

        public string LønPeriodeName { get; set; }

        public SeeOldRecords(Models.LønPeriode selectedItem)
        {
            BindingContext = this;
            this.LønPeriode = selectedItem;
            this.LønPeriodeName = selectedItem.Periode;
            Records = GetRecords();
            InitializeComponent();
        }

        private List<RecordView> GetRecords()
        {
            List<RecordView> list = new List<RecordView>();
            foreach (var record in App.Database.FåRecords(globalVariables.ChosenCompany, LønPeriode))
            {
                list.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime, record.LoggedDate));
            }

            return list;

        }

        private void ListOfRecords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = (ListView)sender;
            RecordView record = (RecordView)view.SelectedItem;
            ListViewRecords.Unfocus();
            Navigation.PushModalAsync(new ViewLogModal(record));


        }

        private void ExportToPdfBtn_Clicked(object sender, EventArgs e)
        {
            string filename = "TEST.txt";
            FileHandling file = new FileHandling();
            file.WriteSpecifikLønPeriode(LønPeriode, globalVariables.ChosenCompany);
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


    }
}