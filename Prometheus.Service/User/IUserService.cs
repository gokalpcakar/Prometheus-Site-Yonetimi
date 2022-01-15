﻿using Prometheus.Model;
using Prometheus.Model.User;

namespace Prometheus.Service.User
{
    public interface IUserService
    {
        public General<UserViewModel> GetById(int id);
        public General<UserViewModel> GetUsers();
        public General<ApartmentUserViewModel> GetApartmentUser(int id);
        public General<UserViewModel> Register(AddUserViewModel newUser);
        public General<UserViewModel> UpdateCreditCard(CreditCardUserViewModel user);
        public General<UserViewModel> DeleteUser(int id);
    }
}
