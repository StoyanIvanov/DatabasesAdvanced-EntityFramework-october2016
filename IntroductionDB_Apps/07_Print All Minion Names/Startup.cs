using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07_Print_All_Minion_Names
{
    public class Startup
    {
        static void Main()
        {
            List<string> minions = new List<string>();
            string connectionString = "Server=STOYAN-NOTEBOOK\\SQLSERVER; Database=master; Trusted_Connection=true";
            SqlConnection sqlconnection = new SqlConnection(connectionString);

            sqlconnection.Open();
            using (sqlconnection)
            {
                SqlCommand commandExecutor = new SqlCommand("", sqlconnection);
                commandExecutor.CommandText = "USE MinionsDB SELECT m.Name FROM Minions AS m";
                SqlDataReader dataReader = commandExecutor.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        minions.Add(dataReader[0].ToString());
                    }
                }

                var count = minions.Count - 1;

                for (int i = 0; i <= minions.Count/2; i++)
                {
                    Console.WriteLine(minions[i]);
                    if (count > minions.Count/2)
                    {
                        Console.WriteLine(minions[count]);
                    }
                    
                    count = count - 1;
                }
            }
        }
    }
}
