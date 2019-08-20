using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours
{
    public class RecordView
    {

        public String OnlyDate { get; set; }

        public string DayOfWeekInDA { get; set; }

        public string FromToString { get; set; }

        public DateTime LoggedDate { get; set; }

        public RecordView(DateTime date, TimeSpan from, TimeSpan to, DateTime loggedDate)
        {
            this.OnlyDate = date.Day + "-" + date.Month + "-" + date.Year;
            this.DayOfWeekInDA = FormatToDA.GetDayOfWeekInDA(date.DayOfWeek.ToString()) + " d.";
            this.FromToString = from.Hours + "." + from.Minutes + "-" + to.Hours + "." + to.Minutes;
            this.LoggedDate = loggedDate;
        }

    }

}



