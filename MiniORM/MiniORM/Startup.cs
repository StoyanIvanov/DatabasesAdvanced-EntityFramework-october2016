using System;
using System.Collections.Generic;
using MiniORM.Entities;

namespace MiniORM
{
    public class Startup
    {
        static void Main()
        {
            string connectionString = new ConnectionStringBuilder("MyWebSiteDatabase").ConnectionString;
            IDBContext context = new EntityManager(connectionString, true);
            User user = context.FindFirst<User>();

        }
    }
}
