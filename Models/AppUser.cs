using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroops.Models
{
    public class AppUser
    {
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }


    }
}
