using Assignment.Models;
using Assignment.Models.LoginRequest;

namespace Assignment.Services
{
    public interface IUserService
    {
        /* bool checkUser(String userName, String password);*/
        bool IsUniqueUser(string username);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<LocalUser> Register(RegistrationRequest registrationRequest);

    }
}
