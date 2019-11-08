using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace Assignment_UWP_T808A_026.Utils
{
    public class SQLiteDemo
    {
        private void LoadDatabase1()
        {
            // Get a reference to the SQLite database    
            var conn = new SQLiteConnection("sqlitepcl.db");
            string sql =
                @"CREATE TABLE IF NOT EXISTS Customer (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,Name VARCHAR( 140 ), City VARCHAR( 140 ), Contact VARCHAR( 140 ) );";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void LoadDatabase()
        {
            // Get a reference to the SQLite database
            var conn = new SQLiteConnection("sqlitepcl.db");
            string sql =
                @"CREATE TABLE IF NOT EXISTS Customer (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,Name VARCHAR( 140 ),City VARCHAR( 140 ),Contact VARCHAR( 140 ));";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }

            try
            {
                using (var custstmt = conn.Prepare("INSERT INTO Customer (Name, City, Contact) VALUES (?, ?, ?)"))
                {
                    custstmt.Bind(1, "Hung");
                    custstmt.Bind(2, "hanoi");
                    custstmt.Bind(3, "alo");
                    custstmt.Step();
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
            }
        }
    }
}