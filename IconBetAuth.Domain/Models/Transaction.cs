using IconBetAuth.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("TransactionsType")]
        public string TransactionsType { get; set; }

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [JsonProperty("TicketUUID")]
        public string TicketUUID { get; set; }

        [JsonProperty("UUID")]
        public string UUID { get; set; }

        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}