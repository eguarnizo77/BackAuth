using BackAuth.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackAuth.Data.Entity
{
    public interface IUserEntity
    {
        Task<IList<User>> GetAllUsers();
        Task<User> GetUser(int id);
        Task<bool> InserUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
    }
    public class UserEntity : IUserEntity
    {
        private APIConfiguration _connectionString;        
        public UserEntity(APIConfiguration connectionString)
        {
            _connectionString = connectionString;            
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var db = dbConnection();
            var sql = @"SELECT id, email, username, password, bio, phone, state
                        FROM user";

            return (IList<User>)await db.QueryAsync<User>(sql, new { });
        }

        public async Task<User> GetUser(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id, email, username, password, bio, phone, state
                        FROM user
                        WHERE id = @Id";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<bool> InserUser(User user)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO user(email, username, password, bio, phone, state)
                        VALUES (@Email, @Username, @Password, @Bio, @Phone, @State) ";

            var result = await db.ExecuteAsync(sql, new { user.Email, user.Username, user.Password, user.Bio, user.Phone, user.State });
            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();
            var sql = @"UPDATE user
                        SET email = @Email, username = @Username, password = @Password, bio = @Bio, Phone @phone, state = @State
                        WHERE id = @Id ";

            var result = await db.ExecuteAsync(sql, new { user.Email, user.Username, user.Bio, user.Password, user.Phone, user.State, user.Id });
            return result > 0;
        }

        public async Task<bool> DeleteUser(User user)
        {
            var db = dbConnection();
            var sql = @"DELETE 
                        FROM user                        
                        WHERE id = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = user.Id });
            return result > 0;
        }
        
    }
}
