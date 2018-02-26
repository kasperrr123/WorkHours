using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
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

            database.DropTable<User>();
            database.DropTable<Company>();
            database.DropTable<Month>();
            database.DropTable<Tillæg>();


            database.CreateTable<Tillæg>();
            database.CreateTable<User>();
            database.CreateTable<Company>();
            database.CreateTable<Month>();

         
        }

        // Controllers for TABLE: User.
        public List<User> GetUsers()
        {
            List<User> listOfUsers = new List<User>();
            //return database.Table<TableUser>();
            var query = database.Table<User>();
            foreach (var names in query)
            {
                
                listOfUsers.Add(names);
            }
           
            return listOfUsers;

        }

        public User GetUser()
        {
            return database.Table<User>().First();
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
        public Company GetCompanies()
        {
            return database.Table<Company>().First();
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

    }
}
