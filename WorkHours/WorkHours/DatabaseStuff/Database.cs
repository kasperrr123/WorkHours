using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace WorkHours.DatabaseStuff
{
    class Database : SQLiteConnection
    {
        public Database(string path) : base(path)
        {
            Initialize();
        }

        void Initialize()
        {
            CreateTable<TabelUser>();
            CreateTable<TabelCompany>();
            CreateTable<TabelMonth>();
        }


        // Controllers for TABLE: User.
        public List<TabelUser> GetUsers()
        {
            return Table<TabelUser>().ToList();
        }

        public TabelUser GetUser(string FullName)
        {
            return Table<TabelUser>().Where(t => t.FullName == FullName).First();
        }

        public int AddUser(TabelUser user)
        {
            return Insert(user);
        }

        public int UpdateUser(TabelUser user)
        {
            return Update(user);
        }

        public void DeleteUser(TabelUser user)
        {
            Delete(user);
        }

        // Controllers for TABLE: Month.
        public List<TabelMonth> GetMonths()
        {
            return Table<TabelMonth>().ToList();
        }

        public TabelMonth GetMonth(string Month)
        {
            return Table<TabelMonth>().Where(t => t.MonthName == Month).First();
        }

        public int AddMonth(TabelMonth month)
        {
            return Insert(month);
        }

        public int UpdateMonth(TabelMonth month)
        {
            return Update(month);
        }

        public void DeleteMonth(TabelMonth month)
        {
            Delete(month);
        }

        // Controllers for TABLE: Company.
        public List<TabelCompany> GetCompanies()
        {
            return Table<TabelCompany>().ToList();
        }

        public TabelCompany GetCompany(string company)
        {
            return Table<TabelCompany>().Where(t => t.CompanyName == Month).First();
        }

        public int AddCompany(TabelCompany company)
        {
            return Insert(company);
        }

        public int UpdateCompany(TabelCompany company)
        {
            return Update(company);
        }

        public void DeleteCompany(TabelCompany company)
        {
            Delete(company);
        }
    }
}
}
