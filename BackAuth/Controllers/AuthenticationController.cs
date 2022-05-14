using BackAuth.Data.Interface;
using BackAuth.Model.Request;
using BackAuth.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;        
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;            
        }

        [HttpPost]
        public IActionResult Authentication([FromBody] AuthRequest model)
        {
            ResponseClient response = new ResponseClient();
            var userResponse = _authenticationService.Auth(model);
            if (userResponse == null)
            {
                response.Error = true;
                response.Message = "User or password incorrect";
                return Ok(response);
            }

            response.Error = false;
            response.Data = userResponse;

            return Ok(response);
        }
    }
}
