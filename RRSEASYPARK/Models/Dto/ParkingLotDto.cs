namespace RRSEASYPARK.Models.Dto
{
    public class ParkingLotDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Adress { get; set; } = string.Empty;
        public string? Nit { get; set; } = string.Empty;
        public long Telephone { get; set; }
        public int NormalPrice { get; set; }
        public int DisabilityPrice { get; set; }
        public string? Info { get; set; } = string.Empty;
        public int CantSpacesMotorcycle { get; set; }
        public int CantSpacesCar { get; set; }
        public int CantSpacesDisability { get; set; }
        public string? disabilityservices { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public string? CityName { get; set; } = string.Empty;
        public Guid PropietaryParkId { get; set; }
    }
}
