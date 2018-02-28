using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLiteNetExtensions.Attributes;

namespace WorkHours.Models
{

   


    [Table("User")]
    public class User
    {
        [PrimaryKey]
        public String FullName { get; set; }


        [OneToMany]
        public List<Company> Companies { get; set; }

        public User()
        {
        
        }
    }
}
