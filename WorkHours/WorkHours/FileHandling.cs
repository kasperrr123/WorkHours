using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace WorkHours
{
    class FileHandling
    {
        private string Path { get; set; }
        private string FileName { get; set; }
        private WorkHours.Data.WorkHoursDatabaseController Database;

        public FileHandling()
        {
            this.Database = new Data.WorkHoursDatabaseController();
        }
        /// <summary>
        /// Creates a txt file with all records from the given lønperiode. 
        /// </summary>
        /// <param name="fileName">The name you want to give the file</param>
        /// <param name="lønPeriode">The lønperiode on which you want to have records writed</param>
        /// <returns>Returns a full document path to the file</returns>
        public string WriteSpecifikLønPeriode(string fileName, string lønPeriode)
        {
#if __ANDROID__


            string documentPath = global::Android.OS.Environment.ExternalStorageDirectory.Path + "/" + global::Android.OS.Environment.DirectoryDownloads + "/" + fileName;
            int currentWeek = 0;
            try
            {
                using (var tw = new StreamWriter(documentPath))
                {
                    foreach (var item in Database.FåRecordsByPeriode(lønPeriode, DateTime.Now.Year))
                    {
                        if (currentWeek == 0 )
                        {
                            currentWeek = GetIso8601WeekOfYear(new DateTime(item.LoggedDate.Ticks));
                            tw.WriteLine("Uge " + currentWeek);
                        }
                        tw.WriteLine(item.ToString());
                        if (GetIso8601WeekOfYear(new DateTime(item.LoggedDate.Ticks)) > currentWeek)
                        {
                            currentWeek = GetIso8601WeekOfYear(new DateTime(item.LoggedDate.Ticks));
                            tw.WriteLine("Uge " + currentWeek);

                        }


                    }
                };
                Toast.MakeText(Xamarin.Forms.Forms.Context, "File has been created and save under your download folder", ToastLength.Long).Show();
            }
            catch (Exception)
            {

            }

            return documentPath;
#endif
        }
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
