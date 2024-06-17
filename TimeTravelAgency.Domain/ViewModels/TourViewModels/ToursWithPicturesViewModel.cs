using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;

namespace TimeTravelAgency.Domain.ViewModels.TourViewModels
{
    public class ToursWithPicturesViewModel
    {
        public List<Tour>? tours { get; set; }
        public Dictionary<string, Picture>? pictures { get; set; }
    }
}
