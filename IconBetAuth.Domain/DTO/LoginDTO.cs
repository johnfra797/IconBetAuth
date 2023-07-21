using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class LoginDTO : DatabaseConnectionDTO
    {
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }
    }
}
