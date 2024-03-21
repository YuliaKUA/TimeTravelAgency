using System;
using System.Collections.Generic;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int? TourId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateCreate { get; set; }
        public StatusOrder Status { get; set; }
        public string? Number { get; set; }

        public virtual Tour? Tour { get; set; }
        public virtual User? User { get; set; }
    }
}
