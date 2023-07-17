using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Agent { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public string BalanceStr { get; set; }
        public HallDTO Hall { get; set; }
    }
}
