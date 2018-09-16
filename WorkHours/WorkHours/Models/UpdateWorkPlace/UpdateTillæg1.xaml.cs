using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.UpdateWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateTillæg1 : ContentPage
    {
        CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;
        public String Workplace { get; set; }

        public UpdateTillæg1(String workplace)
        {
            this.Workplace = workplace;
            InitializeComponent();
            SetValues(workplace);
        }


        private void SetValues(String workPlace)
        {
            var database = App.Database;
            // Setting the aften tillæg values
            //var company = database.GetCompanies().Find(n => n.CompanyName == workPlace)
            //DisplayAlert("ok", company, "ok");
            //AftenTimePicker.Time = TimeSpan.Parse(company);
            //AftenTillægKr.Text = database.GetCompanies().Find(n => n.CompanyName == workPlace).tillægs.Select(n => n.From).ToString();
            // Setting the lørdags tillæg values.
            //var time1 = database.GetCompanies().Find(n => n.CompanyName == workPlace).tillægs.Where(n => n.Day == "Lørdag").Select(n => n.From).ToString();
            //LørdagTimePicker.Time = TimeSpan.Parse(time1);
            //LørdagsTillægKr.Text = database.GetCompanies().Find(n => n.CompanyName == workPlace).tillægs.Where(n => n.Day == "Lørdag").Select(n => n.TillægKr).ToString();
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



            Navigation.PushAsync(new UpdateTillæg2());
        }
    }
}