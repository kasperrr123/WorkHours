using System;
using System.Globalization;
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
            int min = 1;

            for (int i = 0; i < 20; i++)
            {
                var record = new Record
                {
                    StartTime = new TimeSpan(09, min, 00),
                    EndTime = new TimeSpan(17, 15, 00),
                    LoggedDate = new DateTime(2019, 09, 02, 12, min, 00),
                    Pause = "15",
                    LønPeriodeID = 1,

                };
                database.AddRecord(record);
                App.Database.Commit();
                min += 1;

            }
        }

        private void OpretRecordsTHansen()
        {
            int min = 1;
            for (int i = 0; i < 15; i++)
            {
                var record = new Record
                {
                    StartTime = new TimeSpan(08, min, 00),
                    EndTime = new TimeSpan(18, 20, 00),
                    LoggedDate = new DateTime(2019, 09, 02, 13, min, 00),
                    Pause = "35",
                    LønPeriodeID = 2,
                };
                database.AddRecord(record);
                App.Database.Commit();

                min += 2;
            }

            database.AddRecord(new Record
            {
                StartTime = new TimeSpan(08, min, 00),
                EndTime = new TimeSpan(18, 20, 00),
                LoggedDate = new DateTime(2019, 08, 31, 13, min, 00),
                Pause = "35",
                LønPeriodeID = 2,
            });
            database.AddRecord(new Record
            {
                StartTime = new TimeSpan(08, min, 00),
                EndTime = new TimeSpan(18, 20, 00),
                LoggedDate = new DateTime(2019, 08, 30, 13, min, 00),
                Pause = "35",
                LønPeriodeID = 2,
            });
        }

        private void OpretRecordsKinorama()
        {
            int min = 3;
            for (int i = 0; i < 8; i++)
            {
                var record = new Record
                {
                    StartTime = new TimeSpan(12, min, 00),
                    EndTime = new TimeSpan(23, 30, 00),
                    LoggedDate = new DateTime(2019, 09, 02, 14, min, 00),
                    Pause = "120",
                    LønPeriodeID = 3,

                };
                database.AddRecord(record);
                App.Database.Commit();
                min += 2;
            }
        }

        private void OpretLønPerioder()
        {
            DateTime current = System.DateTime.Now;
            LønPeriode lønPeriode_Elgiganten_1;
            LønPeriode lønPeriode_Elgiganten_2;
            LønPeriode lønPeriode_Elgiganten_3;
            LønPeriode lønPeriode_THansen_2;
            LønPeriode lønPeriode_Kinorama_3;
            // Hvis TestData ikke virker ændre denne. Det er pga. dagsdato.
            if (15 >= current.Day)
            {
                lønPeriode_Elgiganten_1 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, current.Month - 1, 15),
                    To = new DateTime(current.Year, (current.Month), 16),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM", new CultureInfo("da-DK")) + " - " + current.ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_THansen_2 = new LønPeriode
                {
                    CompanyName = "T-Hansen A/S",
                    From = new DateTime(current.Year, current.Month - 1, 8),
                    To = new DateTime(current.Year, (current.Month), 9),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM", new CultureInfo("da-DK")) + " - " + current.ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_Kinorama_3 = new LønPeriode
                {
                    CompanyName = "Kinorama A/S",
                    From = new DateTime(current.Year, current.Month - 1, 10),
                    To = new DateTime(current.Year, (current.Month), 11),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM", new CultureInfo("da-DK")) + " - " + current.ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_Elgiganten_2 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, 2, 15),
                    To = new DateTime(current.Year, 3, 16),
                    Year = 2019,
                    Periode = new DateTime(2019, 2, 15).ToString("MMMM", new CultureInfo("da-DK")) + " - " + new DateTime(2019, 3, 16).ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_Elgiganten_3 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, 1, 15),
                    To = new DateTime(current.Year, 2, 16),
                    Year = 2018,
                    Periode = new DateTime(2018, 1, 15).ToString("MMMM", new CultureInfo("da-DK")) + " - " + new DateTime(2018, 2, 16).ToString("MMMM", new CultureInfo("da-DK")),
                };
            }
            else
            {
                lønPeriode_Elgiganten_1 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, current.Month, 15),
                    To = new DateTime(current.Year, (current.Month + 1), 16),
                    Year = current.Year,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM", new CultureInfo("da-DK")) + " - " + current.ToString("MMMM", new CultureInfo("da-DK")),
                };

                lønPeriode_THansen_2 = new LønPeriode
                {
                    CompanyName = "T-Hansen A/S",
                    From = new DateTime(current.Year, current.Month, 8),
                    To = new DateTime(current.Year, (current.Month + 1), 9),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM", new CultureInfo("da-DK")) + " - " + current.ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_Kinorama_3 = new LønPeriode
                {
                    CompanyName = "Kinorama A/S",
                    From = new DateTime(current.Year, current.Month, 10),
                    To = new DateTime(current.Year, (current.Month + 1), 11),
                    Year = 2019,
                    Periode = new DateTime(current.Year, (DateTime.Now.Month - 1), current.Day).ToString("MMMM", new CultureInfo("da-DK")) + " - " + current.ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_Elgiganten_2 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, 2, 15),
                    To = new DateTime(current.Year, 3, 16),
                    Year = current.Year,
                    Periode = new DateTime(2019, 2, 15).ToString("MMMM", new CultureInfo("da-DK")) + " - " + new DateTime(2019, 3, 16).ToString("MMMM", new CultureInfo("da-DK")),
                };
                lønPeriode_Elgiganten_3 = new LønPeriode
                {
                    CompanyName = "Elgiganten A/S",
                    From = new DateTime(current.Year, 2, 15),
                    To = new DateTime(current.Year, 3, 16),
                    Year = 2018,
                    Periode = new DateTime(2018, 2, 15).ToString("MMMM", new CultureInfo("da-DK")) + " - " + new DateTime(2018, 3, 16).ToString("MMMM", new CultureInfo("da-DK")),
                };
            }

            database.TilføjLønPeriode(lønPeriode_Elgiganten_1);
            App.Database.Commit();
            App.Database.Commit();
            database.TilføjLønPeriode(lønPeriode_THansen_2);
            App.Database.Commit();
            database.TilføjLønPeriode(lønPeriode_Kinorama_3);
            App.Database.Commit();
            database.TilføjLønPeriode(lønPeriode_Elgiganten_2);
            App.Database.Commit();
            database.TilføjLønPeriode(lønPeriode_Elgiganten_3);

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
                Tillæg = new System.Collections.Generic.List<Tillæg>
                {
                   new Tillæg()
                   {
                       TypeOfTillæg = "Aften tillæg",
                       AllDay = false,
                       CompanyName = "Elgiganten A/S",
                       TillægKr = "15",
                       From = new TimeSpan(18,00,00),
                   },
                   new Tillæg()
                   {
                       TypeOfTillæg = "Søndags tillæg",
                       AllDay = true,
                       CompanyName = "Elgiganten A/S",
                       TillægKr = "45",
                       From = new TimeSpan(18,00,00),
                   },
                   new Tillæg()
                   {
                       TypeOfTillæg = "Lørdags tillæg",
                       AllDay = false,
                       CompanyName = "Elgiganten A/S",
                       TillægKr = "25",
                       From = new TimeSpan(15,00,00),
                   },

                },
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