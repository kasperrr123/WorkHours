using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkHours.DatabaseStuff
{
    [Table("Company")]
    class TabelCompany
    {
        [PrimaryKey]
        public string CompanyName { get; set; }


    }
}
