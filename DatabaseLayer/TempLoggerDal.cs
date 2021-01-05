using System;
using System.Collections.Generic;
using System.IO;
using InterviewProblem.Model;
using Microsoft.Data.Sqlite;

namespace InterviewProblem.DatabaseLayer
{
    public class TempLoggerDal
    {
        public TempLoggerDal(string databasePath)
        {
            _filePath = databasePath;
            _connectionString = new SqliteConnectionStringBuilder() { DataSource = _filePath, Mode = SqliteOpenMode.ReadWriteCreate }.ConnectionString;
            InitializeDatabase();
        }

        private readonly string _filePath;
        private readonly string _connectionString;

        public User AddUser(string userName)
        {

            var query = "INSERT INTO Users (UserName) " +
                        "VALUES (@userName); " +
                        "SELECT last_insert_rowid()";

            using (var con = new SqliteConnection(_connectionString))
            using (var cmd = new SqliteCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userName", userName);
                con.Open();
                var id = Convert.ToInt32(cmd.ExecuteScalar());
                return new User(userName, id);
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
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) yield break;
                    while (reader.Read())
                        yield return new User(reader.GetFieldValue<string>(1), reader.GetFieldValue<int>(0));
                }
            }
            yield break;
        }

        public bool AddMeasurement(User user, Temperature temperature)
        {
            var query = "INSERT INTO Temperatures (UserId, Timestamp, Voltage) " +
                        "VALUES (@userId, @timestamp, @voltage)";

            using (var con = new SqliteConnection(_connectionString))
            using (var cmd = new SqliteCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userId", user.UserId);
                cmd.Parameters.AddWithValue("@timestamp", temperature.TimeStamp);
                cmd.Parameters.AddWithValue("@voltage", temperature.TcVolts);
                con.Open();
                return cmd.ExecuteNonQuery() == 1;
            }

        }

        public IEnumerable<Temperature> GetMeasurementsForUser(User user)
        {
            var query = "SELECT Voltage, Timestamp " +
                        "FROM Temperatures " +
                        "WHERE UserId = @userId";

            using (var con = new SqliteConnection(_connectionString))
            using (var cmd = new SqliteCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userId", user.UserId);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) yield break;
                    while (reader.Read())
                        yield return new Temperature(reader.GetFieldValue<double>(0), reader.GetFieldValue<DateTime>(1));

                }
            }
            yield break;
        }

        private bool InitializeDatabase()
        {
            var query = File.ReadAllText(@"DatabaseLayer\TempLoggerSchema.sql");
            if (!File.Exists(_filePath))
            {
                using (var con = new SqliteConnection(_connectionString))
                using (var cmd = new SqliteCommand(query, con))
                {
                    con.Open();
                    return cmd.ExecuteNonQuery() == 0;
                }
            }
            return true;
        }

        //private bool UserExists(string userName)
        //{
        //    var query = "SELECT COUNT(1) " +
        //                "FROM Users " +
        //                "WHERE UserName = @userName";

        //    using (var con = new SqliteConnection(_connectionString))
        //    using (var cmd = new SqliteCommand(query, con))
        //    {
        //        cmd.Parameters.AddWithValue("@userName", userName);
        //        con.Open();
        //        return cmd.ExecuteNonQuery() == 1;
        //    }

        //}




    }
}
