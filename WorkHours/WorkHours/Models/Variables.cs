using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours.Models
{

    [Table("Variables")]
    public class Variables
    {
        [PrimaryKey]
        public int ID { get; set; }
        public String CurrentCompany { get; set; }

        public string LønPeriodeNavn { get; set; }
        public string LønPeriodeID { get; set; }

        public int LønPeriode_GårFraDag { get; set; }

        public int LønPeriode_GårTilDag { get; set; }

        public Variables()
        {

        }
    }
}
