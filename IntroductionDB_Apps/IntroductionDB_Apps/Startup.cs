namespace IntroductionDB_Apps
{
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
                //CREATE DATABASE
                commandExecutor.CommandText = "CREATE DATABASE MinionsDB";
                commandExecutor.ExecuteNonQuery();
                //USE CREATED DATABASE
                commandExecutor.CommandText = "USE MinionsDB";
                commandExecutor.ExecuteNonQuery();
                //CREATE TABLE Towns
                commandExecutor.CommandText = "CREATE TABLE Towns ( " +
                                              "TownID INT PRIMARY KEY IDENTITY(1,1), " +
                                              "Name VARCHAR(50) NOT NULL, " +
                                              "Country VARCHAR(50) NOT NULL " +
                                              ")";
                commandExecutor.ExecuteNonQuery();
                //CREATE TABLE Minions
                commandExecutor.CommandText = "CREATE TABLE Minions(" +
                                              "MinionID INT PRIMARY KEY IDENTITY(1,1), " +
                                              "Name VARCHAR(50) NOT NULL, " +
                                              "Age INT NOT NULL, " +
                                              "TownID INT NOT NULL " +
                                              "CONSTRAINT FK_MinionsTownID_TownsTownID FOREIGN KEY(TownID) " +
                                              "REFERENCES Towns(TownID)" +
                                              ")";
                commandExecutor.ExecuteNonQuery();
                //CREATE TABLE Villains
                commandExecutor.CommandText = "CREATE TABLE Villains(" +
                                              "VillainID INT PRIMARY KEY IDENTITY(1,1), " +
                                              "Name VARCHAR(50) NOT NULL, " +
                                              "Evilnes VARCHAR(10) NOT NULL " +
                                              "CONSTRAINT chk_Evilnes CHECK (Evilnes IN ('good', 'bad', 'evil', 'super evil')) " +
                                              ") ";
                commandExecutor.ExecuteNonQuery();
                //CREATE TABLE MinionsVillains
                commandExecutor.CommandText = "CREATE TABLE MinionsVillains ( " +
                                              "ID INT PRIMARY KEY IDENTITY(1,1), " +
                                              "MinionsID INT NOT NULL " +
                                              "CONSTRAINT FK_MinionsVillainsMinionsID_MinionsMinionID FOREIGN KEY(MinionsID) " +
                                              "REFERENCES Minions(MinionID), " +
                                              "VillianID INT NOT NULL " +
                                              "CONSTRAINT FK_MinionsVillainsVillianID_VillainsVillainID FOREIGN KEY (VillianID) " +
                                              "REFERENCES Villains(VillainID)" +
                                              ")";
                commandExecutor.ExecuteNonQuery();
            }
        }
    }
}
