using System;
using MiniORM.Attributes;

namespace MiniORM.Entities
{
    [Entity(TableName = "Users")]
    public class User
    {
        [Id]
        private int id;

        [Column(ColumnName = "Username")]
        private string username;

        [Column(ColumnName = "Password")]
        private string password;

        [Column(ColumnName = "Age")]
        private int age;

        [Column(ColumnName = "RegistrationDate")]
        private DateTime registrationDate;

        public User(string userName, string password, int age, DateTime registrationDate)
        {
            this.Username = userName;
            this.Password = password;
            this.Age = age;
            this.RegistrationDate = registrationDate;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set { registrationDate = value; }
        }
    }
}
