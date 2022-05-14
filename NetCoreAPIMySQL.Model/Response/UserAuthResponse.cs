using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackAuth.Model.Response
{
    public class UserAuthResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
