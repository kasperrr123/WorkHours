using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using WorkHours.Droid.Data;
using WorkHours.Models;
using Xamarin.Forms;
namespace WorkHours.Data
{
    public class WorkHoursDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public WorkHoursDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<Tillæg>();
            database.CreateTable<User>();
            database.CreateTable<Company>();
            database.CreateTable<Month>();
         
        }

  

        // Controllers for TABLE: User.

        public User GetUser()
        {
            try
            {
                if (database.Table<User>().First() == null)
                {
                    return null;
                }
                return database.Table<User>().First();
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public int AddUser(User user)
        {
            return database.Insert(user);
        }

        public int UpdateUser(User user)
        {
            return database.Update(user);
        }

        public void DeleteUser(User user)
        {
            database.Delete(user);
        }

        // Controllers for TABLE: Month.
        public Month GetMonths()
        {
            return database.Table<Month>().First();
        }

        public Month GetMonth(string Month)
        {
            return database.Table<Month>().Where(t => t.MonthName == Month).First();
        }

        public int AddMonth(Month month)
        {
            return database.Insert(month);
        }

        public int UpdateMonth(Month month)
        {
            return database.Update(month);
        }

    

        public void DeleteMonth(Month month)
        {
            database.Delete(month);
        }

        // Controllers for TABLE: Company.
        public List<Company> GetCompanies()
        {
            List<Company> list = new List<Company>();
            foreach (var item in database.Table<Company>())
            {
                list.Add(item);
            }

            return list;
        }

        public Company GetCompany(string company)
        {
            return database.Table<Company>().Where(t => t.CompanyName == company).First();
        }

        public int AddCompany(Company company)
        {
            return database.Insert(company);
        }

        public int UpdateCompany(Company company)
        {
            return database.Update(company);
        }

        public void DeleteCompany(Company company)
        {
            database.Delete(company);
        }
        // Controller for TABLE: Tillæg.
        public int AddTillæg(Tillæg tillæg)
        {
            return database.Insert(tillæg);
        }
        public ObservableCollection<Tillæg> getTillægs()
        {
            ObservableCollection<Tillæg> list = new ObservableCollection<Tillæg>();
            foreach (var item in database.Table<Tillæg>())
            {
                list.Add(item);
            }
            return list;
        }

        // Other methods
        public void Commit()
        {
            database.Commit();
        }

        internal void DeleteDatabase()
        {
            try
            {
                database.DeleteAll<Company>();
                database.DeleteAll<Month>();
                database.DeleteAll<Tillæg>();
                database.DeleteAll<User>();

            }
            catch (Exception x)
            {
                Console.WriteLine("ERROR: " + x.Message);
            
            }

           
        }

    }
}
