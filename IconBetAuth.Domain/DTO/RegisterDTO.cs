using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class RegisterDTO : DatabaseConnectionDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
