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
    public partial class SetTillæg1 : ContentPage
    {
        SalarySettings salary = SalarySettings.Instance;
        public SetTillæg1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (AftenTillægKr.Text != null)
            {
                salary.AftenTillæg = new SalarySettings.AftenTillægObj(AftenTimePicker.Time, AftenTillægKr.Text);

            }
            else
            {
                salary.AftenTillæg = new SalarySettings.AftenTillægObj(AftenTimePicker.Time, "0");

            }
            if (LørdagsTillægKr.Text != null)
            {
                salary.LørdagsTillæg = new SalarySettings.LørdagsTillægObj(LørdagTimePicker.Time, LørdagsTillægKr.Text);

            }
            else
            {
                salary.LørdagsTillæg = new SalarySettings.LørdagsTillægObj(LørdagTimePicker.Time, "0");

            }



            Navigation.PushAsync(new SetTillæg2());
        }
    }
}