using BackAuth.Data.Interface;
using BackAuth.Model;
using System.Linq;

namespace BackAuth.Data.Service
{
    public class ValidationsService : IValidationsService
    {

        private readonly IUserService _userService;

        public ValidationsService (IUserService userService)
        {
            _userService = userService;
        }

        public bool IsUserExist(User user)
        {
            User userExist = new User();
            var users = _userService.GetAllUsers().Result;

            userExist = users.Where(x => x.Email == user.Email).FirstOrDefault();

            return userExist != null ? true : false;
        }

    }
}
