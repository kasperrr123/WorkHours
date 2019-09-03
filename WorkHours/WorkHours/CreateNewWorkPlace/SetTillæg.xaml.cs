using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Widget;
using System.Threading.Tasks;
using WorkHours.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHours;
using WorkHours.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WorkHours.CreateNewWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetTillæg : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private WorkHoursDatabaseController database = App.Database;
        private ObservableCollection<Tillæg> listOfTillæg;
        private List<Tillæg> TempListOfTillæg;

        public ObservableCollection<Tillæg> ListOfTillæg
        {
            get { return listOfTillæg; }
            set
            {
                listOfTillæg = value;
                INotifyPropertyChanged();
            }
        }

        public List<String> PickerTillæg { get; set; }
        public SetTillæg()
        {
            BindingContext = this;
            ListOfTillæg = new ObservableCollection<Tillæg>();
            PickerTillæg = new List<string>();
            TempListOfTillæg = new List<Tillæg>();
            PickerTillæg = ForskelligeTillæg();
            InitializeComponent();
        }

        private List<String> ForskelligeTillæg()
        {
            return new List<string>
            {
                "Aften tillæg", "Syge tillæg", "Nat tillæg", "Søndags tillæg", "Lørsdags tillæg"
            };
        }

        private void TilføjTillæg_Clicked(object sender, EventArgs e)
        {

            if (TillægPicker.SelectedItem != null && KrField.Text != null)
            {

                String Type = "";
                switch (TillægPicker.SelectedItem.ToString())
                {
                    case "Aften tillæg":
                        Type = "Aften";
                        break;
                    case "Syge tillæg":
                        Type = "Syge";
                        break;
                    case "Nat tillæg":
                        Type = "Nat";
                        break;
                    case "Søndags tillæg":
                        Type = "Søndag";
                        break;
                    case "Lørdags tillæg":
                        Type = "Lørdag";
                        break;

                    default:
                        Type = "Fejl";
                        break;
                }
                Tillæg tillæg = new Tillæg
                {
                    CompanyName = CreateNewWorkPlaceObj.Instance.CompanyName,
                    TypeOfTillæg = Type,
                    TillægKr = KrField.Text,
                    From = TillægTimePicker.Time,
                    AllDay = AllDaySwitch.IsToggled

                };

                foreach (var item in ListOfTillæg)
                {
                    if (item.TypeOfTillæg == tillæg.TypeOfTillæg)
                    {
#if __ANDROID__
                        Toast.MakeText(Forms.Context, "Tillæg allerede tilføjet", ToastLength.Short).Show();
                        return;
#endif
                        DisplayAlert("Hov!", "Tillæg allerede tilføjet", "Ok");
                    }
                }


                ListOfTillæg.Add(tillæg);

            }
            else
            {
                DisplayAlert("Hov!", "Du skal vælge et tillæg samt fortælle, hvor mange kr du får extra de timer", "Ok");
            }
        }

        private void GåVidereBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FinalPage(ListOfTillæg.ToList()));
        }

        private void SletTillæg_Clicked(object sender, EventArgs e)
        {
            if (ListOfTillæg.Count > 0)
            {
                ListOfTillæg.Remove(ListOfTillæg.Last());
            }

        }


    }
}