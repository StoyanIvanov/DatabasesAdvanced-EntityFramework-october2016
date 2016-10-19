
namespace _02.GetVillainsNames
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
                //Get Villains’ Names
                GetVillansNames(commandExecutor);
            }
        }
        public static void GetVillansNames(SqlCommand commandExecutor)
        {
            //Get Villains’ Names
            commandExecutor.CommandText = "Use MinionsDB " +
                                          "SELECT v.Name, COUNT(mv.MinionsID) FROM Villains AS v " +
                                          "INNER JOIN MinionsVillains AS mv " +
                                          "ON v.VillainID = mv.VillianID " +
                                          "GROUP BY v.Name " +
                                          "HAVING COUNT(mv.MinionsID) > 3 " +
                                          "ORDER BY v.Name";
            SqlDataReader dataReader = commandExecutor.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader[0]} {dataReader[1]}");
            }
            dataReader.Close();
        }
    }
}
