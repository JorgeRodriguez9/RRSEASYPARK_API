using System.ComponentModel.DataAnnotations;

namespace RRSEasyPark.Models
{
    public class ParkingLot
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        [MaxLength(150)]
        public string? Adress { get; set; } = string.Empty;
        [MaxLength(20)]
        public string? Nit { get; set; } = string.Empty;
        public long Telephone { get; set; }
        public int NormalPrice { get; set; }
        public int DisabilityPrice { get; set; }
        public string? Info { get; set; } = string.Empty;
        public int CantSpacesMotorcycle { get; set; }
        public int CantSpacesCar { get; set; }
        public int CantSpacesDisability { get; set; }
        public string? disabilityservices {  get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public virtual City? City { get; set; }
        public Guid PropietaryParkId { get; set; }
        public virtual PropietaryPark? PropietaryPark { get; set;}
    }
}
