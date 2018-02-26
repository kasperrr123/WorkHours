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
    public partial class SetTillæg1 : ContentPage
    {
        CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;

        public SetTillæg1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (AftenTillægKr.Text != null)
            {
                obj.AftenTillæg = new CreateNewWorkPlaceObj.AftenTillægObj(AftenTimePicker.Time, AftenTillægKr.Text);

            }
            else
            {
                obj.AftenTillæg = new CreateNewWorkPlaceObj.AftenTillægObj(AftenTimePicker.Time, "0");

            }
            if (LørdagsTillægKr.Text != null)
            {
                obj.LørdagsTillæg = new CreateNewWorkPlaceObj.LørdagsTillægObj(LørdagTimePicker.Time, LørdagsTillægKr.Text);

            }
            else
            {
                obj.LørdagsTillæg = new CreateNewWorkPlaceObj.LørdagsTillægObj(LørdagTimePicker.Time, "0");

            }



            Navigation.PushAsync(new SetTillæg2());
        }
    }
}