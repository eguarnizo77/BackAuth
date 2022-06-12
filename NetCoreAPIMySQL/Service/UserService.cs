using BackAuth.Data.Interface;
using BackAuth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackAuth.Data.Entity;
using System.Linq;

namespace BackAuth.Data.Service
{
    public class UserService : IUserService
    {                
        private readonly IUserEntity _userEntity;
        public UserService(IUserEntity userEntity)
        {                        
            _userEntity = userEntity;
        }

        public Task<IList<User>> GetAllUsers()
        {
            return _userEntity.GetAllUsers();
        }

        public User GetUserByEmail(string email)
        {
            var users = _userEntity.GetAllUsers().Result;
            var user = users.Where(x => x.Email == email).FirstOrDefault();
            return user;
        }

        public Task<bool> InserUser(User user)
        {
            return _userEntity.InserUser(user);
        }

        public Task<bool> UpdateUser(User user)
        {
            return _userEntity.UpdateUser(user);
        }

        public Task<bool> DeleteUser(User user)
        {
            return _userEntity.DeleteUser(user);
        }

        public Task<User> GetUserById(int id)
        {
            return _userEntity.GetUser(id);
        }
    }
}
