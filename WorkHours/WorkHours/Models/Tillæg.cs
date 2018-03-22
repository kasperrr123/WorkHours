using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLiteNetExtensions.Attributes;

namespace WorkHours.Models
{
    
    [Table("Tillæg")]
    public class Tillæg
    {
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public String TypeOfTillæg { get; set; }

        public TimeSpan From { get; set; }
        public String TillægKr { get; set; }

        [ForeignKey (typeof(Company))]
        public string CompanyName { get; set; }

        private String getToString;

        public String GetToString
        {
            get { return TypeOfTillæg + ", " + From + ", " + TillægKr; }
            //set { getToString = value; }
        }


        public Tillæg()
        {

        }

        public override string ToString()
        {
            return TypeOfTillæg + ", " + From + ", " + TillægKr;
        }
    }
}
