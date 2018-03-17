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
    public partial class SeeOldPeriod : ContentPage
    {
        public List<String> Perioder { get; set; }

        public WorkHoursDatabaseController database = App.Database;
        private GlobalVariables globalVariables = GlobalVariables.Instance;

        public SeeOldPeriod()
        {
            BindingContext = this;
            //List<Period> list = new List<Period>();
            //foreach (var item in listOfData)
            //{
            //    list.Add(new Period(item.LoggedDate, item.StartTime, item.EndTime));
            //}
            this.Perioder = GetPerioder();
            //this.Lønperiode = "Løn periode fra: " + lønPeriode.From.ToString("DD/MMMM/YYYY") + " til d. " + lønPeriode.To.ToString("DD/MMMM/YYYY");
            InitializeComponent();
        }


        private List<string> GetPerioder()
        {
            List<string> list = new List<string>();
            var perioder = database.FåLønPerioderForArbejdsplads(globalVariables.ChosenCompany);
            foreach (var item in perioder)
            {
                list.Add(item.Periode);
            }
            return list;
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

        private void ListOfPeriods_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = sender as ListView;
            Navigation.PushAsync(new SeeOldRecords(view.SelectedItem.ToString()));

        }
    }
}