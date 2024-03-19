using System;
using System.Collections.Generic;

namespace TimeTravelAgency.Domain.Entity
{
    public class Uprofile
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Uaddress { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
    }
}
