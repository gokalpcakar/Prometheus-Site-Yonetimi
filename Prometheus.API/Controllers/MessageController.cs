using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.Message;
using Prometheus.Service.Message;
using System.Linq;

namespace Prometheus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        public MessageController(IMessageService _messageService)
        {
            messageService = _messageService;
        }

        [HttpGet("GetBySender/{id}")]
        public General<MessageViewModel> GetBySender(int id)
        {
            return messageService.GetBySenderId(id);
        }

        [HttpGet("GetByReceiver/{id}")]
        public General<MessageViewModel> GetByReceiver(int id)
        {
            return messageService.GetByReceiverId(id);
        }

        [HttpGet]
        public General<MessageViewModel> GetMessages()
        {
            return messageService.GetMessages();
        }

        [HttpPost]
        public General<MessageViewModel> AddMessage(AddMessageViewModel newMessage)
        {
            return messageService.AddMessage(newMessage);
        }

        [HttpPut]
        public General<MessageViewModel> UpdateMessage(UpdateMessageViewModel message)
        {
            return messageService.UpdateMessage(message);
        }

        [HttpDelete("{id}")]
        public General<MessageViewModel> DeleteMessage(int id)
        {
            return messageService.DeleteMessage(id);
        }
    }
}
