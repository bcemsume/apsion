using System;
using System.Collections.Generic;
using System.Linq;
using Apsiyon.Logger.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Apsiyon.Logger.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LoggerContext _dbContext;
        private readonly ILogger _logger;
        public Repository(LoggerContext dbContext, ILogger<T> logger)
        {
            this._dbContext = dbContext;
            _logger = logger;
            this.Table = dbContext.Set<T>();
        }
        public DbSet<T> Table { get; set; }

        public bool Add(T entity)
        {
            Table.Add(entity);
            return Save();
        }

        public IEnumerable<T> All()
        {
            return Table.ToList();
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            return Save();
        }

        public bool Update(T entity)
        {
            Table.Update(entity);
            return Save();
        }

        private bool Save()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Db Error: ", ex.ToString());
                return false;
            }
        }
    }
}