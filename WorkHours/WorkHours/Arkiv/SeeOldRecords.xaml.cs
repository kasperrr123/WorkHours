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

        public List<Period> Records { get; set; }

        public String LønPeriode { get; set; }
        public SeeOldRecords(String selectedItem)
        {
            BindingContext = this;
            this.LønPeriode = selectedItem;
            Records = GetRecords();
            InitializeComponent();
        }

        private List<Period> GetRecords()
        {
            List<Period> list = new List<Period>();
            var a = database.FåLønPerioderForArbejdsplads(globalVariables.ChosenCompany).Where(n=>n.Periode==LønPeriode).First();
            foreach (var item in App.Database.FåRecords(GlobalVariables.Instance.ChosenCompany, a))
            {
                list.Add(new Period(item.LoggedDate, item.StartTime, item.EndTime));
            }

            return list;

        }

        private void ListOfRecords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cell = (ListView)sender;
            ListOfRecords.Unfocus();
            Navigation.PushModalAsync(new ViewLogModal(cell));
           
        }

        public struct Period
        {
            public DateTime Date { get; set; }
            public TimeSpan From { get; set; }
            public TimeSpan To { get; set; }


            public Period(DateTime date, TimeSpan from, TimeSpan to)
            {
                this.Date = date;
                this.From = from;
                this.To = to;

            }

            public override string ToString()
            {
                return Date.DayOfWeek + " " + Date;
            }

        }
    }
}