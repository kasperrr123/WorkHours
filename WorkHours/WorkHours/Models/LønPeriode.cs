using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLiteNetExtensions.Attributes;

namespace WorkHours.Models
{
    [Table("LønPeriode")]
    public class LønPeriode
    {
        [PrimaryKey, AutoIncrement]
        public int LønPeriodeID { get; set; }
        public String Periode { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Year { get; set; }

        [ForeignKey(typeof(Company))]
        public String CompanyName { get; set; }

        [OneToMany]
        public List<Record> Records { get; set; }


        public LønPeriode()
        {

        }

        public bool HasRecordForToday(LønPeriode lønPeriode)
        {
            if (App.Database.FåRecordForIdag(lønPeriode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
