using RunGroops.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroops.Models
{
    public class Race
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Image { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }

        public AppUser? AppUser { get; set; }

        public RaceCategory RaceCategory { get; set; }  

    }
}
