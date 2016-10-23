using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using MiniORM.Attributes;

namespace MiniORM
{
    public class EntityManager : IDBContext
    {
        private SqlConnection connection;
        private string connectionString;
        private bool isCodeFirst;
        public EntityManager(string connectionString, bool isCodeFirst)
        {
            this.connectionString = connectionString;
            this.isCodeFirst = isCodeFirst;
        }
        public bool Persist(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot persist null entity");
            }

            if (this.isCodeFirst && !CheckIfTableExist(entity.GetType()))
            {
                this.CreateTable(entity.GetType());
            }

            Type entityType = entity.GetType();
            FieldInfo fieldInfo = GetID(entityType);
            int id = (int)fieldInfo.GetValue(entity);

            if (id <= 0)
            {
                return this.Insert(entityType, entity);
            }

            return this.Update(entityType, entity);

            //throw new System.NotImplementedException();
            using (this.connection = new SqlConnection(this.connectionString))
            {

            }
        }
        private bool Insert(Type entityType, object entity)
        {
            int affectedRows;
            StringBuilder insertQuery = new StringBuilder().Append($"INSERT INTO {GetTableName(entityType)} "); ;
            StringBuilder columnNamesQueryPart = new StringBuilder().Append("(");
            StringBuilder columnValuesQueryPart = new StringBuilder().Append("(");
            FieldInfo[] fields = entityType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(e => e.IsDefined(typeof(ColumnAttribute))).ToArray();
            foreach (var field in fields)
            {
                columnNamesQueryPart.Append($"{field.Name}, ");

                if (field.FieldType.Name == "Int32")
                {
                    columnValuesQueryPart.Append($"{field.GetValue(entity)}, ");
                }
                else if (field.FieldType.Name == "DateTime")
                {
                    DateTime date = (DateTime)field.GetValue(entity);
                    columnValuesQueryPart.Append($"'{date.Year}{date.Month}{date.Day} " +
                                                 $"{date.Hour}:{date.Minute}:{date.Second}', ");
                }
                else
                {
                    columnValuesQueryPart.Append($"'{field.GetValue(entity)}', ");
                }

            }

            columnNamesQueryPart.Remove(columnNamesQueryPart.Length - 2, 2);
            columnValuesQueryPart.Remove(columnValuesQueryPart.Length - 2, 2);
            columnNamesQueryPart.Append(");");
            columnValuesQueryPart.Append(");");

            insertQuery.Append($"{columnNamesQueryPart} ");
            insertQuery.Append($"VALUES {columnValuesQueryPart}");

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(insertQuery.ToString(), this.connection);
                affectedRows = (int)command.ExecuteNonQuery();
            }

