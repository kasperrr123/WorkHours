using System;
using WorkHours.Data;
using WorkHours.Models;

namespace WorkHours
{
    internal class TestData
    {


        private WorkHoursDatabaseController database = App.Database;
        public LønPeriode LønPeriode { get; set; }
        public User user { get; set; }

        public TestData(User newUser)
        {
            this.user = newUser;
            OpretArbejdsPladser();
            OpretLønPerioder();
            OpretRecordsElgiganten();
            OpretRecordsTHansen();
            OpretRecordsKinorama();

            if (App.Database.GetVariables() == null)
            {
                try
                {
                    App.Database.AddVariable(new Variables
                    {
                        ID = 1,
                        CurrentCompany = App.Database.GetCompanies()[0].CompanyName,
                        
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void OpretRecordsElgiganten()
        {

            for (int i = 0; i < 20; i++)
            {
                var record = new Record
                {
                    StartTime = new TimeSpan(09, 45, 00),
                    EndTime = new TimeSpan(17, 15, 00),
                    LoggedDate = DateTime.Now.AddMinutes(1),
                    Pause = "15",
                    LønPeriodeID = 1,

                };
                database.AddRecord(record);
                App.Database.Commit();
            }
        }

        private void OpretRecordsTHansen()
        {
            for (int i = 0; i < 20; i++)
            {
                var record = new Record
                {
                    StartTime = new TimeSpan(08, 45, 00),
                    EndTime = new TimeSpan(18, 20, 00),
                    LoggedDate = DateTime.Now.AddMinutes(1),
                    Pause = "35",
                    LønPeriodeID = 2,
                };
                database.AddRecord(record);
                App.Database.Commit();
            }
        }

        private void OpretRecordsKinorama()
        {
            for (int i = 0; i < 25; i++)
            {
                var record = new Record
                {
                    StartTime = new TimeSpan(12, 00, 00),
                    EndTime = new TimeSpan(23, 30, 00),
                    LoggedDate = DateTime.Now.AddMinutes(1),
                    Pause = "120",
                    LønPeriodeID = 3,

                };
                database.AddRecord(record);
                App.Database.Commit();
            }
        }

        private void OpretLønPerioder()
        {
            DateTime current = System.DateTime.Now;
            LønPeriode lønPeriode1;
            LønPeriode lønPeriode2;
            LønPeriode lønPeriode3;
            // Hvis TestData ikke virker ændre denne. Det er pga. dagsdato.
            if (15 >= current.Day)
            {
                lønPeriode1 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, current.Month - 1, 15),
                    To = new DateTime(current.Year, (current.Month), 16),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };
                lønPeriode2 = new LønPeriode
                {
                    CompanyName = "T-Hansen A/S",
                    From = new DateTime(current.Year, current.Month - 1, 8),
                    To = new DateTime(current.Year, (current.Month), 9),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };
                lønPeriode3 = new LønPeriode
                {
                    CompanyName = "Kinorama A/S",
                    From = new DateTime(current.Year, current.Month - 1, 10),
                    To = new DateTime(current.Year, (current.Month), 11),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };
            }
            else
            {
                lønPeriode1 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, current.Month, 15),
                    To = new DateTime(current.Year, (current.Month + 1), 16),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };
                lønPeriode2 = new LønPeriode
                {
                    CompanyName = "T-Hansen A/S",
                    From = new DateTime(current.Year, current.Month, 8),
                    To = new DateTime(current.Year, (current.Month + 1), 9),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };
                lønPeriode3 = new LønPeriode
                {
                    CompanyName = "Kinorama A/S",
                    From = new DateTime(current.Year, current.Month, 10),
                    To = new DateTime(current.Year, (current.Month + 1), 11),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM") + " - " + current.ToString("MMMM"),
                };
            }

            database.TilføjLønPeriode(lønPeriode1);
            App.Database.Commit();
            database.TilføjLønPeriode(lønPeriode2);
            App.Database.Commit();
            database.TilføjLønPeriode(lønPeriode3);
            App.Database.Commit();



        }

        private void OpretArbejdsPladser()
        {
            Company company1 = new Company
            {
                CompanyName = "Elgiganten A/S",
                TimeLøn = "121.5",
                User = user.FullName,
                LønPeriode_FraDato = 15,
                LønPeriode_TilDato = 16,
                Color = "Green",
            };
            Company company2 = new Company
            {
                CompanyName = "T-Hansen A/S",
                TimeLøn = "119.5",
                User = user.FullName,
                LønPeriode_FraDato = 8,
                LønPeriode_TilDato = 9,
                Color = "Blue",
            };
            Company company3 = new Company
            {
                CompanyName = "Kinorama A/S",
                TimeLøn = "100.5",
                User = user.FullName,
                LønPeriode_FraDato = 10,
                LønPeriode_TilDato = 11,
                Color = "Red",
            };
            database.AddCompany(company1);
            App.Database.Commit();
            database.AddCompany(company2);
            App.Database.Commit();
            database.AddCompany(company3);
            App.Database.Commit();

        }
    }
}