using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Domain.Enum
{
    public enum Error
    {
        [Description("Existing email, please choose another one!!")]
        ExistsEmail = 1,
        [Description("Existing username, please choose another one!!")]
        ExistsUserName = 2,
        [Description("Existing phone, please choose another one!!")]
        ExistsPhone = 3,
        [Description("General Error!!")]
        GeneralError = 4,
        [Description("An error occurred during the login process, please try again!!")]
        LoginError = 5,
        [Description("There is an error with the UUID of your ticket, please check it!!")]
        TicketUUIDError = 6,
    }
}
