using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace lab1
{
    public class RaceRepository
    {
        private readonly string _connectionString;

        public RaceRepository(string dbPath)
        {
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = dbPath
            };
            _connectionString = builder.ToString();
        }

        public void Initialize()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS RaceResults (
                    Id INTEGER PRIMARY KEY,
                    Driver TEXT NOT NULL,
                    Car TEXT NOT NULL,
                    Track TEXT NOT NULL,
                    LapSeconds INTEGER NOT NULL CHECK (LapSeconds BETWEEN 30 AND 600)
                );";
            command.ExecuteNonQuery();
        }

        public List<RaceResult> GetAll()
        {
            var result = new List<RaceResult>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                "SELECT Id, Driver, Car, Track, LapSeconds FROM RaceResults ORDER BY Id";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new RaceResult
                {
                    Id = reader.GetInt64(0),
                    Driver = reader.GetString(1),
                    Car = reader.GetString(2),
                    Track = reader.GetString(3),
                    LapSeconds = reader.GetInt32(4)
                });
            }

            return result;
        }

        public long Add(RaceResult row)
        {
            RaceRules.Validate(row);

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO RaceResults (Driver, Car, Track, LapSeconds)
                VALUES ($driver, $car, $track, $lap);";

            command.Parameters.AddWithValue("$driver", row.Driver);
            command.Parameters.AddWithValue("$car", row.Car);
            command.Parameters.AddWithValue("$track", row.Track);
            command.Parameters.AddWithValue("$lap", row.LapSeconds);
            command.ExecuteNonQuery();

            using var idCommand = connection.CreateCommand();
            idCommand.CommandText = "SELECT last_insert_rowid();";
            return (long)idCommand.ExecuteScalar();
        }

        public void Update(RaceResult row)
        {
            RaceRules.Validate(row);

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE RaceResults
                SET Driver = $driver, Car = $car, Track = $track, LapSeconds = $lap
                WHERE Id = $id;";

            command.Parameters.AddWithValue("$driver", row.Driver);
            command.Parameters.AddWithValue("$car", row.Car);
            command.Parameters.AddWithValue("$track", row.Track);
            command.Parameters.AddWithValue("$lap", row.LapSeconds);
            command.Parameters.AddWithValue("$id", row.Id);

            int affected = command.ExecuteNonQuery();
            if (affected != 1)
                throw new Exception("Не удалось обновить запись (Id=" + row.Id + ")");
        }

        public void Delete(long id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM RaceResults WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);

            int affected = command.ExecuteNonQuery();
            if (affected != 1)
                throw new Exception("Не удалось удалить запись (Id=" + id + ")");
        }

        public void ReplaceAll(List<RaceResult> rows)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            using (var delete = connection.CreateCommand())
            {
                delete.Transaction = transaction;
                delete.CommandText = "DELETE FROM RaceResults;";
                delete.ExecuteNonQuery();
            }

            foreach (var row in rows)
            {
                using var insert = connection.CreateCommand();
                insert.Transaction = transaction;
                insert.CommandText = @"
                    INSERT INTO RaceResults (Id, Driver, Car, Track, LapSeconds)
                    VALUES ($id, $driver, $car, $track, $lap);";
                insert.Parameters.AddWithValue("$id", row.Id);
                insert.Parameters.AddWithValue("$driver", row.Driver);
                insert.Parameters.AddWithValue("$car", row.Car);
                insert.Parameters.AddWithValue("$track", row.Track);
                insert.Parameters.AddWithValue("$lap", row.LapSeconds);
                insert.ExecuteNonQuery();
            }

            transaction.Commit();
        }
    }
}