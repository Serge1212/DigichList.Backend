using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class DefectImageEntityConfiguration : IEntityTypeConfiguration<DefectImage>
    {
        public void Configure(EntityTypeBuilder<DefectImage> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Defect)
                .WithMany(d => d.DefectImages);
        }
    }
}
