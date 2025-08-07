using E_shift.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using E_shift.DataAccess;

namespace E_shift.Authentication_Service
{
    internal class AuthenticationService
    {
        public static User Login(string username, string password)
        {
            string hashedPassword = Hash(password);

            using var conn = Database.GetConnection();
            conn.Open();

            string query = "SELECT * FROM [User] WHERE Username = @username AND Password = @password";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserID = (int)reader["UserID"],
                    Username = reader["Username"].ToString(),
                    Role = reader["Role"].ToString(),
                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                };
            }

            return null;
        }

        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
