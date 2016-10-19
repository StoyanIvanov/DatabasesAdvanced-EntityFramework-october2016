namespace InitialSetup
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

                //Create Database, Create Tables and Fill data
                CreateDataBase(commandExecutor);
                CreateTables(commandExecutor);
            }
        }

        public static void CreateDataBase(SqlCommand commandExecutor)
        {
            try
            {
                //CREATE DATABASE
                commandExecutor.CommandText = "CREATE DATABASE MinionsDB";
                commandExecutor.ExecuteNonQuery();
                //USE CREATED DATABASE
                commandExecutor.CommandText = "USE MinionsDB";
                commandExecutor.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

        }

        public static void CreateTables(SqlCommand commandExecutor)
        {
            try
            {
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
                FillDataInTables(commandExecutor);
            }
            catch (Exception)
            {

            }

        }

        public static void FillDataInTables(SqlCommand commandExecutor)
        {
            //Insert records into Towns
            commandExecutor.CommandText = "INSERT INTO Towns(Name, Country) " +
                                          "VALUES " +
                                          "('Sofia', 'Bulgaria'), " +
                                          "('Plovdiv', 'Bulgaria'), " +
                                          "('Viena', 'Austria'), " +
                                          "('Barselona', 'Spain')";
            commandExecutor.ExecuteNonQuery();
            //Insert records into Minions
            commandExecutor.CommandText = "INSERT INTO Minions(Name, Age, TownID) " +
                                          "VALUES " +
                                          "('Bob', 13, 1), " +
                                          "('Kevin', 14, 2), " +
                                          "('Stewar', 19, 3), " +
                                          "('Bay Ivan', 168, 3), " +
                                          "('Gogo', 2, 1), " +
                                          "('Moro', 15, 2)";
            commandExecutor.ExecuteNonQuery();
            //Insert records into Villains
            commandExecutor.CommandText = "INSERT INTO Villains(Name, Evilnes) " +
                                          "VALUES " +
                                          "('Gru', 'good'), " +
                                          "('Victor', 'super evil'), " +
                                          "('Jilly', 'evil')";
            commandExecutor.ExecuteNonQuery();

            commandExecutor.CommandText = "INSERT INTO MinionsVillains(MinionsID, VillianID) " +
                                          "VALUES " +
                                          "(6, 2), " +
                                          "(3, 2), " +
                                          "(1, 1), " +
                                          "(4, 3), " +
                                          "(1, 2), " +
                                          "(2, 1), " +
                                          "(6, 1), " +
                                          "(5, 3), " +
                                          "(5, 2), " +
                                          "(3, 3)";
            commandExecutor.ExecuteNonQuery();
        }
    }
}
