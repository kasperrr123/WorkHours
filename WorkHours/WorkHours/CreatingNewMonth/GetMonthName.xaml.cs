using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.CreatingNewMonth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetMonthName : ContentPage
    {
        NewMonthObj newMonthObj = NewMonthObj.Instance;
        public List<string> Months { get; set; }
        public int TodaysDate { get; set; }
        public GetMonthName()
        {
            Months = new List<string>();
            string[] a = DateTimeFormatInfo.CurrentInfo.MonthNames;
            foreach (var item in a)
            {
                Months.Add(item);
            }
            TodaysDate = (System.DateTime.Now.Month) - 1;
            BindingContext = this;
            InitializeComponent();
        }
        /*
        private string GetTodaysDate()
        {
            int month = System.DateTime.Now.Month;
            switch (month)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "Marts";
                case 4:
                    return "April";
                case 5:
                    return "Maj";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }

            return null;
        }
        */
        async void Button_Clicked(object sender, EventArgs e)
        {
            //if (picker.SelectedItem == null)
            //{
            //    await DisplayAlert("Hov!", "Vælg gerne en måned", "Ok");
            //}
            //else
            //{
                //newMonthObj.Month = picker.SelectedItem.ToString();
                await Navigation.PushAsync(new SetTimeLøn());
            //}
        }
    }
}
