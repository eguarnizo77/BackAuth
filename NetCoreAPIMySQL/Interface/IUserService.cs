using BackAuth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackAuth.Data.Interface
{
    public interface IUserService
    {        
        Task<IList<User>> GetAllUsers();
        User GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<bool> InserUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);        
    }
}
