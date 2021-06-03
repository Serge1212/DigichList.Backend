using DigichList.Core.Entities;
using DigichList.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigichList.Infrastructure.Data
{
    public class DigichListContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                var host     = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_HOST"    , EnvironmentVariableTarget.User);
                var username = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_USERNAME", EnvironmentVariableTarget.User);
                var password = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_PASSWORD", EnvironmentVariableTarget.User);
                var database = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_DATABASE", EnvironmentVariableTarget.User);

                optionsBuilder.UseNpgsql($"host={host}; username={username}; password={password}; database={database};" +
                    $"pooling=true; SSL Mode=Require;Trust Server Certificate=true;");
            }
            else
            {
                var userid = Environment.GetEnvironmentVariable("POSTGRES_LOCAL_USERID", EnvironmentVariableTarget.User);
                var password = Environment.GetEnvironmentVariable("POSTGRES_LOCAL_PASSWORD", EnvironmentVariableTarget.User);

                optionsBuilder.UseNpgsql($"Server=127.0.0.1; port=5432; user id={userid}; password={password};" +
                    $"database=DigichListDb; pooling=true");
            }
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<DefectImage> DefectImages { get; set; }
        public DbSet<AssignedDefect> AssignedDefects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DefectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DefectImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AssignedDefectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        }

    }
}
