using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }
        public string Number { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