            return affectedRows > 0;
        }
        private bool Update(Type entityType, object entity)
        {
            int affectedRows;
            StringBuilder insertQuery = new StringBuilder().Append($"UPDATE {GetTableName(entityType)} "); ;
            StringBuilder columnValuesQueryPart = new StringBuilder();
            FieldInfo[] fields = entityType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(e => e.IsDefined(typeof(ColumnAttribute))).ToArray();
            FieldInfo id =
                entityType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .FirstOrDefault(f => f.IsDefined(typeof (IdAttribute)));
            foreach (var field in fields)
            {

                if (field.FieldType.Name == "Int32")
                {
                    columnValuesQueryPart.Append($"{field.Name}= {field.GetValue(entity)}, ");
                }
                else if (field.FieldType.Name == "DateTime")
                {
                    DateTime date = (DateTime)field.GetValue(entity);
                    columnValuesQueryPart.Append($"{field.Name}= '{date.Year}{date.Month}{date.Day} " +
                                                 $"{date.Hour}:{date.Minute}:{date.Second}', ");
                }
                else
                {
                    columnValuesQueryPart.Append($"{field.Name}= '{field.GetValue(entity)}', ");
                }

            }

            columnValuesQueryPart.Remove(columnValuesQueryPart.Length - 2, 2);
            insertQuery.Append($"SET {columnValuesQueryPart}");
            insertQuery.Append($"WHERE ID={id.GetValue(entity)}");

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(insertQuery.ToString(), this.connection);
                affectedRows = (int)command.ExecuteNonQuery();
            }

            return affectedRows > 0;
        }
        private bool CheckIfTableExist(Type type)
        {
            int commandResult = 0;
            string query = $"SELECT COUNT(name) " +
                           $"FROM sys.sysobjects " +
                           $"WHERE [Name] = '{GetTableName(type)}' AND [xtype] = 'U'";
            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(query, this.connection);
                commandResult = (int)command.ExecuteScalar();
            }

            return commandResult > 0;
        }
        public T FindByID<T>(int id)
        {
            T result = default(T);

            using (this.connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {this.GetTableName(typeof(T))} WHERE ID=@Id", this.connection
                    );

                command.Parameters.AddWithValue("@Id", id);
                this.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException($"No found entity with id {id}");
                    }
                    reader.Read();
                    result = CreateEntity<T>(reader);
                }
            }

            return result;
        }
        private T CreateEntity<T>(SqlDataReader reader)
        {
            object[] originalValues = new object[reader.FieldCount];
            reader.GetValues(originalValues);

            object[] values = new object[reader.FieldCount-1];
            Array.Copy(originalValues,1,values,0,reader.FieldCount-1);
            Type[] types = new Type[values.Length];
            for (int i = 0; i < types.Length; i++)
            {
                types[i] = values[i].GetType();
            }

            T entity = (T) typeof (T).GetConstructor(types).Invoke(values);
            typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(x=>x.IsDefined(typeof(IdAttribute)))
                .SetValue(entity, originalValues[0]);

            return entity;
        }
        public IEnumerable<T> FindAll<T>()
        {
            List<T> results = new List<T>();

            using (this.connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {this.GetTableName(typeof(T))}", this.connection
                    );
                this.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException($"No found entity with id");
                    }

                    while (reader.Read())
                    {
                        results.Add(CreateEntity<T>(reader));
                    }
                }
            }

            return results;
        }
        public IEnumerable<T> FindAll<T>(string @where)
        {
            List<T> results = new List<T>();

            using (this.connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT * FROM {this.GetTableName(typeof(T))} {@where}", this.connection);
                this.connection.Open();
            
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException($"No found entity with parameter {@where}");
                    }

                    while (reader.Read())
                    {
                        results.Add(CreateEntity<T>(reader));
                    }
                }
            }

            return results;
        }
        public T FindFirst<T>()
        {
            T result = default(T);

            using (this.connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT TOP 1 * FROM {this.GetTableName(typeof(T))} ORDER BY ID ASC", this.connection
                    );
                this.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException("Not found entity");
                    }
                    reader.Read();
                    result = CreateEntity<T>(reader);
                }
            }

            return result;
        }
        public T FindFirst<T>(string @where)
        {
            T result = default(T);

            using (this.connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    $"SELECT TOP 1 * FROM {this.GetTableName(typeof(T))} {@where} ORDER BY ID ASC", this.connection
                    );
                this.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException("Not found entity");
                    }
                    reader.Read();
                    result = CreateEntity<T>(reader);
                }
            }

            return result;
        }
        public void Delete<T>(object entity)
        {
            Type entityType = entity.GetType();
            FieldInfo ID =entityType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .FirstOrDefault(f => f.IsDefined(typeof (IdAttribute)));
            StringBuilder deleteQuery = new StringBuilder();
            deleteQuery.Append($"DELETE FROM {GetTableName(entityType)} WHERE ID = {ID.GetValue(entity)}");

            using (this.connection=new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(deleteQuery.ToString(), this.connection);
                command.ExecuteNonQuery();
            }
            
        }
        public void DeleteByID<T>(int id)
        {
            StringBuilder deleteQuery = new StringBuilder();
            deleteQuery.Append($"DELETE FROM {GetTableName(typeof(T))} WHERE ID = @id");

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(deleteQuery.ToString(), this.connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
        private void CreateTable(Type entity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"CREATE TABLE {GetTableName(entity)} (");
            builder.Append("ID INT PRIMARY KEY IDENTITY(1,1), ");

            FieldInfo[] columns =
                entity.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(t => t.IsDefined(typeof(ColumnAttribute))).ToArray();

            foreach (var columnField in columns)
            {
                builder.Append($"{GetColumnName(columnField)} {GetColumnType(columnField)}, ");
            }
            builder.Remove(builder.Length - 2, 2);
            builder.Append(");");

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(builder.ToString(), this.connection);
                command.ExecuteNonQuery();
            }
        }
        private string GetColumnType(FieldInfo columnField)
        {
            switch (columnField.FieldType.Name)
            {
                case "Int32":
                    return "INT";
                    break;
                case "String":
                    return "VARCHAR(MAX)";
                    break;
                case "DateTime":
                    return "DATETIME";
                    break;
                case "Boolean":
                    return "BIT";
                    break;
                default:
                    throw new ArgumentException("Not found the filed type");
            }

        }
        private string GetColumnName(FieldInfo columnField)
        {
            return columnField.GetCustomAttribute<ColumnAttribute>().ColumnName;
        }
        private FieldInfo GetID(Type entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot request null for id");
            }

            FieldInfo id = entity.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.IsDefined(typeof(IdAttribute)));

            if (id == null)
            {
                throw new ArgumentNullException("No field was no found in the current class");
            }

            return id;
        }
        private string GetTableName(Type entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot request null for table name");
            }



            if (!entity.IsDefined(typeof(EntityAttribute)) || entity.GetCustomAttribute<EntityAttribute>().TableName == null)
            {
                return entity.Name;
            }

            string tableName = entity.GetCustomAttribute<EntityAttribute>().TableName;
            return tableName;

        }

    }
}