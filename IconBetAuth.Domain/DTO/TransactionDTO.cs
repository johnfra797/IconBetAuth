
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IconBetAuth.Domain.Enum;

namespace IconBetAuth.Domain.DTO
{
    public class TransactionDTO : DatabaseConnectionDTO
    {
        public string Company { get; set; }
        public string UserName { get; set; }
        public string Currency { get; set; }
        public TransactionsType TransactionsType { get; set; }
        public decimal Amount { get; set; }
        public string? UUID { get; set; }
        public string TicketUUID { get; set; }
        public string Description { get; set; }
    }
}
