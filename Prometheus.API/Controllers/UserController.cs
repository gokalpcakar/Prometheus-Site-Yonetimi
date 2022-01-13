using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prometheus.API.Helpers;
using Prometheus.DB.Entities.DataContext;
using Prometheus.Model;
using Prometheus.Model.User;
using Prometheus.Service.User;
using System;
using System.Linq;

namespace Prometheus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly JwtService jwtService;
        public UserController(
            IUserService _userService,
            JwtService _jwtService,
            IMapper _mapper)
        {
            userService = _userService;
            jwtService = _jwtService;
            mapper = _mapper;
        }

        CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        };

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public General<UserViewModel> Login(LoginViewModel loginUser)
        {
            General<UserViewModel> result = new() { IsSuccess = false };

            using (var context = new PrometheusContext())
            {
                var user = context.User.FirstOrDefault(
                    i => i.Email == loginUser.Email &&
                    i.Password == loginUser.Password &&
                    i.IsActive && !i.IsDeleted);

                if (user is not null)
                {
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));

                    result.IsSuccess = true;
                    result.Entity = mapper.Map<UserViewModel>(user);
                    result.SuccessfulMessage = "Giriş işlemi başarıyla gerçekleştirilmiştir";

                    if (result.IsSuccess)
                    {
                        var jwt = jwtService.Generate((user.Id).ToString());

                        // we need to make our cookie secure and samesite mode none
                        // otherwise cookie can be block
                        Response.Cookies.Append("jwt", jwt, cookieOptions);

                        return result;
                    }
                    else
                    {
                        result.ExceptionMessage = "Failed, Try again!";
                    }
                }
            }

            return result;
        }

        [AllowAnonymous]
        [Route("Logout")]
        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("SessionUser", cookieOptions);

            Response.Cookies.Delete("jwt", cookieOptions);

            return Ok(new { message = "success" });
        }

        [AllowAnonymous]
        [Route("LoggedUser")]
        [HttpGet]
        public ActionResult LoggedUser()
        {
            try
            {
                string jwt = Request.Cookies["jwt"];

                var token = jwtService.Verify(jwt);

                string userId = token.Issuer;

                using (var context = new PrometheusContext())
                {
                    var user = context.User.FirstOrDefault(i => i.Id == Convert.ToInt32(userId));

                    var sessionUser = mapper.Map<UserViewModel>(JsonConvert.DeserializeObject<Prometheus.DB.Entities.User>(HttpContext.Session.GetString("SessionUser")));

                    return Ok(sessionUser);
                }
            }
            catch
            {
                return Ok("Kullanıcı bulunamadı");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public General<UserViewModel> GetUser(int id)
        {
            return userService.GetById(id);
        }

        [AllowAnonymous]
        [HttpGet]
        public General<UserViewModel> GetUsers()
        {
            return userService.GetUsers();
        }

        [AllowAnonymous]
        [HttpPost]
        public General<UserViewModel> Register([FromBody] AddUserViewModel newUser)
        {
            return userService.Register(newUser);
        }

        [AllowAnonymous]
        [HttpPut]
        public General<UserViewModel> UpdateUser([FromBody] UpdateUserViewModel user)
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
                        model.Password = user.Password;
                        model.Tc = user.Tc;
                        model.PlateNo = user.PlateNo;
                        model.ApartmentId = user.ApartmentId;
                        model.Udate = DateTime.Now;

                        context.SaveChanges();

                        result.Entity = mapper.Map<UserViewModel>(model);
                        result.IsSuccess = true;
                        result.SuccessfulMessage = "Kullanıcılar başarıyla güncellenmiştir.";
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

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public General<UserViewModel> DeleteUser(int id)
        {
            return userService.DeleteUser(id);
        }
    }
}
