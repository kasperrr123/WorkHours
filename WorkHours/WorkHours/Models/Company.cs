using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLiteNetExtensions.Attributes;

namespace WorkHours.Models
{



    [Table("Company")]
    public class Company
    {



        [PrimaryKey]
        public string CompanyName { get; set; }

        public String TimeLøn { get; set; }

        [OneToMany]
        public List<Tillæg> tillægs { get; set; }

        public Company()
        {
            
        }

    
    }
}
