using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using WorkHours.Models;
using WorkHours.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.Arkiv
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeOldPeriods : ContentPage
    {
        public List<PeriodeView> Perioder { get; set; }
        public string Text { get; set; }
        public object ListOfRecords { get; private set; }

        public WorkHoursDatabaseController database = App.Database;

        public SeeOldPeriods()
        {
            BindingContext = this;
            this.Text = "Vælg en periode for året " + DateTime.Now.Year;
            this.Perioder = GetPerioder();
            InitializeComponent();
        }


        private List<PeriodeView> GetPerioder()
        {
            List<PeriodeView> list = new List<PeriodeView>();
            var perioder = database.FåLønPerioderForArbejdsplads(database.GetVariables().CurrentCompany, DateTime.Now.Year);
            int counter = 1;
            foreach (var item in perioder)
            {
                if ((counter % 2) == 0)
                {
                    list.Add(new PeriodeView(item, "LightGray"));
                }
                else
                {
                    list.Add(new PeriodeView(item, "Gray"));
                }
                counter++;
            }
            return list;
        }


        private void ListOfPeriods_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = sender as ListView;
            PeriodeView periode = (PeriodeView)view.SelectedItem;
            LønPeriode lønPeriode = database.GetLønPeriode(periode.Periode.LønPeriodeID);
            Navigation.PushAsync(new SeeOldRecords(lønPeriode));
        }
    }
}