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


            // Creating tables.
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
                database.CreateTable<Variables>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        // Controllers for TABLE: Records
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

        internal Record FåRecordByLoggedDate(DateTime SelectedRecordLoggedDate, LønPeriode valgteLønPeriode)
        {

            List<Record> list = new List<Record>();
            foreach (var item in database.Table<Record>().Where(n => n.LønPeriodeID == valgteLønPeriode.LønPeriodeID))
            {
                list.Add(item);
            }
            foreach (var record in list)
            {
                String time1 = SelectedRecordLoggedDate.ToString("hh/mm/ss");
                String time2 = record.LoggedDate.ToString("hh/mm/ss");
                if (time1 == time2)
                {
                    return record;
                }
            }
            return null;


        }

        internal List<int> FåÅrstalForFirma(string chosenCompany)
        {
            List<int> år = new List<int>();
            var lønperioder = database.Table<LønPeriode>();
            foreach (var periode in lønperioder)
            {
                if (!år.Contains(periode.Year))
                {
                    år.Add(periode.Year);
                }
            }

            return år;
        }

        internal void ÆndreRecord(Record selectedRecord)
        {
            database.Update(selectedRecord);
        }

        public void SletRecord(Record selectedRecord)
        {
            database.Delete(selectedRecord);
        }

        public LønPeriode GetLønPeriode(int LønPeriodeID)
        {
            if (database.Table<LønPeriode>() != null)
            {
                return database.Table<LønPeriode>().Where(n => n.LønPeriodeID == LønPeriodeID).First();
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
        /// <summary>
        /// Returner en list med alle records for den givende lønperiode.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal List<Record> FåRecordsByPeriode(LønPeriode item)
        {
            try
            {
                var records = database.Table<Record>().Where(n => n.LønPeriodeID == item.LønPeriodeID);
                List<Record> list = new List<Record>();
                foreach (var record in records)
                {
                    list.Add(record);
                }

                return list;
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// Tjekker om det er oprettet en record for dags dato og returner record. Hvis ikke nogen record er fundet bliver null returned.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal bool FåRecordForIdag(LønPeriode item)
        {
            try
            {
                var recordsForPeriod = database.Table<Record>().Where(n => n.LønPeriodeID == item.LønPeriodeID).ToList();
                foreach (var record in recordsForPeriod)
                {
                    if (record.LoggedDate.ToString("dd-MM-yyyy").Equals(DateTime.Now.Date.ToString("dd-MM-yyyy")))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return false;
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
        /// <summary>
        /// Finder alle lønperioder for den valgte arbejdsplads.
        /// </summary>
        /// <param name="arbejdsplads"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Finder alle lønperioder for den valgte arbejdsplads for det valgte år.
        /// </summary>
        /// <param name="arbejdsplads"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<LønPeriode> FåLønPerioderForArbejdsplads(String arbejdsplads, int year)
        {
            if (database.Table<LønPeriode>() != null)
            {
                List<LønPeriode> list = new List<LønPeriode>();
                foreach (var item in database.Table<LønPeriode>().Where(n => n.CompanyName == arbejdsplads && n.Year == year))
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

        // Controller for TABLE: Variables.
        public Variables GetVariables()
        {
            try
            {
                if (database.Table<Variables>().First() == null)
                {
                    return null;
                }
                return database.Table<Variables>().First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                return null;
            }

        }

        public int UpdateVariables(Variables variables)
        {

            try
            {
                database.Update(variables);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public void AddVariable(Variables variables)
        {
            try
            {
                database.Insert(variables);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Other methods
        public void Commit()
        {
            database.Commit();
        }

        internal void AddRecord(Record record)
        {
            database.Insert(record);
        }
    }
}
