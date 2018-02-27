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
        public string Company { get; set; }
        public String From { get; set; }
        public String TillægKr { get; set; }


        public Tillæg()
        {

        }
    }
}
