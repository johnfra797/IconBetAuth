using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.Enum
{
    public enum Role
    {
        [Description("Admin")]
        Admin = 2,
        [Description("User")]
        User = 1
    }
}
