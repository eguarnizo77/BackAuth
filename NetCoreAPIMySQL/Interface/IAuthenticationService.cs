using BackAuth.Model.Request;
using BackAuth.Model.Response;


namespace BackAuth.Data.Interface
{
    public interface IAuthenticationService
    {
        UserAuthResponse Auth(AuthRequest model);
    }
}
