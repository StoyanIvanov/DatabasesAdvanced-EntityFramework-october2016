using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using MiniORM.Attributes;

namespace MiniORM
{
    public interface IDBContext
    {
        bool Persist(object entity);
        
        T FindByID<T>(int id);

        IEnumerable<T> FindAll<T>();

        IEnumerable<T> FindAll<T>(string where);

        T FindFirst<T>();

        T FindFirst<T>(string where);

        void Delete<T>(object entity);

        void DeleteByID<T>(int id);

    }
}