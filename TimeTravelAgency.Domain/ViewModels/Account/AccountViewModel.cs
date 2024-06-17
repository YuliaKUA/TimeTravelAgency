using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.ViewModels.Account
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string? ULogin { get; set; }
        public string? HashPassword { get; set; }
        public Role? URole { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Uaddress { get; set; }
    }
}
