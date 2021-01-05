using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewProblem.Model;
using Microsoft.Data.Sqlite;

namespace InterviewProblem.DatabaseLayer
{
    public class TempLoggerDal
    {
        public TempLoggerDal(string databasePath)
        {
            _filePath = databasePath;
            _connectionString = new SqliteConnectionStringBuilder() {DataSource = _filePath, Mode = SqliteOpenMode.ReadWriteCreate }.ConnectionString;
            InitializeDatabase();
        }

        private string _filePath;
        private string _connectionString;

        private bool InitializeDatabase()
        {
            var query = File.ReadAllText(@"DatabaseLayer\TempLoggerSchema.sql");
            if(!File.Exists(_filePath))
            {
                using(var con = new SqliteConnection(_connectionString))
                using(var cmd = new SqliteCommand(query, con))
                {
                    con.Open();
                    return cmd.ExecuteNonQuery() == 0;
                }
            }
            return true;
        }

        private bool UserExists(string userName)
        {
            var query = "SELECT COUNT(1) " +
                        "FROM Users " +
                        "WHERE UserName = @userName";

            using (var con = new SqliteConnection(_connectionString))
            using (var cmd = new SqliteCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userName", userName);
                con.Open();
                return cmd.ExecuteNonQuery() == 1; 
            }

        }

        public bool AddUser(string userName)
        {
            if (UserExists(userName)) return true;

            var query = "INSERT INTO Users (UserName) " +
                        "VALUES (@userName)";

            using (var con = new SqliteConnection(_connectionString))
            using (var cmd = new SqliteCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userName", userName);
                con.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            var query = "SELECT UserId, UserName " +
                        "FROM Users";

            using (var con = new SqliteConnection(_connectionString))
            using (var cmd = new SqliteCommand(query, con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) yield break;
                while (reader.Read())
                    yield return new User() { UserId = reader.GetInt32(0), UserName = reader.GetString(1) };
            }
            yield break;
        }   




    }
}
