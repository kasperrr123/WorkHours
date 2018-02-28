using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours.Models
{

    [Table("Day")]

   public  class Record
    {
        [PrimaryKey]
        public String Logged { get; set; }
        public String Date { get; set; }

        public String From { get; set; }

        public Decimal Pause { get; set; }

        [ForeignKey(typeof(Tillæg))]
        public String Day { get; set; }

        [ForeignKey(typeof(Month))]
        public String MonthName { get; set; }

    }
}
