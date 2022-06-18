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

        public bool IsUserExist(string email)
        {
            User userExist = new User();
            var users = _userService.GetAllUsers().Result;

            userExist = users.Where(x => x.Email == email).FirstOrDefault();

            return userExist != null ? true : false;
        }

    }
}
