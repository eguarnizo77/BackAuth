using System.Threading.Tasks;

namespace BackAuth.Data.Interface
{
    public interface ISendingEmailService
    {
        int GenerateCode();
        string GenerateBodyHtml(int code, string name);
        Task<bool> SendEmail(string to, string subject, string body);
    }
}
