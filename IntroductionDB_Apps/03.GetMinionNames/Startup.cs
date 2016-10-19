
namespace _03.GetMinionNames
{
    using System;
    using System.Data.SqlClient;

    public class Startup
    {
        static void Main()
        {
            string connectionString = "Server=STOYAN-NOTEBOOK\\SQLSERVER; Database=master; Trusted_Connection=true";
            SqlConnection sqlconnection = new SqlConnection(connectionString);

            sqlconnection.Open();
            using (sqlconnection)
            {
                SqlCommand commandExecutor = new SqlCommand("", sqlconnection);

                //Get Minion Names
                GetMinionsName(commandExecutor);

            }
        }

        public static void GetMinionsName(SqlCommand commandExecutor)
        {
            //Get Minion Names
            string inputID = Console.ReadLine();
            commandExecutor.CommandText = "Use MinionsDB " +
                                          "SELECT v.Name, m.Name, m.Age FROM Minions AS m " +
                                          "INNER JOIN MinionsVillains AS mv " +
                                          "ON mv.VillianID =" + inputID +
                                          "INNER JOIN Villains AS v " +
                                          "ON v.VillainID = mv.VillianID " +
                                          "GROUP BY v.Name, m.Name, m.Age";
            SqlDataReader dataReader = commandExecutor.ExecuteReader();

            if (dataReader.HasRows)
            {
                int count = 1;

                while (dataReader.Read())
                {
                    if (count == 1)
                    {
                        Console.WriteLine($"Villain: {dataReader[0]}");
                    }

                    Console.WriteLine($"{count}. {dataReader[1]} {dataReader[2]}");
                    count++;
                }

                dataReader.Close();
            }
            else
            {
                Console.WriteLine($"No villain with ID {inputID} exists in the database.");
            }
        }

    }
}
