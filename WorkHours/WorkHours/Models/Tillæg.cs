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

        public bool AllDay { get; set; }

        [ForeignKey(typeof(Company))]
        public string CompanyName { get; set; }

        public String GetToString
        {

            get {

                if (AllDay == true)
                {
                    return "Type: " + TypeOfTillæg + ", Fra: " + "Hele dagen" + ", Tillæg: " + TillægKr;
                }
                else
                {
                    return "Type: " + TypeOfTillæg + ", Fra: " + From.ToString("hh':'mm") +  ", " + "Tillæg: " + TillægKr;
                }
                

            }
        }

        private string getAllDay()
        {
            if (AllDay)
            {
                return "Ja";
            }
            else
            {
                return "Nej";
            }
        }

        public Tillæg()
        {

        }

        public override string ToString()
        {
            return "Type: " + TypeOfTillæg + ", Fra: " + From + ", Kr: " + TillægKr + ", Hele dagen: " + getAllDay();
        }
    }
}
