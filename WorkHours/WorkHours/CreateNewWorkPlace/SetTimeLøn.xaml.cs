using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.CreateNewWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetTimeLøn : ContentPage
    {
        public static CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;

        public SetTimeLøn()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            obj.BasisTimeLøn = TimeLøn.Text;
            Navigation.PushAsync(new LønPeriode());


        }
    }
}