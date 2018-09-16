using System;
using System.Collections.Generic;
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
            var cell = (ListView)sender;
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
    }
}