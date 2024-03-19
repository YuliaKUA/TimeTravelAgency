using System;
using System.Collections.Generic;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.Entity
{
    public class Tour
    {
        public Tour()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public TypeTour? TypeTour { get; set; }
        public string? Title { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Descriptions { get; set; }
        public double? Price { get; set; }
        public int? NumberOfPlaces { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
