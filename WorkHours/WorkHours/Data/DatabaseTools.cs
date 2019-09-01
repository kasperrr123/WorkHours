using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using WorkHours.Models;
using Xamarin.Forms;

namespace WorkHours.Data
{
    class DatabaseTools
    {
        SQLiteConnection database;


        public DatabaseTools()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
        }
        public void ResetDatabase( )
        {
            try
            {
                database.DropTable<Company>();
                database.DropTable<LønPeriode>();
                database.DropTable<Tillæg>();
                database.DropTable<Record>();
                database.DropTable<User>();

                database.CreateTable<User>();
                database.CreateTable<Company>();
                database.CreateTable<Tillæg>();
                database.CreateTable<LønPeriode>();
                database.CreateTable<Record>();

            }
            catch (Exception x)
            {
                Console.WriteLine("ERROR: " + x.Message);

            }
        }
    }
}
