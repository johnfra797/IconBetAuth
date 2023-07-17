
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.Models
{
     public class Hall 
    {
        public int HallId { get; set; }
        [JsonProperty("parentHash")]
        public string ParentHash { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("isCalculated")]
        public bool IsCalculated { get; set; }
        [JsonProperty("amountExChanged")]
        public decimal AmountExChanged { get; set; }


    }
}
