using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkHours.DatabaseStuff
{
    [Table("Month")]
    class TabelMonth
    {
        [PrimaryKey]
        public string MonthName { get; set; }


    }
}
