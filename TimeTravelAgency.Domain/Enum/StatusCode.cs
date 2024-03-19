using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTravelAgency.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound = 0,
        ObjectNotFound = 10,
        OK = 200,
        LoginAlreadyUse = 365,
        NotFound = 404,
        InternalServerError = 500,
    }
}
