﻿using Npgsql;

namespace MyBackend.Database;

/// <summary>
/// Provides helper methods for working with database.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Checks database existence and create database if it is not present.
    /// </summary>
    /// <param name="connectionString">SQL server connection string.</param>
    /// <param name="dbName">Database name.</param>
    /// <remarks>
    /// This method is intended to run on startup and thus is executed synchronously.
    /// </remarks>
    public static bool EnsureExists(string connectionString, string dbName)
    {
        using var dbConnection = new NpgsqlConnection(connectionString);

        dbConnection.Open();

        var existCmd = dbConnection.CreateCommand();
        existCmd.CommandText = "select count(*) from pg_database where datname = @name";
        existCmd.Parameters.Add(new NpgsqlParameter("name", dbName.ToLowerInvariant()));

        var existed = Convert.ToInt32(existCmd.ExecuteScalar());

        if (existed != 0)
        {
            return false;
        }

        var createCmd = dbConnection.CreateCommand();
        createCmd.CommandText = $"CREATE DATABASE \"{dbName.ToLowerInvariant()}\"";
        createCmd.ExecuteNonQuery();

        return true;
    }
}
