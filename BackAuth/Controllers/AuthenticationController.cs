using BackAuth.Data.Interface;
using BackAuth.Model;
using BackAuth.Model.Request;
using BackAuth.Model.Response;
using BackAuth.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BackAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private IUserService _userService;
        private ISendingEmailService _sendingEmailService;
        private IValidationsService _validationsService;
        public AuthenticationController(IAuthenticationService authenticationService, 
                                        IUserService userService, 
                                        ISendingEmailService sendingEmailService,
                                        IValidationsService validationsService)
        {
            _authenticationService = authenticationService;            
            _userService = userService;
            _sendingEmailService = sendingEmailService;
            _validationsService = validationsService;
        }

        
        [HttpPost]
        public IActionResult Authentication([FromBody] AuthRequest model)
        {
            ResponseClient response = new ResponseClient();            
            var userResponse = _authenticationService.Auth(model);

            if (userResponse == null)
            {
                response.Success = false;
                response.Error = "User or password incorrect";
                return BadRequest(response);
            }          

            return Ok(userResponse);
        }

        [Route("PasswordReset")]
        [HttpPost]
        public async Task<IActionResult> PasswordReset([FromBody] UserEmailRequest userEmail)
        {
            ResponseClient responseError = new ResponseClient();         
            ResponseCodeReset  response = new ResponseCodeReset();
            int codeReset = 0;
            string bodyEmail = string.Empty;            
            response.Success = true;           

            if (!_validationsService.IsUserExist(userEmail.Email))
            {
                responseError.Success = false;
                responseError.Error = "Email already exists";
                return BadRequest(responseError);
            }

            User user = _userService.GetUserByEmail(userEmail.Email);
            codeReset = _sendingEmailService.GenerateCode();
            bodyEmail = _sendingEmailService.GenerateBodyHtml(codeReset, user.Username);

            await _sendingEmailService.SendEmail(userEmail.Email, "Restablecimiento de contraseña Authenticathion - App", bodyEmail);
            response.Code = codeReset;
            return Ok(response);
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            ResponseClient response = new ResponseClient();
            response.Success = false;

            if (user == null)
            {
                response.Error = "Model null";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_validationsService.IsUserExist(user.Email))
            {
                response.Error = "Email already exists";
                return BadRequest(response);
            }

            user.Password = Encrypt.GetSHA256(user.Password);
            await _userService.InserUser(user);

            response.Success = true;
            return Ok(response);
        }
    }
}
