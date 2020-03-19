using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apsiyon.Logger.Entities
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
        public byte Type { get; set; }
        public DateTime CretedAt { get; set; }
    }
}
