using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.CreateNewWorkPlace
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetTillæg : ContentPage
	{
        public List<String> ListOfTillæg { get; set; }
        public SetTillæg ()
		{
            BindingContext = this;
			InitializeComponent ();
		}
	}
}