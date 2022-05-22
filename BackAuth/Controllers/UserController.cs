using BackAuth.Data.Interface;
using BackAuth.Model;
using BackAuth.Model.Response;
using BackAuth.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IValidationsService _validationsService;

        public UserController(IUserService userService, 
                              IValidationsService validationsService)
        {
            _userService = userService;
            _validationsService = validationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userService.GetUser(id));
        }

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
          
            if (_validationsService.IsUserExist(user))
            {
                response.Error = "Email already exists";
                return BadRequest(response);
            }
                                          
           user.Password = Encrypt.GetSHA256(user.Password);
           await _userService.InserUser(user);

            response.Success = true;
           return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.Password = Encrypt.GetSHA256(user.Password);
            await _userService.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.InserUser(new User() { Id = id} );            
            return NoContent();
        }
    }
}
