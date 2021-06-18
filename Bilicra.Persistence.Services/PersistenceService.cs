using Bilicra.API.Domain.Models;
using Bilicra.Persistence.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Bilicra.Persistence.Services
{
    public static class PersistenceService
    {
        public static bool ValidateConnectionParameter(string parameter)
        {
            return string.IsNullOrWhiteSpace(parameter);
        }

        public static SqlConnection Connect(ConnectionModel con)
        {
            return new SqlConnection($"Data Source={con.ServerName};Initial Catalog={con.DatabaseName};User ID={con.UserName};Password={con.Password};Persist Security Info = True;");
        }

        public static SqlCommand CreateSqlCommand(SqlConnection connection, string commandText, params object[] sqlParameters)
        {
            using (SqlCommand cmd = new SqlCommand(commandText, connection))
            {
                for (int i = 0; i < sqlParameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + (i + 1).ToString(), sqlParameters[i]);
                }

                return cmd;
            }
        }

        public static List<TResult> GetList<TResult>(SqlCommand sqlCommand)
        {
            if (sqlCommand.Connection.State == ConnectionState.Closed)
            {
                sqlCommand.Connection.Open();
            }

            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                return DataReaderMapToList<TResult>(dataReader);
            }
        }

        public static TResult Get<TResult>(SqlCommand sqlCommand)
        {
            TResult result = Activator.CreateInstance<TResult>();

            if (sqlCommand.Connection.State == ConnectionState.Closed)
            {
                sqlCommand.Connection.Open();
            }

            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    foreach (System.Reflection.PropertyInfo prop in result.GetType().GetProperties())
                    {
                        if (!object.Equals(dataReader[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(result, dataReader[prop.Name], null);
                        }
                    }
                }

                return result;
            }
        }

        public static DataTable GetDataTable(SqlCommand sqlCommand)
        {
            if (sqlCommand.Connection.State == ConnectionState.Closed)
            {
                sqlCommand.Connection.Open();
            }

            using (IDataReader _DataReader = sqlCommand.ExecuteReader())
            {
                DataTable datatable = new DataTable();

                datatable.Load(_DataReader);

                return datatable;
            }
        }

        public static void SqlExecute(SqlCommand sqlCommand)
        {
            if (sqlCommand.Connection.State == ConnectionState.Closed)
            {
                sqlCommand.Connection.Open();
            }

            sqlCommand.ExecuteNonQuery();
        }

        public static object GetValue(SqlCommand sqlCommand)
        {
            DataTable datatable = GetDataTable(sqlCommand);

            if (datatable.Rows.Count > 0)
            {
                return datatable.Rows[0][0];
            }
            else
            {
                return null;
            }
        }

        private static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);

            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
