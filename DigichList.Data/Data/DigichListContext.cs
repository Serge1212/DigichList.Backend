using DigichList.Core.Entities;
using DigichList.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DigichList.Infrastructure.Data
{
    public class DigichListContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DigichListDb;Trusted_Connection=True;");
            optionsBuilder.UseNpgsql("Server=127.0.0.1; port=5432; user id=postgres; password=postgresidk; database=DigichListDb; pooling=true");
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
