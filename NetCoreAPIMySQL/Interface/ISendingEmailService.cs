using System.Threading.Tasks;

namespace BackAuth.Data.Interface
{
    public interface ISendingEmailService
    {
        int GenerateCode();
        Task<bool> SendEmail(string to, string subject, string body);
    }
}
