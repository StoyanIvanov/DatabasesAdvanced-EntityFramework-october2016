using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace _08_IncreaseMinionsAge
{
    public class Startup
    {
        static void Main()
        {
            List<string> minionsNames = new List<string>();
            List<string> minionsIDs = new List<string>();
            string connectionString = Properties.Settings.Default.Connection;
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            var consoleInputIDs = Console.ReadLine().Trim().Split(new []{' '},StringSplitOptions.RemoveEmptyEntries);

            sqlconnection.Open();
            using (sqlconnection)
            {
                SqlCommand commandExecutor = new SqlCommand("", sqlconnection);
                commandExecutor.CommandText = "SELECT m.MinionID, m.Name " +
                                              "FROM Minions AS m " +
                                              "WHERe m.MinionID = "+ string.Join("OR", consoleInputIDs);

                //Get wanted Minions and fill data into List's
                SqlDataReader dataReader = commandExecutor.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        minionsIDs.Add(dataReader[0].ToString());
                        minionsNames.Add(dataReader[1].ToString());
                    }
                    dataReader.Close();
                }

                //Change the names to title case
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                for (int i = 0; i < minionsIDs.Count; i++)
                {
                    minionsNames[i] = textInfo.ToTitleCase(minionsNames[i]);
                    commandExecutor.CommandText = "SET Name = '"+ minionsNames[i] + "' " +
                                                  "WHERE MinionID = "+ minionsIDs[i];
                    commandExecutor.ExecuteNonQuery();
                }

                //Get updated Minions data and print its to the console
                commandExecutor.CommandText = "SELECT m.Name, m.Age " +
                                              "FROM Minions AS m " +
                                              "WHERe m.MinionID = "+ string.Join("OR", minionsIDs);
                dataReader = commandExecutor.ExecuteReader();
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
