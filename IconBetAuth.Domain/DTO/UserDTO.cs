using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class UserDTO : ResponseDTO
    {
        public string? UserName { get; set; }
        public string? Currency { get; set; }
        public decimal? Balance { get; set; }
        public string? Rol { get; set; }
        public string? Country { get; set; }
        public bool? Active { get; set; }
    }
}