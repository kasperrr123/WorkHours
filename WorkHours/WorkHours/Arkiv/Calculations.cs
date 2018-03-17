using System;
using System.Collections.Generic;
using System.Text;
using WorkHours.Data;
using WorkHours.Models;

namespace WorkHours.Arkiv
{
    class Calculations
    {
        WorkHoursDatabaseController database = App.Database;
        public Calculations()
        {

        }

        public void GetTotalHours()
        {

        }

        internal List<int> GetTotalHours(String company, LønPeriode valgteLønPeriode)
        {
            var records = database.FåRecords(company, valgteLønPeriode);
            if (records != null)
            {
                int WorkHours = 0;
                int WorkMinutes = 0;
                foreach (var item in records)
                {
                    WorkHours += item.EndTime.Subtract(item.StartTime).Hours;
                    WorkMinutes += item.EndTime.Subtract(item.StartTime).Minutes;
                }
                return new List<int> { WorkHours, WorkMinutes };
            }
            else
            {
                return new List<int> { 0, 0 };
            }

        }




        internal List<int> GetTotalBreak(string company, LønPeriode valgteLønPeriode)
        {
            var records = database.FåRecords(company, valgteLønPeriode);
            if (records != null)
            {
                int minutes = 0;
                foreach (var item in records)
                {
                    minutes += int.Parse(item.Pause);

                }
                TimeSpan time = TimeSpan.FromMinutes(minutes);
                return new List<int> { time.Hours, time.Minutes };
            }
            else
            {
                return new List<int> { 0, 0 };
            }
         
        }
    }

}
