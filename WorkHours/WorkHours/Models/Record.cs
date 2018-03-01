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
        public String LoggedDate { get; set; }

        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String Pause { get; set; }

        [ForeignKey(typeof(Tillæg))]
        public String TypeOfTillæg { get; set; }

        [ForeignKey(typeof(LønPeriode))]
        public int LønPeriodeID { get; set; }


        public Record()
        {

        }

    }
}
