using System.Linq;
using Apsiyon.Logger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apsiyon.Logger.Context
{
    public class LoggerContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public LoggerContext(DbContextOptions<LoggerContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}