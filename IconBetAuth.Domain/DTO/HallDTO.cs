using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class HallDTO
    {
        [JsonProperty("isCalculated")]
        public bool IsCalculated { get; set; }
        [JsonProperty("amountExChanged")]
        public decimal AmountExChanged { get; set; }
    }
}
