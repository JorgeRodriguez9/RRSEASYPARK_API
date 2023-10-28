namespace RRSEASYPARK.Models.Response
{
    public class ReservationResponse
    {

        public Guid Id { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public long TotalPrice { get; set; }
        public string? Disability { get; set; }
        public Guid TypeVehicleId { get; set; }
        public Guid ParkingLotId { get; set; }
        public string ParkingLotName { get; set; }
        public string Adress { get; set; }

    }
}
