using BackAuth.Data.Interface;
using BackAuth.Model;
using BackAuth.Model.Response;
using BackAuth.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetAllUsers(string email)
        {
            return Ok(await _userService.GetUserDetails(email));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            ResponseClient response = new ResponseClient();
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           user.Password = Encrypt.GetSHA256(user.Password);

            await _userService.InserUser(user);

            response.Error = false;            

            return Ok(response);            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
