

using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder
                .HasMany(u => u.Users)
                .WithOne(r => r.Role)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
