using AutoMapper;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prometheus.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        // getting user only by id
        public General<UserViewModel> GetById(int id)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.User.SingleOrDefault(i => i.Id == id);

                // we are checking whether we have data or not
                if (data is not null)
                {
                    result.Entity = mapper.Map<UserViewModel>(data);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Kullanıcı başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Hiçbir kullanıcı bulunamadı.";
                }
            }

            return result;
        }

        // getting all users
        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.User.ToList();

                // if we have user then we can list them otherwise we get exception message
                if (data.Any())
                {
                    result.List = mapper.Map<List<UserViewModel>>(data);
                    result.IsSuccess = true;
                    result.SuccessfulMessage = "Kullanıcılar başarıyla getirilmiştir.";
                }
                else
                {
                    result.ExceptionMessage = "Hiçbir kullanıcı bulunamadı.";
                }
            }

            return result;
        }

        public General<UserViewModel> AddUser(AddUserViewModel newUser)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            try
            {
                var model = mapper.Map<Prometheus.DB.Entities.User>(newUser);

                using (var context = new PrometheusContext())
                {
                    // if model is not null then we can add user to database
                    if (model is not null)
                    {
                        model.Idate = DateTime.Now;
                        context.User.Add(model);
                        context.SaveChanges();

                        result.Entity = mapper.Map<UserViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Kullanıcılar başarıyla eklenmiştir.";
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
    }
}
