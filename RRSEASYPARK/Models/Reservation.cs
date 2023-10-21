namespace RRSEasyPark.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public long TotalPrice { get; set; }
        public string? Disabled { get; set; } = string.Empty;
        public Guid ClientParkingLotId { get; set; }
        public virtual ClientParkingLot? ClientParkingLot { get; set; }
        public Guid TypeVehicleId { get; set; }
        public virtual TypeVehicle? TypeVehicle { get; set; }
        public Guid ParkingLotId { get; set; }
        public virtual ParkingLot? ParkingLot { set; get; }
    }
}
