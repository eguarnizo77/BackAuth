using BackAuth.Data.Interface;
using BackAuth.Model.Response;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackAuth.Data.Service
{
    public class SendingEmailService : ISendingEmailService
    {
        private EmailConfiguration _emailConfiguration;      
        public SendingEmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public int GenerateCode()
        {
            return new Random().Next(1000, 9999);
        }

        public Task<bool> SendEmail(string to, string subject, string body)
        {
            MailMessage oMailMessage = new MailMessage(_emailConfiguration.Email, to, subject, body);

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();

            return Task.FromResult(true);
        }
    }
}
