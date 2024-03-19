using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.Response
{
    public interface IBaseResponse<T>
    {
        public string Description { get; set; } //Warning + exception
        public StatusCode StatusCode { get; set; }
        T Data { get; set; } //returned request object
    }
}
