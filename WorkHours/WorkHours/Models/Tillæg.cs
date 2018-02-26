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



        [PrimaryKey]
        public string Day { get; set; }

        public String From { get; set; }
        public String To { get; set; }
        public Decimal TillægKr { get; set; }


        [ForeignKey(typeof(Company))]
        public String CompanyName { get; set; }


        public Tillæg()
        {

        }
    }
}
