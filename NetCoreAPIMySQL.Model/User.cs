using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackAuth.Model
{
    public class User
    {
         public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Phone { get; set; }
        public int State { get; set; }
    }
}
