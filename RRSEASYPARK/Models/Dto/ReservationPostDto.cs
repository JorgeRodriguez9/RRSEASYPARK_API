namespace RRSEASYPARK.Models.Dto
{
    public class ReservationPostDto
    {       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid VehicleType { get; set; }
        public Guid ParkingLotId { get; set; }
        public string Disability { get; set; }
        public long TotalPrice { get; set; }
    }
}
