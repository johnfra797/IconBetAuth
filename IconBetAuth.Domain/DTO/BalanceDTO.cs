using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class BalanceDTO
    {
        public string UserName { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
