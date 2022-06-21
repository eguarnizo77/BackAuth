
namespace BackAuth.Data
{
    public class EmailConfiguration
    {
        public EmailConfiguration(string email, string password)
        {
            Email = email;
            Password = password;
        }
    
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
   

