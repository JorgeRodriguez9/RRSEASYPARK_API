namespace RRSEASYPARK.Models.Dto
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TotalPrice { get; set; }
        public string? Disabled { get; set; }
        public Guid ClientId { get; set; }
        public Guid TypeVehicleId { get; set; }
        public Guid ParkingLotId { get; set; }
    }
}
