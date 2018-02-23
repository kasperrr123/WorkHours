using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetTimeLøn : ContentPage
    {
        public static NewMonthObj obj = NewMonthObj.Instance;
        public static SalarySettings SalarySettings = SalarySettings.Instance;
        public String EmployeeName { get; set; } = obj.EmployeeName;
        public String CompanyName { get; set; } = obj.CompanyName;
        public String Month { get; set; } = obj.Month;

        public SetTimeLøn()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            SalarySettings.BasisTimeLøn = TimeLøn.Text;
            Navigation.PushAsync(new SetTillæg1());


        }
    }
}