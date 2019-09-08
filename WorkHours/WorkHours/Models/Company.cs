using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace WorkHours.Models
{
    [Table("Company")]
    public class Company
    {



        [PrimaryKey]
        public string CompanyName { get; set; }

        public String TimeLøn { get; set; }

        public int LønPeriode_FraDato { get; set; }
        public int LønPeriode_TilDato { get; set; }

        public string Color { get; set; }

        [ForeignKey(typeof(User))]
        public String User { get; set; }

        [OneToMany]
        public List<LønPeriode> LønPerioder { get; set; }

        [OneToMany]
        public List<Tillæg> Tillæg { get; set; }

        public Company()
        {

        }

        /// <summary>
        /// If there is a periode for this month it will be returned, otherwise null.
        /// </summary>
        /// <returns>
        /// Lønperiode eller null
        /// </returns>
        public LønPeriode HasCurrentPeriode()
        {
            
            if (App.Database.GetLønPerioder().Find(n => n.Year == System.DateTime.Now.Year && n.CompanyName == CompanyName && n.To.Ticks > DateTime.Now.Ticks) != null)
            {
                return App.Database.GetLønPerioder().Find(n => n.Year == System.DateTime.Now.Year && n.CompanyName == CompanyName && n.To.Ticks > DateTime.Now.Ticks);

            }
            else
            {
                return null;
            }




        }


    }
}
