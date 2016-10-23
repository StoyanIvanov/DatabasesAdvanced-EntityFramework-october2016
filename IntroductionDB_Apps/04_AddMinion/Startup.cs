using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace _04_AddMinion
{
    public class Startup
    {
        private static List<string> logger = new List<string>();
        private static string villainID;
        private static string Namevillain = "";
        private static string city;
        private static string minionID;
        static void Main()
        {
            string connectionString = Properties.Settings.Default.Connection;
            SqlConnection sqlconnection = new SqlConnection(connectionString);

            sqlconnection.Open();
            using (sqlconnection)
            {
                SqlCommand commandExecutor = new SqlCommand("", sqlconnection);
                var firstLine = Console.ReadLine().Trim().Split(' ');
                var secondLine = Console.ReadLine().Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                AddCity(commandExecutor, firstLine[3]);
                AddVillain(commandExecutor, secondLine[1]);
                AddMinion(commandExecutor, firstLine[1], firstLine[2]);

                foreach (var line in logger)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static void AddCity(SqlCommand commandExecutor, string town)
        {
            commandExecutor.CommandText = "SELECT t.TownID FROM Towns AS t " +
                                          "WHERE t.Name = '" + town + "'";
            SqlDataReader dataReader = commandExecutor.ExecuteReader();

            if (!dataReader.HasRows)
            {
                dataReader.Close();
                commandExecutor.CommandText = string.Format("INSERT INTO Towns (Name, Country) " +
                                                            "VALUES ('{0}', ' ')", town);
                commandExecutor.ExecuteNonQuery();

                commandExecutor.CommandText = "SELECT TOP 1 t.TownID FROM Towns AS t " +
                                              "ORDER BY t.TownID DESC";
                dataReader = commandExecutor.ExecuteReader();
                dataReader.Read();
                city = dataReader[0].ToString();
                dataReader.Close();
                logger.Add($"Town {town} was added to the database.");

            }
            else
            {
                dataReader.Read();
                city = dataReader[0].ToString();
                dataReader.Close();
            }
        }

        public static void AddVillain(SqlCommand commandExecutor, string villainName)
        {

            commandExecutor.CommandText = "SELECT v.VillainID, v.Name FROM Villains AS v " +
                                          "WHERE v.Name = '" + villainName + "'";
            SqlDataReader dataReader = commandExecutor.ExecuteReader();
            
            if (!dataReader.HasRows)
            {
                dataReader.Close();
                commandExecutor.CommandText = string.Format("INSERT INTO Villains (Name, Evilnes) " +
                                                            "VALUES ('{0}', 'evil')", villainName);
                commandExecutor.ExecuteNonQuery();

                commandExecutor.CommandText = "SELECT TOP 1 v.VillainID, v.Name FROM Villains AS v " +
                                              "ORDER BY v.VillainID DESC";
                dataReader = commandExecutor.ExecuteReader();
                dataReader.Read();
                villainID = dataReader[0].ToString();
                villainName = dataReader[1].ToString();
                dataReader.Close();

                logger.Add($"Villain {villainName} was added to the database.");

            }
            else
            {
                dataReader.Read();
                villainID = dataReader[0].ToString();
                Namevillain = dataReader[1].ToString();
                dataReader.Close();
            }
        }

        public static void AddMinion(SqlCommand commandExecutor, string minionName, string minionAge)
        {
            commandExecutor.CommandText = string.Format("INSERT INTO Minions (Name, Age, TownID) " +
                                                        "VALUES ('{0}', {1}, {2})", minionName, minionAge, city);
            commandExecutor.ExecuteNonQuery();

            commandExecutor.CommandText = "SELECT TOP 1 MinionID FROM Minions AS m " +
                                          "ORDER BY m.MinionID DESC";
            SqlDataReader dataReader = commandExecutor.ExecuteReader();
            dataReader.Read();
            minionID = dataReader[0].ToString();
            dataReader.Close();

            commandExecutor.CommandText = string.Format("INSERT INTO MinionsVillains(MinionsID, VillianID) " +
                                                        "VALUES ({0}, {1})", minionID, villainID);
            commandExecutor.ExecuteNonQuery();
            logger.Add($"Successfully added {minionName} to be minion of {Namevillain}.");

        }
    }
}
