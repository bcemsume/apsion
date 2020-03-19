using System;
using System.Collections.Generic;
using Apsiyon.Logger.DataAccess;
using Apsiyon.Logger.Entities;

namespace Apsiyon.Logger
{
    public class App
    {
        private ILogRepository _logRepository;
        public App(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void Run()
        {
            var log = new Log { CretedAt = DateTime.Now, Payload = "Test Payload", Message = "Test Message", Type = 1 };
            _logRepository.Add(log);
        }

        public void Add(Log entity)
        {
            _logRepository.Add(entity);
        }

        public IEnumerable<Log> GetAll()
        {
            var result = _logRepository.All();
            return result;
        }

        public void Update(Log entity)
        {
            _logRepository.Update(entity);
        }
    }
}