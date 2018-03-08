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
        private GlobalVariables global = GlobalVariables.Instance;


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
            Console.WriteLine(global.ValgteLønPeriode.From);
            Console.WriteLine(global.ValgteLønPeriode.To);
            Console.WriteLine(global.ValgteLønPeriode.CompanyName);

            // WTF is going on here!!! Should be so fucking simple. jesus fuck.
            Record recordObj = database.FåRecordByLoggedDate(a, global.ValgteLønPeriode);
            Fra = recordObj.StartTime;
            Til = recordObj.EndTime;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}