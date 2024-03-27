using Assignment.Models;
using Assignment.Models.LoginRequest;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public async Task<LocalUser> Register(RegistrationRequest registrationRequest)
        {
            var passwordHash = HashPassword(registrationRequest.Password);
            var user = new LocalUser
            {
                Role = registrationRequest.Role,
                UserName = registrationRequest.UserName,
                Name = registrationRequest.Name,
                Pasword = passwordHash  // Store the hashed password
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Pasword = "";  // Clear the password for security
            return user;
        }

        private string HashPassword(string password)
        {
            // Use a strong hashing algorithm like bcrypt or PBKDF2
            // Example using PBKDF2
            byte[] salt = GenerateSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20); // 20 is the size of the hash
            byte[] hashBytes = new byte[36];  // 20 for the hash, 16 for the salt
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[16]; // Choose appropriate salt size
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }


        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
            if (user == null || !VerifyPassword(loginRequest.Password, user.Pasword))
            {
                return new LoginResponse()
                {
                    Token = "",
                    User = null
                };
            }

            // Token generation remains the same
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

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}