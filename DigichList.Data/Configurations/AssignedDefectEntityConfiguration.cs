using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class AssignedDefectEntityConfiguration : IEntityTypeConfiguration<AssignedDefect>
    {
        public void Configure(EntityTypeBuilder<AssignedDefect> builder)
        {
            builder.HasKey(d => d.Id);
        }
    }
}
