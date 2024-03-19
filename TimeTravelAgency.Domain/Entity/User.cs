using System;
using System.Collections.Generic;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.Entity
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? ULogin { get; set; }
        public string? HashPassword { get; set; }
        public Role? URole { get; set; }

        public virtual Uprofile UProfile { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
