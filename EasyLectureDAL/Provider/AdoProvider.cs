using EasyLectureDAL.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace EasyLectureDAL.Provider
{
    public class AdoProvider
    {
        private string _connectionString { get; init; }

        public AdoProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionString);

            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                conn?.Dispose();
                Console.WriteLine("İlk Denemede Hata..");
                Console.WriteLine(ex);

                Thread.Sleep(1000);
                try
                {
                    conn = new SqlConnection(_connectionString);
                    conn.Open();
                    return conn;
                }
                catch (Exception ex2)
                {
                    conn?.Dispose();
                    Console.WriteLine("İlk Denemede Hata..");
                    Console.WriteLine(ex2);

                    throw;
                }
            }
        }

        // SQL bağlantısını açan ve komut çalıştıran bir metod
        private SqlCommand GetCommand(SqlConnection connection, string query, List<DbParam> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(query, connection);

            // Parametreler ekleniyor
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    SqlParameter sqlParam = new SqlParameter(param.ParameterName, param.Value);
                    sqlParam.DbType = param.DbType;  // DbType ayarlama
                    cmd.Parameters.Add(sqlParam);
                }
            }

            return cmd;
        }

        public void GetList(Action<SqlDataReader> func, string query, List<DbParam> parameters = null)
        {
            if (ValidateSql(query))
            {
                Console.WriteLine("Empty Sql");
                return;
            }

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection cnn = GetConnection())
                {
                    using (SqlCommand cmd = GetCommand(cnn, query, parameters))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                func(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex);
                throw;
            }
        }

        public void GetSingle(Action<SqlDataReader> func, string query, List<DbParam> parameters = null)
        {
            if (ValidateSql(query))
            {
                Console.WriteLine("Empty Sql");
                return;
            }

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection cnn = GetConnection())
                {
                    using (SqlCommand cmd = GetCommand(cnn, query, parameters))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            func(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex);
                throw;
            }
        }

        public T GetValue<T>(string query, List<DbParam> parameters = null)
        {
            if (ValidateSql(query))
            {
                Console.WriteLine("Empty Sql");
                return default;
            }

            try
            {
                object val = default(T);

                using (SqlConnection cnn = GetConnection())
                {
                    using (SqlCommand cmd = GetCommand(cnn, query, parameters))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                val = reader.GetValue(0);
                            }
                        }
                    }
                }

                // Eğer değer null ise ve T bir değer türü (int, float, vb.) ise varsayılan değeri (0) döndür
                if (val == null && typeof(T) == typeof(int))
                {
                    return (T)(object)0;
                }

                return (T)val;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex);
                throw;
            }
        }


        public T ExecuteWithId<T>(string query, List<DbParam> parameters = null, bool useTransaction = false)
        {
            T result = default(T);

            if (ValidateSql(query))
            {
                Console.WriteLine("Empty Sql");
                return default;
            }

            SqlTransaction transaction = null;

            try
            {
                object val = null;

                using (SqlConnection cnn = GetConnection())
                {
                    if (useTransaction)
                        transaction = cnn.BeginTransaction();

                    using (SqlCommand cmd = GetCommand(cnn, query, parameters))
                    {
                        cmd.Transaction = transaction; // Transaction nesnesini ata

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                val = reader.GetValue(0);
                            }
                        }
                        
                        if (useTransaction)
                            transaction?.Commit();
                    }
                }
                if (val != null)
                {
                    result = (T)Convert.ChangeType(val, typeof(T));
                }
                else if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    result = default;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (useTransaction)
                            transaction?.Rollback();
                }
                catch (Exception exSub)
                {
                    Console.WriteLine("Hata: " + exSub);
                    throw;
                }

                Console.WriteLine("Hata: " + ex);
                throw;
            }

            return result;
        }


        public void Execute(string query, List<DbParam> parameters = null, bool useTransaction = false)
        {
            if (ValidateSql(query))
            {
                Console.WriteLine("Empty Sql");
                return;
            }

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection cnn = GetConnection())
                {
                    if (useTransaction)
                        transaction = cnn.BeginTransaction();

                    using (SqlCommand cmd = GetCommand(cnn, query, parameters))
                    {
                        cmd.Transaction = transaction; // Transaction nesnesini ata

                        cmd.ExecuteNonQuery();

                        if (useTransaction)
                            transaction?.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (useTransaction)
                        transaction?.Rollback();
                }
                catch (Exception exSub)
                {
                    Console.WriteLine("Hata: " + exSub);
                    throw;
                }

                Console.WriteLine("Hata: " + ex);
                throw;
            }
        }


        private bool ValidateSql(string query)
        {
            return string.IsNullOrEmpty(query);
        }
    }
}
