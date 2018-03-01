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

        public String LønPeriode_FraDato { get; set; }
        public String LønPeriode_TilDato { get; set; }

        [ForeignKey(typeof(User))]
        public String User { get; set; }


        [OneToMany]
        public List<LønPeriode> LønPerioder { get; set; }

        [OneToMany]
        public List<Tillæg> Tillæg { get; set; }

        public Company()
        {

        }


    }
}
