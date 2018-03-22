using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private GlobalVariables globalVariables = GlobalVariables.Instance;
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
                "Aften tillæg", "Syge tillæg", "Nat tillæg", "Søndags tillæg", "Lørsdags Tillæg"
            };
        }

        private void TilføjTillæg_Clicked(object sender, EventArgs e)
        {
            Tillæg tillæg = new Tillæg
            {
                CompanyName = globalVariables.ChosenCompany,
                TypeOfTillæg = TillægPicker.SelectedItem.ToString(),
                TillægKr = KrField.Text,
                From = TillægTimePicker.Time
            };

            // Tilføj tillæg til database.
            //database.AddTillæg(tillæg);
            //database.Commit();

            ListOfTillæg.Add(tillæg);

        }

        private void GåVidereBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void DeleteTillægFromList_Clicked(object sender, EventArgs e)
        {
            var selectedItem = ListView1.SelectedItem;
            ListOfTillæg.Remove(ListOfTillæg.Where(n => n.TypeOfTillæg == selectedItem));
        }
    }
}