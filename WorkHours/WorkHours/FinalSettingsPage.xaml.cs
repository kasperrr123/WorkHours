using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHours.Models;

namespace WorkHours
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalSettingsPage : ContentPage
    {
        private static NewMonthObj obj = NewMonthObj.Instance;


        private static SalarySettings salary = SalarySettings.Instance;

        public String AftenTillæg { get; set; } = salary.AftenTillæg.ToString();
        public String LørdagTillæg { get; set; } = salary.LørdagsTillæg.ToString();

        public String BasisLøn { get; set; } = "Timeløn: " + salary.BasisTimeLøn;

        public String SøndagTillæg { get; set; } = salary.SøndagsTillæg.ToString();

        public FinalSettingsPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void YesBtn_Clicked(object sender, EventArgs e)
        {
            User user = new User
            {
                FullName = obj.EmployeeName,
            };
            Company company = new Company
            {
                CompanyName = obj.CompanyName,
            };
            Month month = new Month
            {
                MonthName = obj.Month,
            };

            // Before inserting a new user to the database, we check if there already are one with that name.
            var users = App.UserDatabase.GetUsers();
            var name = users.Find(n=>n.FullName == obj.EmployeeName);
            DisplayAlert("Hov!", name.FullName, "Ok");

            if (name.FullName == obj.EmployeeName)
            {
                DisplayAlert("Hov!", "Det brugernavn er allerede blevet taget.", "Ok");
            }
            else
            {
                App.UserDatabase.AddUser(user);
                App.UserDatabase.AddCompany(company);
                App.UserDatabase.AddMonth(month);

            }
            


        }

        private void NoBtn_Clicked(object sender, EventArgs e)
        {
            List<String> data = new List<string>();
            foreach (var item in App.UserDatabase.GetUsers())
            {
                DisplayAlert("YES", item.FullName + " " + item.Companies, "DAMN");
            }
        }
    }
}