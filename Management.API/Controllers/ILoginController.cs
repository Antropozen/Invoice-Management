﻿//using AutoMapper;
//using Management.DB.Entities.DataContext;
//using Management.Model;
//using Management.Model.User;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;

//namespace Management.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ILoginController : ControllerBase
//    {
//        private readonly IMapper mapper;
//        private IConfiguration config;

//        public ILoginController(IConfiguration _config, IMapper _mapper)
//        {
//            config = _config;
//            mapper = _mapper;
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public IActionResult Login([FromBody] UserLoginModel userLogin)
//        {
//            var user = Authenticate(userLogin);

//            if (user != null)
//            {
//                var token = Generate(user.Entity);
//                Response.Cookies.Append(key:"jwt", value: token);
//                return Ok(token);
//            }

//            return NotFound("User not found");
//        }

//        private string Generate(UserViewModel user)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.SerialNumber,user.Id.ToString()),
//                //new Claim(ClaimTypes.Role, user.IsAdmin.ToString()),
//                //new Claim(ClaimTypes.Name, user.Name),
//                //new Claim(ClaimTypes.Surname, user.Surname),
//                //new Claim(ClaimTypes.Email, user.Email)
//            };
//            //claims.Add(new Claim(ClaimTypes.Role, user.IsAdmin.ToString()));
//            //claims.Add(new Claim(ClaimTypes.SerialNumber,user.Id.ToString())); ;

//            var token = new JwtSecurityToken(config["Jwt:Issuer"],
//              config["Jwt:Audience"],
//              claims,
//              expires: DateTime.Now.AddMinutes(120),
//              signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        private General<UserViewModel> Authenticate(UserLoginModel userLogin)
//        {
//            General<UserViewModel> result = new();

//            using (var srv = new ManagementContext())
//            {
//                var data = srv.User.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
//                if (data is not null)
//                {
//                    result.IsSucces = true;
//                    result.Entity = mapper.Map<UserViewModel>(data);
//                    result.SuccesMessage = "Giriş işlemi başarıyla gerçekleşmiştir.";
//                }
               
//            }
//            return result;
//        }
//    }
//}
