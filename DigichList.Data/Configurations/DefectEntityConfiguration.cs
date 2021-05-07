using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class DefectEntityConfiguration : IEntityTypeConfiguration<Defect>
    {
        public void Configure(EntityTypeBuilder<Defect> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasMany(d => d.DefectImages)
                .WithOne(d => d.Defect)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.AssignedDefect)
                .WithOne(d => d.Defect)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
