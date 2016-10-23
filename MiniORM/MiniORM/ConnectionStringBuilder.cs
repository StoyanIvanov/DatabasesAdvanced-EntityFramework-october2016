using System.Data.SqlClient;

namespace MiniORM
{
    public class ConnectionStringBuilder
    {
        SqlConnectionStringBuilder connectionStringBuilder;
        private string connectionString;
        public ConnectionStringBuilder(string databaseName)
        {
            this.connectionStringBuilder=new SqlConnectionStringBuilder();
            this.connectionStringBuilder["Data Source"] = "STOYAN-NOTEBOOK\\SQLSERVER";
            this.connectionStringBuilder["Integrated Security"] = true;
            this.connectionStringBuilder["Connect Timeout"] = 1000;
            this.connectionStringBuilder["Trusted_Connection"] = true;
            this.connectionStringBuilder["Initial Catalog"] = databaseName;
            this.connectionString = this.connectionStringBuilder.ToString();
        }

        public string ConnectionString
        {
            get { return this.connectionString; }
            private set { this.connectionString = value; }
        }
    }
}