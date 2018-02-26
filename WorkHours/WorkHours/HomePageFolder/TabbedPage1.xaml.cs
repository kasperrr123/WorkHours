using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.HomePageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TabbedPage1 : TabbedPage
    {
		public TabbedPage1()
		{
            NavigationPage.SetHasNavigationBar(this, false);
         
            InitializeComponent ();
		}




    }
}