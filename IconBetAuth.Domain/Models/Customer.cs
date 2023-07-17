using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string APIUrl { get; set; }
        public bool Active { get; set; }
        public string UrlError { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
    }
}
