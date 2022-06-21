using BackAuth.Model;

namespace BackAuth.Data.Interface
{
    public interface IValidationsService
    {
        bool IsUserExist (string user);
    }

}
