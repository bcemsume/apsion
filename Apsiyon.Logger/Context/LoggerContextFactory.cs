using System.IO;
using Apsiyon.Logger;
using Apsiyon.Logger.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class LoggerContextFactory : IDesignTimeDbContextFactory<LoggerContext>
{
    LoggerContext IDesignTimeDbContextFactory<LoggerContext>.CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var config = builder.Build();

        var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

        var optionsBuilder = new DbContextOptionsBuilder<LoggerContext>()
            .UseSqlServer(appSettings.LoggingDb);

        return new LoggerContext(optionsBuilder.Options);
    }
}

