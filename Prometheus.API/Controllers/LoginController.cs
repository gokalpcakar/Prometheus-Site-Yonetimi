﻿//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using Prometheus.API.Areas.Identity.Data;
//using Prometheus.API.Helpers;
//using Prometheus.API.Data;
//using Prometheus.Model.User;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace Prometheus.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class LoginController : ControllerBase
//    {
//        private readonly JwtConfig configuration;
//        private readonly JwtService jwtService;
//        private readonly IdentityPrometheusContext context;
//        private readonly SignInManager<AppUser> signInManager;
//        private readonly UserManager<AppUser> userManager;
//        public LoginController(
//                        IOptions<JwtConfig> _configuration,
//                        IdentityPrometheusContext _context,
//                        SignInManager<AppUser> _signInManager,
//                        UserManager<AppUser> _userManager,
//                        JwtService _jwtService)
//        {
//            configuration = _configuration.Value;
//            jwtService = _jwtService;
//            signInManager = _signInManager;
//            userManager = _userManager;
//            context = _context;
//        }

//        [AllowAnonymous]
//        [Route("Login")]
//        [HttpPost]
//        public async Task<ActionResult> Login([FromBody] LoginViewModel loginUser)
//        {
//            var user = context.Users.FirstOrDefault(i => i.Email == loginUser.Email);

//            if (user is not null)
//            {
//                var login = await signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);

//                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));

//                if (login.Succeeded)
//                {
//                    //generate token that is valid for a day

//                   var tokenHandler = new JwtSecurityTokenHandler();
//                    var key = Encoding.ASCII.GetBytes(configuration.Secret);
//                    var tokenDescriptor = new SecurityTokenDescriptor
//                    {
//                        Subject = new ClaimsIdentity(new Claim[] {

//                    new Claim(ClaimTypes.Name, "Cemil")}),
//                        Expires = DateTime.UtcNow.AddDays(1),
//                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//                    };

//                    var token = tokenHandler.CreateToken(tokenDescriptor);
//                    loginUser.Token = tokenHandler.WriteToken(token);

//                    Response.Cookies.Append("jwt", loginUser.Token, new CookieOptions
//                    {
//                        HttpOnly = true,
//                        Secure = true,
//                        SameSite = SameSiteMode.None
//                    });

//                    //var jwt = jwtService.Generate(user.Id);

//                    // we need to make our cookie secure and samesite mode none
//                    // otherwise cookie can be block
//                    //Response.Cookies.Append("jwt", jwt, new CookieOptions
//                    //{
//                    //    HttpOnly = true,
//                    //    Secure = true,
//                    //    SameSite = SameSiteMode.None
//                    //});

//                    //return Ok(loginUser);
//                    return Ok(user);
//                    //return Ok(jwt);
//                    //return Ok(new { message = "success" });
//                }
//                else
//                {
//                    return Ok("Failed, Try again!");
//                }
//            }

//            return Ok("Failed, Try again!");
//        }

//        [AllowAnonymous]
//        [Route("Logout")]
//        [HttpPost]
//        public async Task<ActionResult> Logout()
//        {
//            await signInManager.SignOutAsync();

//            HttpContext.Session.Clear();

//            Response.Cookies.Delete("SessionUser", new CookieOptions
//            {
//                HttpOnly = true,
//                Secure = true,
//                SameSite = SameSiteMode.None
//            });

//            Response.Cookies.Delete("jwt", new CookieOptions
//            {
//                HttpOnly = true,
//                Secure = true,
//                SameSite = SameSiteMode.None
//            });

//            return Ok(new { message = "success" });
//        }

//        [AllowAnonymous]
//        [Route("Register")]
//        [HttpPost]
//        public async Task<ActionResult> Register([FromBody] AddUserViewModel newUser)
//        {
//            AppUser appUser = new AppUser()
//            {
//                Email = newUser.Email,
//                UserName = newUser.Name,
//                EmailConfirmed = false
//            };

//            var result = await userManager.CreateAsync(appUser, newUser.Password);

//            if (result.Succeeded)
//            {
//                return Ok(new { Result = "Register succeded" });
//            }
//            else
//            {
//                StringBuilder stringBuilder = new StringBuilder();

//                foreach (var error in result.Errors)
//                {
//                    stringBuilder.Append(error.Description);
//                }

//                return Ok(new { Result = $"Register fail: {stringBuilder.ToString()}" });
//            }
//        }

//        [AllowAnonymous]
//        [Route("GetUser")]
//        [HttpGet]
//        public ActionResult GetUser()
//        {
//            try
//            {
//                string jwt = Request.Cookies["jwt"];

//                var token = jwtService.Verify(jwt);

//                string userId = token.Issuer;

//                var user = context.Users.FirstOrDefault(i => i.Id == userId);

//                var sessionUser = JsonConvert.DeserializeObject<AppUser>(HttpContext.Session.GetString("SessionUser"));

//                //return Ok(user);
//                return Ok(sessionUser);
//            }
//            catch
//            {
//                return Ok("Kullanıcı buluanamadı");
//            }
//        }
//    }
//}
