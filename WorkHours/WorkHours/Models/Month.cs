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
        [PrimaryKey]
        public string MonthName { get; set; }

        [ForeignKey(typeof(Company))]
        public String CompanyName { get; set; }



    }
}
