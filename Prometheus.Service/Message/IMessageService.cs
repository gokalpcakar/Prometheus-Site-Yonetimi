using Prometheus.Model;
using Prometheus.Model.Message;

namespace Prometheus.Service.Message
{
    public interface IMessageService
    {
        public General<MessageViewModel> GetBySenderId(int id);
        public General<MessageViewModel> GetByReceiverId(int id);
        public General<MessageViewModel> GetMessages();
        public General<MessageViewModel> AddMessage(AddMessageViewModel newMessage);
        public General<MessageViewModel> UpdateMessage(UpdateMessageViewModel message);
        public General<MessageViewModel> DeleteMessage(int id);
    }
}
