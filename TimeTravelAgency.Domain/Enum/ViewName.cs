using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTravelAgency.Domain.Enum
{
    public enum ViewName
    {
        [Display(Name = "Index")]
        Index = 0,
        [Display(Name = "TravelGuide")]
        TravelGuide = 1,
        [Display(Name = "Profile")]
        Profile = 2,
        [Display(Name = "Tour")]
        Tour = 3,
        [Display(Name = "Layout")]
        Layout = 4,
        [Display(Name = "Error")]
        Error = 5
    }
}
