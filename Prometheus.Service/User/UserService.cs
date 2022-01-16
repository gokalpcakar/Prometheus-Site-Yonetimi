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
                var data = context.User
                                    .SingleOrDefault
                                    (
                                        i => i.Id == id &&
                                        i.IsActive == true && !i.IsDeleted
                                    );

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

        // getting all active and not deleted users
        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.User
                                    .Where(x => x.IsActive && !x.IsDeleted)
                                    .OrderBy(x => x.Id);

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
        public General<UserViewModel> GetAdminUsers()
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var data = context.User
                                    .Where(x => x.IsAdmin && x.IsActive && !x.IsDeleted)
                                    .OrderBy(x => x.Id);

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

        public General<UserViewModel> Register(AddUserViewModel newUser)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            try
            {
                var model = mapper.Map<Prometheus.DB.Entities.User>(newUser);

                using (var context = new PrometheusContext())
                {
                    var apartmentModel = context.Apartment.SingleOrDefault(x => x.Id == newUser.ApartmentId);

                    // if model is not null then we can add user to database
                    if (model is not null)
                    {
                        model.Idate = DateTime.Now;
                        context.User.Add(model);

                        // i changed isFull to true during register
                        // every user have to add apartment when they are register
                        apartmentModel.IsFull = true;
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

        public General<UserViewModel> UpdateCreditCard(CreditCardUserViewModel user)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = context.User.SingleOrDefault(i => i.Id == user.Id);

                    if (model is not null)
                    {
                        model.CreditCardId = user.CreditCardId;
                        model.Udate = DateTime.Now;

                        context.SaveChanges();

                        result.Entity = mapper.Map<UserViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Kredi kartı başarıyla eklenmiştir.";
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

        public General<UserViewModel> UpdateProfile(UpdateProfileViewModel user)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = context.User.SingleOrDefault(i => i.Id == user.Id);

                    if (model is not null)
                    {
                        model.Name = user.Name;
                        model.Surname = user.Surname;
                        model.Email = user.Email;
                        model.Phone = user.Phone;
                        model.Tc = user.Tc;
                        model.PlateNo = user.PlateNo;
                        model.Udate = DateTime.Now;

                        context.SaveChanges();

                        result.Entity = mapper.Map<UserViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Profil başarıyla güncellenmiştir.";
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

        //public General<UserViewModel> UpdateUser(UpdateUserViewModel user)
        //{
        //        var result = new General<UserViewModel>() { IsSuccess = false };

        //                    try
        //                    {
        //                        using (var context = new PrometheusContext())
        //                        {
        //                            var model = context.User.SingleOrDefault(i => i.Id == user.Id);

        //                            if (model is not null)
        //                            {
        //                                model.Name = user.Name;
        //                                model.Surname = user.Surname;
        //                                model.Email = user.Email;
        //                                model.Phone = user.Phone;
        //                                model.Password = user.Password;
        //                                model.Tc = user.Tc;
        //                                model.PlateNo = user.PlateNo;
        //                                model.ApartmentId = user.ApartmentId;
        //                                model.Udate = DateTime.Now;

        //                                context.SaveChanges();

        //                                result.Entity = mapper.Map<UserViewModel>(model);
        //                                result.IsSuccess = true;
        //                                result.SuccessfulMessage = "Kullanıcılar başarıyla güncellenmiştir.";
        //                            }
        //                            else
        //                            {
        //                                result.ExceptionMessage = "Lütfen tekrardan deneyiniz.";
        //                            }
        //                        }
        //                    }
        //                    catch
        //{
        //    result.ExceptionMessage = "Beklenmedik bir hata oluştu.";
        //}

        //return result;
        //}

        public General<UserViewModel> DeleteUser(int id)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };

            try
            {
                using (var context = new PrometheusContext())
                {
                    var model = context.User.SingleOrDefault(i => i.Id == id);

                    if (model is not null)
                    {
                        model.IsActive = false;
                        model.IsDeleted = true;
                        context.SaveChanges();

                        result.Entity = mapper.Map<UserViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Kullanıcı silme işlemi başarıyla gerçekleştirilmiştir.";
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
