using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkHours.DatabaseStuff
{
    [Table("User")]
    class TabelUser
    {
        [PrimaryKey]
        public string FullName { get; set; }


    }
}
