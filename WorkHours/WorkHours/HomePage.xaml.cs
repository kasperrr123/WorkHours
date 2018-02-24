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
    public partial class HomePage : ContentPage
    {

        public String WelcomeUser { get; set; }
        public HomePage()
        {
            BindingContext = this;
            WelcomeUser = "Velkommen " + GetUser();
            InitializeComponent();
        }

        private string GetUser()
        {
            var database = App.UserDatabase;
            return database.GetUser().FullName;
        }
    }
}