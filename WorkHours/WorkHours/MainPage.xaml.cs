using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.CreatingNewMonth;
using Xamarin.Forms;

namespace WorkHours
{
    public partial class MainPage : ContentPage
    {
        public string WelcomeText { get; set; }
        public DateTime DateTime { get; set; }
        public MainPage()
        {

            this.WelcomeText = "Velkommen til løn programmet";
            this.DateTime = System.DateTime.Now;
            BindingContext = this;
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new GetWorkPlaceName());
        }
    }
}
