using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHours.UpdateWorkPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateWorkPlaceNameAndTimeLøn : ContentPage
    {
        public static CreateNewWorkPlaceObj obj = CreateNewWorkPlaceObj.Instance;
        public String Workplace { get; set; }
        public UpdateWorkPlaceNameAndTimeLøn(String workPlace)
        {
            this.Workplace = workPlace;
            BindingContext = this;
            InitializeComponent();

            SetValues();
        }

        private void SetValues()
        {
            var database = App.Database;
            TimeLøn.Text = database.GetCompanies().Find(n => n.CompanyName == Workplace).TimeLøn;
            NameInput.Text = database.GetCompanies().Find(n => n.CompanyName == Workplace).CompanyName;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            obj.CompanyName = NameInput.Text;
            obj.BasisTimeLøn = TimeLøn.Text;
            Navigation.PushAsync(new UpdateTillæg1(Workplace));


        }
    }
}