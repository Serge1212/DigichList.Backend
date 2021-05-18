using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DigichList.Infrastructure.Data
{
    internal class DigichListContextFactory : IDesignTimeDbContextFactory<DigichListContext>
    {
        public DigichListContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DigichListContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DigichListDb;Trusted_Connection=True;");
            return new DigichListContext();
        }
    }
}
