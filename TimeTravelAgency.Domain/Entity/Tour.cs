using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Введите название тура")]
        //[Remote("IsTourTitleAvailable", "TourController", HttpMethod = "POST", ErrorMessage = "Tour title already in use")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите дату начала")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Введите дату окончания")]
        public DateTime DateEnd { get; set; }
        public string? Descriptions { get; set; }
        public double? Price { get; set; }
        public int? NumberOfPlaces { get; set; }
        public string? FullInfo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
