using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using WorkHours.Data;
using WorkHours.iOS.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]


namespace WorkHours.iOS.Data
{
    class SQLite_IOS : ISQLite
    {

        public SQLite_IOS() { }

        public SQLiteConnection GetConnection()
        {
            var fileName = "WorkHours.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var conn = new SQLite.SQLiteConnection(path);


            return conn;

        }


    }
}