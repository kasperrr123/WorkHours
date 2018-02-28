using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLiteNetExtensions.Attributes;

namespace WorkHours.Models
{
    [Table("Month")]
    public class Month
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        [ForeignKey(typeof(Company))]
        public String CompanyName { get; set; }

        [OneToMany]
        public Record Record { get; set; }

    }
}
