using BackAuth.Model;

namespace BackAuth.Data.Interface
{
    public interface IValidationsService
    {
        bool IsUserExist (User user);
    }

}
