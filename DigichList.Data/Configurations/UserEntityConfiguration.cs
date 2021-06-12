using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasMany(d => d.Defects)
                .WithOne(u => u.Publisher)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(a => a.AssignedDefects)
                .WithOne(u => u.AssignedWorker)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
