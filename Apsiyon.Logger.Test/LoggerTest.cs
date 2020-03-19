
using System;
using System.Collections.Generic;
using Apsiyon.Logger.DataAccess;
using Apsiyon.Logger.Entities;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Apsiyon.Logger.Test
{
    [TestFixture]
    public class LoggerTest
    {
        const string MESSAGE = "Test Message";
        const string PAYLOAD = "Test Payload";
        const string UPDATED_PAYLOAD = "Test Payload 123";
        private App App;
        private List<Log> _items = new List<Log>();
        private void InitMock()
        {
            var mock = new Mock<ILogRepository>();
            mock.Setup(p => p.Add(It.IsAny<Log>()))
            .Callback((Log item) => _items.Add(item));
            mock.Setup(p => p.All()).Returns(_items);
            mock.Setup(p => p.Update(_items.FirstOrDefault())).Callback((Log log) =>
           {
               var lg = _items.FirstOrDefault(x => x.Id == log.Id);
               if (lg == null)
                   throw new Exception("Kayit bulunamadi");
               lg.Payload = log.Payload;
           });

            App = new App(mock.Object);
        }
        public LoggerTest()
        {
            Init.InitializeApp();
            InitMock();
        }
        [Test]
        public void Add_Log_Db_Return_True()
        {
            var log = new Log { CretedAt = DateTime.Now, Payload = "Test Payload", Message = "Test Message", Type = 1 };
            App.Add(log);
            Assert.True(_items.Count > 0);
        }

        [Test]
        public void Update_Log_Db_Return_True()
        {
            var logItem = new Log { Id = 1, CretedAt = DateTime.Now, Payload = UPDATED_PAYLOAD, Message = MESSAGE };
            App.Update(logItem);
            Assert.AreEqual(_items.FirstOrDefault().Payload, UPDATED_PAYLOAD);
        }


    }
}
