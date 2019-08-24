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
            this.OnlyDate = date.ToString("dd/MM/yyyy");
            this.DayOfWeekInDA = FormatToDA.GetDayOfWeekInDA(date.DayOfWeek.ToString()) + " d.";
            this.FromToString = from.ToString("hh':'mm") + "-" + to.ToString("hh':'mm");
            this.LoggedDate = loggedDate;
        }

    }

}



