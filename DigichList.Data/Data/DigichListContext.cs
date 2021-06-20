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
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var userid = Environment.GetEnvironmentVariable("POSTGRES_LOCAL_USERID", EnvironmentVariableTarget.User);
                var password = Environment.GetEnvironmentVariable("POSTGRES_LOCAL_PASSWORD", EnvironmentVariableTarget.User);

                optionsBuilder.UseNpgsql($"Server=127.0.0.1; port=5432; user id={userid}; password={password};" +
                    $"database=DigichListDb; pooling=true; Maximum Pool Size=100");
            }
            else
            {
                optionsBuilder.UseNpgsql("host=ec2-34-247-118-233.eu-west-1.compute.amazonaws.com; username=qvjmmqjpewzsne;" +
                "password=1e8c63da9337fbc7bf354e9154ac130881d7d4b8b9aa84c6311fdcadc6f3f422;" +
                "database=dcu1kak5dscd9a; pooling=true; SSL Mode=Require;Trust Server Certificate=true;");
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
