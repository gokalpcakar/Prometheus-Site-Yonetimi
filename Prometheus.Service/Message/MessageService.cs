using AutoMapper;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Service.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMapper mapper;
        public MessageService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<MessageViewModel> GetBySenderId(int id)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var model = context.Message.Where(i => i.SenderId == id && !i.IsDeleted);

                if (model is not null)
                {
                    result.List = mapper.Map<List<MessageViewModel>>(model);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Mesaj bilgisi başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar deneyiniz";
                }
            }

            return result;
        }
        public General<MessageViewModel> GetByReceiverId(int id)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var model = context.Message.Where(i => i.ReceiverId == id && !i.IsDeleted);

                if (model is not null)
                {
                    result.List = mapper.Map<List<MessageViewModel>>(model);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Mesaj bilgisi başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar deneyiniz";
                }
            }

            return result;
        }
        public General<MessageViewModel> GetMessages()
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var model = context.Message.Where(x => !x.IsDeleted).OrderBy(i => i.Id);

                //foreach (var message in model)
                //{
                //    if (message is not null)
                //    {
                //        message.IsNewMessage = false;
                //        context.SaveChanges();
                //    }
                //}

                if (model is not null)
                {

                    result.List = mapper.Map<List<MessageViewModel>>(model);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Mesajlar başarıyla getirildi.";
                }
                else
                {
                    result.ExceptionMessage = "Lütfen tekrar giriniz.";
                }
            }

            return result;
        }
        public General<MessageViewModel> AddMessage(AddMessageViewModel newMessage)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            try
            {

                using (var context = new PrometheusContext())
                {
                    var model = mapper.Map<Prometheus.DB.Entities.Message>(newMessage);

                    var sender = context.User.SingleOrDefault(i => i.Id == newMessage.SenderId);

                    model.SenderName = sender.Name;
                    model.SenderSurname = sender.Surname;

                    var receiver = context.User.SingleOrDefault(x => x.Id == newMessage.ReceiverId);

                    model.ReceiverName = receiver.Name;
                    model.ReceiverSurname = receiver.Surname;

                    if (model is not null)
                    {
                        context.Message.Add(model);
                        context.SaveChanges();

                        result.Entity = mapper.Map<MessageViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Mesaj başarıyla eklendi.";
                    }
                    else
                    {
                        result.ExceptionMessage = "Lütfen tekrar giriniz.";
                    }
                }
            }
            catch
            {
                result.ExceptionMessage = "Beklenmedik bir hata oluştu";
            }

            return result;
        }
        public General<MessageViewModel> UpdateMessage(UpdateMessageViewModel message)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = context.Message.SingleOrDefault(i => i.Id == message.Id);

                    if (model is not null)
                    {
                        model.MessageContent = message.MessageContent;
                        model.IsNewMessage = true;
                        model.Udate = DateTime.Now;

                        context.SaveChanges();

                        result.Entity = mapper.Map<MessageViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Mesaj başarıyla güncellenmiştir.";
                    }
                    else
                    {
                        result.ExceptionMessage = "Lütfen tekrardan deneyiniz.";
                    }
                }
            }
            catch
            {
                result.ExceptionMessage = "Beklenmedik bir hata oluştu.";
            }

            return result;
        }
        public General<MessageViewModel> DeleteMessage(int id)
        {
            var result = new General<MessageViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = context.Message.SingleOrDefault(i => i.Id == id);

                    if (model is not null)
                    {
                        model.IsDeleted = true;
                        context.SaveChanges();

                        result.Entity = mapper.Map<MessageViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Mesaj silme işlemi başarıyla gerçekleştirilmiştir.";
                    }
                    else
                    {
                        result.ExceptionMessage = "Lütfen tekrar deneyiniz.";
                    }
                }
            }
            catch
            {
                result.ExceptionMessage = "Beklenmedik bir hata oluştu";
            }

            return result;
        }
    }
}
