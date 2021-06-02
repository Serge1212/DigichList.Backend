using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DigichList.Infrastructure.Data
{
    internal class DigichListContextFactory : IDesignTimeDbContextFactory<DigichListContext>
    {
        public DigichListContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DigichListContext>();
            optionsBuilder.UseNpgsql("Server=ec2-34-247-118-233.eu-west-1.compute.amazonaws.com;" +
                " port=5432; user id=qvjmmqjpewzsne; password=1e8c63da9337fbc7bf354e9154ac130881d7d4b8b9aa84c6311fdcadc6f3f422;" +
                " database=dcu1kak5dscd9a; pooling=true");
            return new DigichListContext();
        }
    }
}
