using AutoMapper;
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
                    i.IsActive == true && !i.IsDeleted);

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

        [Route("Logout")]
        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("SessionUser", cookieOptions);

            Response.Cookies.Delete("jwt", cookieOptions);

            return Ok(new { message = "success" });
        }

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

        [HttpGet("AdminUsers")]
        public General<UserViewModel> GetAdminUsers()
        {
            return userService.GetAdminUsers();
        }

        [HttpGet("ApartmentUsers")]
        public ActionResult GetApartmentUsers()
        {
            using (var context = new PrometheusContext())
            {
                // joining two tables for listing apartments with user info
                var query = from user in context.User
                            join apartment in context.Apartment
                            on user.ApartmentId equals apartment.Id
                            where apartment.Id == user.ApartmentId && apartment.IsDeleted == false
                            orderby apartment.Id
                            select new ApartmentUserViewModel()
                            {
                                Id = user.Id,
                                BlockName = apartment.BlockName,
                                ApartmentFloor = apartment.ApartmentFloor,
                                ApartmentNo = apartment.ApartmentNo,
                                ApartmentType = apartment.ApartmentType,
                                IsFull = apartment.IsFull,
                                Name = user.Name,
                                Surname = user.Surname,
                                ApartmentId = apartment.Id,
                            };

                var list = query.ToList();

                return Ok(list);
            }
        }

        // joining user and apartment tables for getting a user by id
        [HttpGet("ApartmentUser/{id}")]
        public ActionResult GetApartmentUser(int id)
        {
            using (var context = new PrometheusContext())
            {

                var query = from user in context.User
                            join apartment in context.Apartment 
                            on user.ApartmentId equals apartment.Id
                            where apartment.Id == id
                            orderby apartment.Id
                            select new ApartmentUserViewModel()
                            {
                                Id = user.Id,
                                BlockName = apartment.BlockName,
                                ApartmentFloor = apartment.ApartmentFloor,
                                ApartmentNo = apartment.ApartmentNo,
                                ApartmentType = apartment.ApartmentType,
                                IsFull = apartment.IsFull,
                                Name = user.Name,
                                Surname = user.Surname,
                                ApartmentId = apartment.Id,
                            };

                var list = query.ToList();

                return Ok(list);
            }
        }

        [HttpPost]
        public General<UserViewModel> Register([FromBody] AddUserViewModel newUser)
        {
            return userService.Register(newUser);
        }

        [Route("UpdateCreditCard")]
        [HttpPut]
        public General<UserViewModel> UpdateCreditCard(CreditCardUserViewModel user)
        {
            return userService.UpdateCreditCard(user);
        }

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
                        model.IsAdmin = user.IsAdmin;
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

        [HttpPut("EditProfile")]
        public General<UserViewModel> UpdateProfile([FromBody] UpdateProfileViewModel user)
        {
            return userService.UpdateProfile(user);
        }

        [HttpDelete("{id}")]
        public General<UserViewModel> DeleteUser(int id)
        {
            return userService.DeleteUser(id);
        }
    }
}
