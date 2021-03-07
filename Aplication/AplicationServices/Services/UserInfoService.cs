using Aplication.AplicationServices.Interface;
using Aplication.Users;
using Domain;
using Aplication.Users.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Aplication.AplicationServices.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly Config _config;

        public readonly IUsersService _UsersService;

        public UserInfoService(IUsersService UsersService, Config config)
        {
            _UsersService = UsersService;
            _config = config;
        }
        public Userinfo Autenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) return null;
            LoginDTO pRet = _UsersService.GetUsersByEmail(email, password);
            Userinfo userinfo = new Userinfo();
            userinfo.Nombre = pRet.Nombre;
            userinfo.Email = pRet.Email;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, pRet.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(_config.appSettings.dias),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userinfo.Token = tokenHandler.WriteToken(token);
            return userinfo;
        }
    }
}
