using System.ComponentModel.DataAnnotations;

namespace Reservations.Api.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
    }
}
