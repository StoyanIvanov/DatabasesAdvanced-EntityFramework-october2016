
namespace _05_ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class Startup
    {
        private static List<string> logger = new List<string>();
        static void Main()
        {
            string connectionString = "Server=STOYAN-NOTEBOOK\\SQLSERVER; Database=master; Trusted_Connection=true";
            SqlConnection sqlconnection = new SqlConnection(connectionString);

            sqlconnection.Open();
            using (sqlconnection)
            {
                SqlCommand commandExecutor = new SqlCommand("", sqlconnection);
                var readConsoleLine = Console.ReadLine();

                ChangeTownNames(commandExecutor, readConsoleLine);

                foreach (var line in logger)
                {
                    Console.WriteLine(line);
                }
            }
        }

        private static void ChangeTownNames(SqlCommand sqlCommand, string country)
        {
            sqlCommand.CommandText = "USE MinionsDB " +
                                     "UPDATE Towns " +
                                     "SET Name = UPPER(Name) " +
                                     "WHERE Country = '" + country + "'";

            logger.Add(sqlCommand.ExecuteNonQuery().ToString() + " town names were affected.");

            sqlCommand.CommandText = "SELECT t.Name FROM Towns AS t " +
                                     "WHERE t.Country = '" + country + "'";

            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            if (dataReader.HasRows)
            {
                string cities = "[";

                while (dataReader.Read())
                {
                    cities = cities + string.Format("{0}, ", dataReader[0]);
                }

                dataReader.Close();
                cities = cities.Substring(0, cities.Length - 3) + "]";
                logger.Add(cities);
            }
            else
            {
                Console.WriteLine($"No town names were affected.");
            }

        }
    }
}
