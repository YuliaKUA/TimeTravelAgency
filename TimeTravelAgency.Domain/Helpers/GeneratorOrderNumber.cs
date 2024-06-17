using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTravelAgency.Domain.Helpers
{
    public class GeneratorOrderNumber
    {
        public static string GenerateNumber(int idOrder, DateTime date)
        {
            return date.ToString("yyyyMMdd") + string.Format("{0:d5}", idOrder); ;
        }
    }
}
