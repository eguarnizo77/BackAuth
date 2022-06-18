using BackAuth.Data.Interface;
using BackAuth.Model;
using BackAuth.Model.Request;
using BackAuth.Model.Response;
using BackAuth.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Route("GetUserByEmail")]
        [HttpPost]
        public IActionResult GetUserByEmail([FromBody] UserEmailRequest userEmail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_userService.GetUserByEmail(userEmail.Email));
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

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
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

            if (user.Id.Equals(""))
            {
                response.Error = "Error model";
                return BadRequest(response);
            }


            User userUpdate = _userService.GetUserById(user.Id).Result;
            user.Password = user.Password != userUpdate.Password ? Encrypt.GetSHA256(user.Password) : userUpdate.Password;            
            userUpdate.Email = user.Email != string.Empty && userUpdate.Email != user.Email ? user.Email : userUpdate.Email;
            userUpdate.Password = user.Password != string.Empty && userUpdate.Password != user.Password ? user.Password : userUpdate.Password;
            userUpdate.Username = user.Username != string.Empty && userUpdate.Username != user.Username ? user.Username : userUpdate.Username;
            userUpdate.Image = user.Image != 0 && userUpdate.Image != user.Image ? user.Image : userUpdate.Image;
            userUpdate.Bio = user.Bio != string.Empty && userUpdate.Bio != user.Bio ? user.Bio : userUpdate.Bio;
            userUpdate.Phone = user.Phone;
            userUpdate.State = userUpdate.State != user.State ? user.State : userUpdate.State;
            
            await _userService.UpdateUser(userUpdate);

            response.Success = true;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.InserUser(new User() { Id = id} );            
            return NoContent();
        }
    }
}
