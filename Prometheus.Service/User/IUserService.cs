using Prometheus.Model;
using Prometheus.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Service.User
{
    public interface IUserService
    {
        public General<UserViewModel> GetById(int id);
        public General<UserViewModel> GetUsers();
        public General<UserViewModel> AddUser(AddUserViewModel newUser);
    }
}
