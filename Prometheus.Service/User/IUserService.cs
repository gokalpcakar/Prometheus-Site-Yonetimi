using Prometheus.Model;
using Prometheus.Model.User;

namespace Prometheus.Service.User
{
    public interface IUserService
    {
        public General<UserViewModel> GetById(int id);
        public General<UserViewModel> GetUsers();
        public General<UserViewModel> Register(AddUserViewModel newUser);
        public General<UserViewModel> UpdateUser(UpdateUserViewModel user);
        public General<UserViewModel> DeleteUser(int id);
    }
}
