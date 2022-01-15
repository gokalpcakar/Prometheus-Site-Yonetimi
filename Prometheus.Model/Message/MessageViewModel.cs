using System;

namespace Prometheus.Model.Message
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public bool IsRead { get; set; }
        public bool IsNewMessage { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Idate { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
    }
}
