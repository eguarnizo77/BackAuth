using BackAuth.Data.Interface;
using BackAuth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackAuth.Data.Entity;

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

        public Task<User> GetUserDetails(string email)
        {
            return _userEntity.GetUserDetails(email);
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
    }
}
