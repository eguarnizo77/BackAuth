using BackAuth.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAuth.Data.Entity
{
    public interface IImageProfileEntity
    {
        Task<IList<ImageProfile>> GetAll();
        Task<ImageProfile> Get(int id);
    }
    public class ImageProfileEntity : IImageProfileEntity
    {
        private APIConfiguration _connectionString;
        public ImageProfileEntity(APIConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        public async Task<IList<ImageProfile>> GetAll()
        {
            var db = dbConnection();
            var sql = @"SELECT id, name, path
                        FROM images_profile";

            return (IList<ImageProfile>)await db.QueryAsync<ImageProfile>(sql, new { });
        }

        public async Task<ImageProfile> Get(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id, name, path
                        FROM images_profile
                        WHERE id = @Id";
            
            return (await db.QueryAsync<ImageProfile>(sql, new { Id = id })).First();
            
        }
    }
}
