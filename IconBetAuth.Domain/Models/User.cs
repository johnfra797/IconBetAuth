using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Rol { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
    }
}
