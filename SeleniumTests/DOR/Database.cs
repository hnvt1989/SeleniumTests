using System;
using System.Data.SqlClient;
using NUnit.Framework;

namespace CUST2095.WebTest.DTO
{
    public class j6Database
    {
        private string _SqlServer;
        private string _SqlDatabase;
        private string _SqlUser;
        private string _SqlPassword;

        public j6Database()
        {
        }

        public string SqlServer { get { return _SqlServer; } set { _SqlServer = value; } }
        public string SqlDatabase { get { return _SqlDatabase; } set { _SqlDatabase = value; } }
        public string SqlUser { get { return _SqlUser; } set { _SqlUser = value; } }
        public string SqlPassword { get { return _SqlPassword; } set { _SqlPassword = value; } }


        public void ReadFromDatabase()
        {
            //try
            //{
                SqlCommand comm = new SqlCommand();
                comm.Connection = new SqlConnection(
                   "Server=(local);"
                  + "Database=QA_753_J6SQL_5_9_2013;"
                  + "Trusted_Connection=Yes;");
                String sql = @"Select description,Active from SalesOrder.Product where Code = '101'";
                comm.CommandText = sql;
                comm.Connection.Open();
                SqlDataReader cursor = comm.ExecuteReader();
                
                while (cursor.Read())
                    Assert.AreEqual("hey", cursor["description"]);
                    //Console.WriteLine(cursor["name"] + "\t" +
                      //                cursor["population"]);
                comm.Connection.Close();
            //}
            //catch (Exception e)
            //{
              //  Console.WriteLine(e.ToString());
           // }

        }
    }
}
