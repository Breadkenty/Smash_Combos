﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmashHub.Domain.Models;
using SmashHub.Core.Services;
using System;
using System.Text.RegularExpressions;
using Npgsql;

namespace SmashHub.Persistence
{
    public partial class PostgreSqlDatabaseContext : DbContext, IDbContext
    {
        // Change this if you want to have a different database name in development
        private static string DEVELOPMENT_DATABASE_NAME = "Smash_CombosDatabase";

        // Change this to true if you want to have logging of SQL statements in development
        private static bool LOG_SQL_STATEMENTS_IN_DEVELOPMENT = false;

        public DbSet<Character> Characters { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ComboVote> ComboVotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Infraction> Infractions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (LOG_SQL_STATEMENTS_IN_DEVELOPMENT && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                optionsBuilder.UseLoggerFactory(loggerFactory);
            }

            if (!optionsBuilder.IsConfigured)
            {
                var databaseURL = Environment.GetEnvironmentVariable("DATABASE_URL");
                var defaultConnectionString = $"server=localhost;database={DEVELOPMENT_DATABASE_NAME}";

                var conn = databaseURL != null ? ConvertPostConnectionToConnectionString(databaseURL) : defaultConnectionString;
                optionsBuilder.UseNpgsql(conn);
            }
        }

        private string ConvertPostConnectionToConnectionString(string connection)
        {
            var _connection = connection.Replace("postgres://", String.Empty);
            var connectionParts = Regex.Split(_connection, ":|@|/");

            return $"server={connectionParts[2]};SSL Mode=Require;Trust Server Certificate=true;database={connectionParts[4]}; User Id = { connectionParts[0] }; password={connectionParts[1]}; port={connectionParts[3]}";
        }
    }
}
