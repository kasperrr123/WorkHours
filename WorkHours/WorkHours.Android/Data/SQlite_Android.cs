using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using WorkHours.Data;
using WorkHours.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQlite_Android))]


namespace WorkHours.Droid.Data
{
    class SQlite_Android : ISQLite
    {

        public SQlite_Android() { }

        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "WorkHours.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);
            var conn = new SQLite.SQLiteConnection(path);


            return conn;

        }
    }
}