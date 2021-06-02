using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DigichList.Infrastructure.Data
{
    internal class DigichListContextFactory : IDesignTimeDbContextFactory<DigichListContext>
    {
        public DigichListContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DigichListContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1; port=5432; user id =postgres; password=postgresidk; database=DigichListDb; pooling = true");
            return new DigichListContext();
        }
    }
}
