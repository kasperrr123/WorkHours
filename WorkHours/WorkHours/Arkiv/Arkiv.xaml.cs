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
        private String totalTimer;
        public Color ThemeColor { get; set; }
        public string labelText { get; set; }
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

        public ArkivPage()
        {
            BindingContext = this;
            if (globalVariables.ValgteLønPeriode != null)
            {
                labelText = globalVariables.ValgteLønPeriode.Periode;
            }
            else
            {
                labelText = "Ingen periode oprettet endnu";
            }
            ThemeColor = GetColor();
            SetRecords();
            SetTotalHoursAndBreaks();
            InitializeComponent();
        }
        private Color GetColor()
        {
            if (database.GetCompany(globalVariables.ChosenCompany) != null)
            {
                switch (database.GetCompany(globalVariables.ChosenCompany).Color)
                {
                    case "Red":
                        return Color.FromRgb(255, 0, 0);

                    case "Green":
                        return Color.FromRgb(0, 255, 0);

                    case "Blue":
                        return Color.FromRgb(0, 0, 255);
                    case "Standard":
                        return Color.CadetBlue;
                    default:
                        break;
                }

            }
            return Color.CadetBlue;
        }

        private void SetTotalHoursAndBreaks()
        {
            Calculations cal = new Calculations();
            var hours = cal.GetTotalHours(globalVariables.ChosenCompany, globalVariables.ValgteLønPeriode);
            var minutes = cal.GetTotalBreak(globalVariables.ChosenCompany, globalVariables.ValgteLønPeriode);
            TotalTimer = hours[0].ToString() + "t " + hours[1].ToString() + "m";
            TotalPause = minutes[0].ToString() + "t " + minutes[1].ToString() + "m";

        }

        public void SetRecords()
        {
            ListOfRecords = new List<RecordView>();
            if (GlobalVariables.Instance.ValgteLønPeriode != null)
            {
                try
                {
                    var NuværendeLønPeriode = database.FåLønPerioderForArbejdsplads(globalVariables.ChosenCompany).Where(n => n.To > DateTime.Now).First();
                    foreach (var record in App.Database.FåRecords(GlobalVariables.Instance.ChosenCompany, NuværendeLønPeriode))
                    {
                      
                        ListOfRecords.Add(new RecordView(record.LoggedDate, record.StartTime, record.EndTime));
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnAppearing()
        {
            SetRecords();
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