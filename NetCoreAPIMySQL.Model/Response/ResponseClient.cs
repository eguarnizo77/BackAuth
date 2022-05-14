using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackAuth.Model.Response
{
    public class ResponseClient
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
