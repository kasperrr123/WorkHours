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
	public partial class ViewLogModal : ContentPage
	{
        // Database instance
        private WorkHoursDatabaseController database = App.Database;
        public String Oprettet { get; set; }
        public TimeSpan Fra { get; set; }
        public TimeSpan Til { get; set; }
        public ListView SelectedListView { get; set; }
        public ViewLogModal (ListView listview)
		{
            BindingContext = this;
            this.SelectedListView = listview;
            GetDataOnSpecificLog();
			InitializeComponent ();
		}

        private void GetDataOnSpecificLog()
        {
            Oprettet = SelectedListView.SelectedItem.ToString();
            DateTime a = DateTime.Parse(Oprettet);
            Console.WriteLine(GlobalVariables.Instance.ValgteLønPeriode.From);
            Console.WriteLine(GlobalVariables.Instance.ValgteLønPeriode.To);
            Console.WriteLine(GlobalVariables.Instance.ValgteLønPeriode.CompanyName);
            List<Record> lønperioder = database.FåRecords(GlobalVariables.Instance.ChosenCompany, GlobalVariables.Instance.ValgteLønPeriode);

            // WTF is going on here!!! Should be so fucking simple. jesus fuck.
            Record recordObj = database.FåRecordByLoggedDate(a, GlobalVariables.Instance.ValgteLønPeriode);
            Fra = recordObj.StartTime;
            Til = recordObj.EndTime;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}