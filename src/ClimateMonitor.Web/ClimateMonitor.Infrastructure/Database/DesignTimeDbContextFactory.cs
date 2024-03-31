using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClimateMonitor.Infrastructure.Database;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ClimateDbContext>
{
    public ClimateDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ClimateDbContext>();

        var baseDir = Directory.GetParent(Directory.GetCurrentDirectory());
        const string appsettingsRelativePath = @"ClimateMonitor.Web.API/appsettings.json";
        var appsettingsPath = Path.Combine(baseDir!.FullName, appsettingsRelativePath);

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile(appsettingsPath)
            .Build();

        var connectionString = configuration.GetValue<string>("ConnectionStrings:ClimateMonitorDatabase");

        builder.UseSqlServer(connectionString);

        return new ClimateDbContext(builder.Options);
    }
}
