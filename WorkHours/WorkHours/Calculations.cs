using System;
using System.Collections.Generic;
using System.Text;
using WorkHours.Data;
using WorkHours.Models;

namespace WorkHours
{
    public class Calculations
    {
        WorkHoursDatabaseController database = App.Database;
        public Calculations()
        {

        }

        public List<int> GetTotalHours(LønPeriode valgteLønPeriode)
        {
            var records = database.FåRecordsByPeriode(valgteLønPeriode);
            if (records != null)
            {
                int WorkHours = 0;
                int WorkMinutes = 0;
                foreach (var item in records)
                {
                    WorkHours += item.EndTime.Subtract(item.StartTime).Hours;
                   
                    WorkMinutes += item.EndTime.Subtract(item.StartTime).Minutes;
                    if (WorkMinutes > 60)
                    {
                        double a = (WorkMinutes / 60.0);
                        int hoursToAdd = (int)a;
                        WorkMinutes = WorkMinutes - (hoursToAdd * 60);
                        WorkHours += hoursToAdd;
                    }
                }
               
                return new List<int> { WorkHours, WorkMinutes };
            }
            else
            {
                return new List<int> { 0, 0 };
            }

        }




        public List<int> GetTotalBreak(LønPeriode valgteLønPeriode)
        {
            var records = database.FåRecordsByPeriode(valgteLønPeriode);
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
