﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkHours.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WorkHours.Arkiv
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArkivPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private List<Period> listOfRecords;

        public List<Period> ListOfRecords
        {
            get { return listOfRecords; }
            set
            {
                listOfRecords = value;
                INotifyPropertyChanged();
            }
        }

        public ArkivPage()
        {
            BindingContext = this;
            FåPerioder();
            InitializeComponent();
        }

        public void FåPerioder()
        {
            List<Period> list = new List<Period>();
            if (GlobalVariables.Instance.ValgteLønPeriode != null)
            {
                foreach (var item in App.Database.FåRecords(GlobalVariables.Instance.ChosenCompany, GlobalVariables.Instance.ValgteLønPeriode))
                {
                    list.Add(new Period(item.LoggedDate, item.StartTime, item.EndTime));
                }

                ListOfRecords = list;
            }

        }

        protected override void OnAppearing()
        {
            FåPerioder();
            Console.WriteLine("Just entered");
        }


    }

    public struct Period
    {
        public DateTime Date { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public Period(DateTime date, TimeSpan from, TimeSpan to)
        {
            this.Date = date;
            this.From = from;
            this.To = to;
        }

        public override string ToString()
        {
            return Date + "/" + From + "/" + To;
        }

    }

}