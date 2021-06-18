using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace DigichList.Infrastructure.Data
{
    internal class DigichListContextFactory : IDesignTimeDbContextFactory<DigichListContext>
    {
        public DigichListContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DigichListContext>();
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var userid = Environment.GetEnvironmentVariable("POSTGRES_LOCAL_USERID", EnvironmentVariableTarget.User);
                var password = Environment.GetEnvironmentVariable("POSTGRES_LOCAL_PASSWORD", EnvironmentVariableTarget.User);

                optionsBuilder.UseNpgsql($"Server=127.0.0.1; port=5432; user id={userid}; password={password};" +
                    $"database=DigichListDb; pooling=true; Maximum Pool Size=100");
            }
            else
            {
                //var host = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_HOST", EnvironmentVariableTarget.User);
                //var username = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_USERNAME", EnvironmentVariableTarget.User);
                //var password = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_PASSWORD", EnvironmentVariableTarget.User);
                //var database = Environment.GetEnvironmentVariable("POSTGRES_REMOTE_DATABASE", EnvironmentVariableTarget.User);

                //optionsBuilder.UseNpgsql($"host={host}; username={username}; password={password}; database={database};" +
                //    $"pooling=true; SSL Mode=Require;Trust Server Certificate=true;");
                optionsBuilder.UseNpgsql("host=ec2-34-247-118-233.eu-west-1.compute.amazonaws.com; username=qvjmmqjpewzsne;" +
                "password=1e8c63da9337fbc7bf354e9154ac130881d7d4b8b9aa84c6311fdcadc6f3f422;" +
                "database=dcu1kak5dscd9a; pooling=true; SSL Mode=Require;Trust Server Certificate=true;");
            }
            return new DigichListContext();
        }
    }
}
