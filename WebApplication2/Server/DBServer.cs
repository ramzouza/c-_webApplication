using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using WebApplication2.Models;

namespace WebApplication2.Server
{
    public class DBServer
    {
        //string cs = "Data Source=:memory:";

        private static string dbName = System.IO.Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content", "test.db");
        private static string cs = "URI=file:" + dbName;
        public static void TestConenction()
        {
            if (!System.IO.File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);
            }

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Cars";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Cars(Id INTEGER PRIMARY KEY, 
                    Name TEXT, Price INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(1,'Audi',52642)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(2,'Mercedes',57127)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(3,'Skoda',9000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(4,'Volvo',29000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(5,'Bentley',350000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(6,'Citroen',21000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(7,'Hummer',41400)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(8,'Volkswagen',21600)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Car> GetCarsByName(string name)
        {
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = $"SELECT Id, Name, Price FROM Cars WHERE Name like '%{name}%'";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        List<Car> cars = new List<Car>();
                        while (reader.Read())
                        {
                            cars.Add(new Car()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetInt32(2)
                            });
                        }

                        return cars;
                    }
                }
            }

        }
    }

}