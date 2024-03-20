using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.Entity
{
    public class ExtendedOrder
    {
        public int Id { get; set; }
        public int? TourId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateCreate { get; set; }
        public StatusOrder Status { get; set; }
        public string? Title { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Descriptions { get; set; }
    }
}
