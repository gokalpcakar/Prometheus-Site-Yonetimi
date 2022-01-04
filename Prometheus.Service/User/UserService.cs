using AutoMapper;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<UserViewModel> GetById(int id)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.User.SingleOrDefault(i => i.Id == id);

                // veritabanında böyle bir kullanıcının olup olmadığı kontrol ediliyor
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

        // Tüm kullanıcıların listelendiği metot
        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.User.ToList();

                // veritabanında kullanıcı varsa kullanılar listeleniyor yoksa mesaj dönüyor
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
            var result = new General<UserViewModel>();

            try
            {
                var model = mapper.Map<Prometheus.DB.Entities.User>(newUser);

                using (var context = new PrometheusContext())
                {
                    // veritabanında kullanıcı varsa kullanılar listeleniyor yoksa mesaj dönüyor
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
