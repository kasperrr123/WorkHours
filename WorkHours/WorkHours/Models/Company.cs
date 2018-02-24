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



        [ForeignKey(typeof(User))]
        public String FullName { get; set; }

        [ManyToOne]
        public User User { get; set; }



        public Company()
        {
            
        }
    }
}
