using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FinalSettingsPage : ContentPage
	{
        private static SalarySettings salary = SalarySettings.Instance;

        public String AftenTillæg { get; set; } = salary.AftenTillæg.ToString();
        public String LørdagTillæg { get; set; } = salary.LørdagsTillæg.ToString();

        public String BasisLøn { get; set; } = "Timeløn: " + salary.BasisTimeLøn;

        public String SøndagTillæg { get; set; } = salary.SøndagsTillæg.ToString();
        
        public FinalSettingsPage()
		{
            BindingContext = this;
			InitializeComponent ();
		}
	}
}