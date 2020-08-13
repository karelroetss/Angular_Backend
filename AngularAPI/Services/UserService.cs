using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularAPI.Helpers;
using AngularAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AngularAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly DatabaseContext _databaseContext;

        public UserService(IOptions<AppSettings> appSettings, DatabaseContext databaseContext)
        {
            _appSettings = appSettings.Value;
            _databaseContext = databaseContext;
        }

        public Gebruiker Authenticate(string email, string wachtwoord)
        {
            var user = _databaseContext.Gebruikers.SingleOrDefault(x => x.Email == email && x.Wachtwoord == wachtwoord);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication succesful so generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("GebruikerID", user.GebruikerID.ToString()),
                    new Claim("Email", user.Email.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Wachtwoord = null;

            return user;
        }
    }
}
