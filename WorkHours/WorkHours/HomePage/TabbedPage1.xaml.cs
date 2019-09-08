using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.HomePage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TabbedPage1 : TabbedPage
    {

        private WorkHoursDatabaseController database = App.Database;

        public TabbedPage1()
		{

            NavigationPage.SetHasNavigationBar(this, false);
         
            InitializeComponent ();
		}
    }
}