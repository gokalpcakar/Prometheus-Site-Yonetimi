using Microsoft.AspNetCore.Mvc;
using Prometheus.Model;
using Prometheus.Model.User;
using Prometheus.Service.User;

namespace Prometheus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet("{id}")]
        public General<UserViewModel> GetUser(int id)
        {
            return userService.GetById(id);
        }

        [HttpGet]
        public General<UserViewModel> GetUsers()
        {
            return userService.GetUsers();
        }

        [HttpPost]
        public General<UserViewModel> AddUser([FromBody] AddUserViewModel newUser)
        {
            return userService.AddUser(newUser);
        }

        [HttpPut]
        public General<UserViewModel> UpdateUser([FromBody] UpdateUserViewModel user)
        {
            return userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public General<UserViewModel> DeleteUser(int id)
        {
            return userService.DeleteUser(id);
        }
    }
}
