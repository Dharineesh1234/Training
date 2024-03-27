using Assignment.Models;
using Assignment.Models.LoginRequest;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class UserServices : IUserService
    {
        private readonly MoviesContext _db;
        private string secretKey;

        public UserServices(MoviesContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:secretKey");
        }

        public UserServices(MoviesContext db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            return user == null;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower() && u.Pasword == loginRequest.Password);
            if (user == null)
            {
                return new LoginResponse()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var loginResponse = new LoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return loginResponse;
        }

        public async Task<LocalUser> Register(RegistrationRequest registrationRequest)
        {
            var user = new LocalUser
            {
                Role = registrationRequest.Role,
                UserName = registrationRequest.UserName,
                Name = registrationRequest.Name,
                Pasword = registrationRequest.Password
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Pasword = "";
            return user;
        }
    }
}