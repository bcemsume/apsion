using Apsiyon.Logger.Context;
using Apsiyon.Logger.Entities;
using Microsoft.Extensions.Logging;

namespace Apsiyon.Logger.DataAccess
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(LoggerContext dbContext, ILogger<Log> logger) : base(dbContext, logger) 
        {
        }
    
    }

}