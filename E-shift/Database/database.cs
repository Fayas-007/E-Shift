using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace E_shift.DataAccess
{
    public static class Database
    {
        private static readonly string connectionString = @"Server=DESKTOP-4NONO39\SQLEXPRESS;Database=E_shift;Trusted_Connection=True;";


        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        //GetData() returns a full DataTable even for just one number.
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();

            using SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }

            return dt;
        }
       // GetData() returns a full DataTable even for just one number.
        public static DataTable GetData(string query, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();

            using SqlConnection conn = GetConnection();
            try
            {
                conn.Open();
                using SqlCommand cmd = new SqlCommand(query, conn);
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }

            return dt;
        }

        //ExecuteNonQuery is used for commands that don't return data
        public static void ExecuteNonQuery(string query)
        {
            using SqlConnection conn = GetConnection();
            conn.Open();
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }
        //ExecuteNonQuery is used for commands that don't return data
        public static void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using SqlConnection conn = GetConnection();
            conn.Open();
            using SqlCommand cmd = new SqlCommand(query, conn);

            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            cmd.ExecuteNonQuery();
        }

        //To get only one value from the table instead of whole table
        public static object ExecuteSingleValue(string query)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    return cmd.ExecuteScalar();// this method is designed to return single value 
                }
            }
        }
        public static object ExecuteSingleValue(string query, Dictionary<string, object> parameters = null)
        {
            using SqlConnection conn = GetConnection();
            conn.Open();
            using SqlCommand cmd = new SqlCommand(query, conn);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            return cmd.ExecuteScalar();
        }
    }
}
