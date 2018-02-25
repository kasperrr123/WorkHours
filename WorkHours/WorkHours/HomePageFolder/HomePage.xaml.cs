using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.HomePageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        private DateTime time;

        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                INotifyPropertyChanged();
            }
        }

        public String WelcomeUser { get; set; }
        public HomePage()
        {
            BindingContext = this;

            ThreadStart timer = new ThreadStart(TimerFunction);
            Thread myThread = new Thread(timer);
            myThread.Start();
            WelcomeUser = "Velkommen " + GetUser();
            InitializeComponent();
        }

        public void TimerFunction()
        {
            while (true)
            {
                Time = System.DateTime.Now;
                Thread.Sleep(100);

            }

        }

        private string GetUser()
        {
            var database = App.UserDatabase;
            return database.GetUser().FullName;
        }
    }
}