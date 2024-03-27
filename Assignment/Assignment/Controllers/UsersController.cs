using Assignment.Models;
using Assignment.Models.LoginRequest;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;

namespace Assignment.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    
    public class UsersController : Controller
    {
      private readonly IUserService _userService;
        protected APIResponse _apiResponse;
        public UsersController(IUserService userService)
        {
            _userService = userService;
            this._apiResponse = new();
        }
        [HttpPost("Login")]
        public async Task <IActionResult> Login([FromBody] LoginRequest model)
        {
            var loginResponse = await _userService.Login(model);
            if(loginResponse.User==null||string.IsNullOrEmpty(loginResponse.Token)) 
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorsMessage.Add("user name or password is incorrect");
                
                
              
                return BadRequest(_apiResponse);
            }
            _apiResponse.statusCode = HttpStatusCode.OK;

            _apiResponse.IsSuccess = true;
            _apiResponse.Result= loginResponse;

            return Ok(_apiResponse);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest model)
        {
            bool ifUserNameUnique = _userService.IsUniqueUser(model.UserName);
            if(!ifUserNameUnique) 
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorsMessage.Add("user name already exist");


                return BadRequest(_apiResponse);
            }
            var user=await _userService.Register(model);
            if(user==null) 
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorsMessage.Add("error while registering");


                return BadRequest(_apiResponse);
            }
            _apiResponse.statusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;



            return Ok(_apiResponse);
        }
        
    }
}
