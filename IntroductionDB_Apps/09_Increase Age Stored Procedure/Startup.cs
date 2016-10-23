using System;
using System.Data.SqlClient;
using System.Globalization;

namespace _09_Increase_Age_Stored_Procedure
{
    public class Startup
    {
        static void Main()
        {
            string connectionString = Properties.Settings.Default.Connection;
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            var consoleInputID = Console.ReadLine();

            sqlconnection.Open();
            using (sqlconnection)
            {
                SqlCommand commandExecutor = new SqlCommand("", sqlconnection);
                commandExecutor.CommandText = "EXEC usp_GetOlder @MinionID = " + consoleInputID;
                commandExecutor.ExecuteNonQuery();

                commandExecutor.CommandText = "SELECT m.Name, m.Age " +
                                              "FROM Minions AS m " +
                                              "WHERE m.MinionID = " + consoleInputID;

                //Get wanted Minion and print on the console
                SqlDataReader dataReader = commandExecutor.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"{dataReader[0]} {dataReader[1]}");
                    }
                    dataReader.Close();
                }

            }
        }
    }
}
