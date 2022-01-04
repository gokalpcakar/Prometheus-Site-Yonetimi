
using Prometheus.Model;
using Prometheus.Model.User;

namespace Prometheus.Service.User
{
    public interface IUserService
    {
        public General<UserViewModel> GetById(int id);
        public General<UserViewModel> GetUsers();
        public General<UserViewModel> AddUser(AddUserViewModel newUser);
    }
}
