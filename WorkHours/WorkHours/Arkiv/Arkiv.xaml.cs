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
        private List<RecordView> listOfRecords;
        public List<RecordView> ListOfRecords
        {
            get { return listOfRecords; }
            set
            {
                listOfRecords = value;
                SetTotalHoursAndBreaks();
                INotifyPropertyChanged();
            }
        }
        public string CurrentCompany { get; set; }
        public LønPeriode CurrentPeriode { get; set; }
        public string PeriodeLabel { get; set; }
        private String totalTimer;
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
        public String TotalPause
        {
            get { return totalPause; }
            set
            {
                totalPause = value;
                INotifyPropertyChanged();
            }
        }
        public string PeriodeFraLabel { get; set; }
        public string PeriodeTilLabel { get; set; }

        private WorkHoursDatabaseController database = App.Database;


        public ArkivPage()
        {
            CurrentCompany = database.GetVariables().CurrentCompany;
            BindingContext = this;

            if (database.GetCompany(CurrentCompany).HasCurrentPeriode() == null)
            {
                PeriodeLabel = "Ingen periode oprettet endnu";
            }
            else
            {
                CurrentPeriode = database.GetCompany(CurrentCompany).HasCurrentPeriode();
                PeriodeFraLabel = CurrentPeriode.From.ToString("dd-MM-yyyy");
                PeriodeTilLabel = CurrentPeriode.To.ToString("dd-MM-yyyy");
                PeriodeLabel = CurrentPeriode.Periode;
                SetRecords();
                SetTotalHoursAndBreaks();
            }

            InitializeComponent();
        }


        private void SetTotalHoursAndBreaks()
        {
            Calculations cal = new Calculations();
            var hours = cal.GetTotalHours(CurrentCompany, CurrentPeriode);
            var minutes = cal.GetTotalBreak(CurrentCompany, CurrentPeriode);
            TotalTimer = hours[0].ToString() + "t " + hours[1].ToString() + "m";
            TotalPause = minutes[0].ToString() + "t " + minutes[1].ToString() + "m";

        }

        public void SetRecords()
        {
            List<RecordView> list = new List<RecordView>();
            try
            {
                foreach (var record in App.Database.FåRecordsByPeriode(CurrentPeriode))
                {
                    if (record.LoggedDate.DayOfWeek.Equals(DayOfWeek.Saturday))
                    {
                        list.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime, record.LoggedDate, "Yellow"));
                    }
                    else if (record.LoggedDate.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        list.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime, record.LoggedDate, "Red"));
                    }
                    else
                    {
                        list.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime, record.LoggedDate, "LightBlue"));
                    }
                }
               ListOfRecords = list.OrderBy(n => n.LoggedDate).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
             
            }


        }

        protected override void OnAppearing()
        {
            SetRecords();
        }

        private void ListOfRecords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = (ListView)sender;
            RecordView record = (RecordView)view.SelectedItem;
            ListViewRecords.Unfocus();
            Navigation.PushModalAsync(new ViewLogModal(record));
        }

        private void SeeAllPeriodsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeOldPeriods());
        }

        private void SeLønSeddelBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SeLønSeddel(database.GetLønPeriode(CurrentPeriode.LønPeriodeID)));
        }
    }


}