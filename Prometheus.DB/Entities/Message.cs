using System;
using System.Collections.Generic;

#nullable disable

namespace Prometheus.DB.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public bool IsRead { get; set; }
        public bool? IsNewMessage { get; set; }
    }
}
