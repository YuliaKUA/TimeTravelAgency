using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimeTravelAgency.Domain.Enum
{
    public enum TypeTour
    {
        [Display(Name = "Нулевой")]
        Null = 0,
        [Display(Name = "Первый")]
        First = 1,
    }
}
