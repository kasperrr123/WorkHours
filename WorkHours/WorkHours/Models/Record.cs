using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours.Models
{

    [Table("Record")]

   public class Record
    {
        [PrimaryKey]
        public DateTime LoggedDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public String Pause { get; set; }

        [ForeignKey(typeof(Tillæg))]
        public String TypeOfTillæg { get; set; }

        [ForeignKey(typeof(LønPeriode))]
        public int LønPeriodeID { get; set; }


        public Record()
        {

        }

        public override string ToString()
        {
            return LoggedDate + " " + LoggedDate.DayOfWeek + " " + StartTime + " " + EndTime + " " + Pause; 
        }

    }
}
