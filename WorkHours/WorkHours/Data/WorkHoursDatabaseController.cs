using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            // Dropping the tables.
            try
            {
                //database.DropTable<Tillæg>();
                //database.DropTable<User>();
                //database.DropTable<Record>();
                //database.DropTable<Company>();
                //database.DropTable<LønPeriode>();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            // Creating tables.
            try
            {
                database.CreateTable<Tillæg>();
                database.CreateTable<User>();
                database.CreateTable<Record>();
                database.CreateTable<Company>();
                database.CreateTable<LønPeriode>();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }

        public List<Record> FåRecords(string chosenCompany, LønPeriode valgteLønPeriode)
        {
            try
            {
                List<Record> list = new List<Record>();
                foreach (var item in database.Table<Record>().Where(n => n.LønPeriodeID == valgteLønPeriode.LønPeriodeID))
                {
                    list.Add(item);
                }

                return list;
            }
            catch (Exception)
            {

                return null;
            }


        }

        internal Record FåRecordByLoggedDate(DateTime loggedDate, LønPeriode valgteLønPeriode)
        {

            List<Record> list = new List<Record>();
            foreach (var item in database.Table<Record>().Where(n => n.LønPeriodeID == valgteLønPeriode.LønPeriodeID))
            {
                list.Add(item);
            }
            foreach (var periode in list)
            {
                String time1 = loggedDate.ToString("hh/mm/ss");
                String time2 = periode.LoggedDate.ToString("hh/mm/ss");
                if (time1 == time2)
                {
                    return periode;
                }
            }
            return null;


        }

        internal void ÆndreRecord(Record selectedRecord)
        {
            database.Update(selectedRecord);
        }

        public void SletRecord(Record selectedRecord)
        {
            database.Delete(selectedRecord);
        }

        public LønPeriode GetLønPeriode(string LønPeriodeName, int year)
        {
            if (database.Table<LønPeriode>() != null)
            {
                return database.Table<LønPeriode>().Where(n => n.Periode == LønPeriodeName && n.Year == year).First();
            }
            else
            {
                return null;
            }
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

        public int TilføjLønPeriode(LønPeriode lønPeriode)
        {
            return database.Insert(lønPeriode);

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
        public LønPeriode GetMonths()
        {
            return database.Table<LønPeriode>().First();
        }

        public LønPeriode GetMonth(string Month)
        {
            return null;
        }

        public int AddMonth(LønPeriode month)
        {
            return database.Insert(month);
        }

        public int UpdateMonth(LønPeriode month)
        {
            return database.Update(month);
        }



        public void DeleteMonth(LønPeriode month)
        {
            database.Delete(month);
        }

        internal List<Record> FåRecordsByPeriode(String selectedItem, int year)
        {
            try
            {
                var lønPeriode = database.Table<LønPeriode>().Where(n => n.Periode == selectedItem).Where(n => n.Year == year).First();

                var records = database.Table<Record>().Where(n => n.LønPeriodeID == lønPeriode.LønPeriodeID);
                List<Record> list = new List<Record>();
                foreach (var item in records)
                {
                    list.Add(item);
                }

                return list;
            }
            catch (Exception)
            {

                return null;
            }
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
            try
            {
                return database.Table<Company>().Where(t => t.CompanyName == company).First();

            }
            catch (Exception)
            {

                return null;
            }
        }

        public int AddCompany(Company company)
        {
            return database.Insert(company);
        }

        public List<LønPeriode> FåLønPerioderForArbejdsplads(String arbejdsplads)
        {
            if (database.Table<LønPeriode>() != null)
            {
                List<LønPeriode> list = new List<LønPeriode>();
                foreach (var item in database.Table<LønPeriode>().Where(n => n.CompanyName == arbejdsplads))
                {
                    list.Add(item);
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        public Record FindesDerRecordForDagsDato(LønPeriode lønperiode, DateTime TodaysDate)
        {
            try
            {

                if (database.Table<Record>() != null)
                {
                    var records = database.Table<Record>().Where(n => n.LønPeriodeID == lønperiode.LønPeriodeID);
                    foreach (var item in records)
                    {
                        if (item.LoggedDate.Day == TodaysDate.Day && item.LoggedDate.Month == TodaysDate.Month && item.LoggedDate.Year == TodaysDate.Year)
                        {
                            return item;
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
               
            }
        }

        public List<LønPeriode> GetLønPerioder()
        {

            List<LønPeriode> list = new List<LønPeriode>();
            foreach (var item in database.Table<LønPeriode>())
            {
                list.Add(item);
            }

            return list;
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
                database.DeleteAll<LønPeriode>();
                database.DeleteAll<Tillæg>();
                database.DeleteAll<Record>();
                database.DeleteAll<User>();

            }
            catch (Exception x)
            {
                Console.WriteLine("ERROR: " + x.Message);

            }


        }

        internal void AddRecord(Record record)
        {
            database.Insert(record);
        }
    }
}
