using BackAuth.Data.Entity;
using BackAuth.Data.Interface;
using BackAuth.Model;
using BackAuth.Model.Common;
using BackAuth.Model.Request;
using BackAuth.Model.Response;
using BackAuth.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BackAuth.Data.Service
{
    public class AuthenticationService : IAuthenticationService    
    {

        private readonly AppSettings _appSettings;
        private readonly IUserEntity _userEntity;
        public AuthenticationService(IUserEntity userEntity,
                                     IOptions<AppSettings> appSettings)
        {            
            _appSettings = appSettings.Value;
            _userEntity = userEntity;
        }

        public UserAuthResponse Auth(AuthRequest model)
        {
            UserAuthResponse userResponse = new UserAuthResponse();
            string password = Encrypt.GetSHA256(model.Password);
            var users = _userEntity.GetAllUsers().Result;

            var user = users.Where(x => x.Email == model.Email && x.Password == password).FirstOrDefault();

            if (user == null) return null;

            userResponse.Email = user.Email;
            userResponse.Token = GetToken(user);

            return userResponse;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
