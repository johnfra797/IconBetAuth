using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.DTO
{
    public class ResponseDTO : DatabaseConnectionDTO
    {
        public bool hasError { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
