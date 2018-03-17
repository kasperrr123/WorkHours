using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.Arkiv
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArkivPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private List<Period> listOfRecords;
        private String totalTimer;
        private GlobalVariables globalVariables = GlobalVariables.Instance;
        public String TotalTimer
        {
            get { return totalTimer; }
            set
            {
                totalTimer = value;
                INotifyPropertyChanged();
            }
        }
        private String totalPause;
        public List<String> Perioder { get; set; }
        private WorkHoursDatabaseController database = App.Database;
        public String TotalPause
        {
            get { return totalPause; }
            set
            {
                totalPause = value;
                INotifyPropertyChanged();
            }
        }
        public List<Period> ListOfRecords
        {
            get { return listOfRecords; }
            set
            {
                listOfRecords = value;
                SetTotalHoursAndBreaks();
                INotifyPropertyChanged();
            }
        }

        public ArkivPage()
        {
            BindingContext = this;
            FåRecords();
            SetTotalHoursAndBreaks();
            InitializeComponent();
        }

      

        private void SetTotalHoursAndBreaks()
        {
            Calculations cal = new Calculations();
            var hours = cal.GetTotalHours(globalVariables.ChosenCompany, globalVariables.ValgteLønPeriode);
            var minutes = cal.GetTotalBreak(globalVariables.ChosenCompany, globalVariables.ValgteLønPeriode);
            TotalTimer = hours[0].ToString() + "t " + hours[1].ToString() + "m";
            TotalPause = minutes[0].ToString() + "t " + minutes[1].ToString() + "m";

        }

        public void FåRecords()
        {
            List<Period> list = new List<Period>();
            if (GlobalVariables.Instance.ValgteLønPeriode != null)
            {
                try
                {
                    var a = database.FåLønPerioderForArbejdsplads(globalVariables.ChosenCompany).Where(n => n.To > DateTime.Now).First();
                    foreach (var item in App.Database.FåRecords(GlobalVariables.Instance.ChosenCompany, a))
                    {
                        list.Add(new Period(item.LoggedDate, item.StartTime, item.EndTime));
                    }

                    ListOfRecords = list;
                }
                catch (Exception)
                {


                }

            }

        }

        protected override void OnAppearing()
        {
            FåRecords();
        }



        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cell = (ListView)sender;
            ListViewRecords.Unfocus();
            Navigation.PushModalAsync(new ViewLogModal(cell));


        }

        private void SeeAllPeriodsBtn_Clicked(object sender, EventArgs e)
        {
            

            Navigation.PushAsync(new SeeOldPeriod());
        }
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