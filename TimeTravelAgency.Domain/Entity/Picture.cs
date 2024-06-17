using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.Domain.Entity
{
    public class Picture
    {
        public int Id { get; set; }
        public ViewName ViewName { get; set; }
        public string? Title { get; set; }
        public string? Href { get; set; }
        public byte[]? Image { get; set; }
    }
}
