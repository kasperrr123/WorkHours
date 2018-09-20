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
        public List<String> ListOfColors { get; set; }
        public SetTimeLøn()
        {
            BindingContext = this;
            ListOfColors = GetColors();
            InitializeComponent();
        }

        private List<string> GetColors()
        {
            return new List<string> { "Red", "Green", "Blue", "Standard" };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            obj.BasisTimeLøn = TimeLøn.Text;
            obj.color = ColorPicker.SelectedItem.ToString();
            Navigation.PushAsync(new FåLønPeriode());


        }
    }
}