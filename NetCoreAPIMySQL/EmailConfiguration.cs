
namespace BackAuth.Data
{
    public class EmailConfiguration
    {
        public EmailConfiguration(string email, string password, string host, 
                                  int port, bool ssl, bool defeaulCredentials)
        {
            Email = email;
            Password = password;
            Host = host;
            Port = port;
            Ssl = ssl;  
            DefaulCredentials = defeaulCredentials;
        }
    
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public bool DefaulCredentials { get; set; }

    }
}
   

