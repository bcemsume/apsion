using System;
using System.IO;
using Apsiyon.Logger.Context;
using Apsiyon.Logger.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Apsiyon.Logger
{
    public static class Init
    {
        public static ServiceProvider ServiceProvider;
        public static void InitializeApp()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
            .WriteTo.File("logs/errors.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            ConfigureServices(serviceCollection, appSettings);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        private static void ConfigureServices(IServiceCollection services, AppSettings connSetting)
        {
            services
            .AddDbContext<LoggerContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(connSetting.LoggingDb);
            })
            .AddLogging(configure => configure.AddSerilog())
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<ILogRepository, LogRepository>()
            .AddTransient<App>();

        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Init.InitializeApp();
            var app = Init.ServiceProvider.GetService<App>();
            app.Run();
        }

    }
}
