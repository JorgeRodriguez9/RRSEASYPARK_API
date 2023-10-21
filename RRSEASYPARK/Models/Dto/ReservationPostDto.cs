namespace RRSEASYPARK.Models.Dto
{
    public class ReservationPostDto
    {       
        public string Date { get; set; }
        public TimeOnly starttime { get; set; }
        public TimeOnly Endtime { get; set; }
        public Guid VehicleType { get; set; }
        public Guid ParkingLotId { get; set; }
        public string Disability { get; set; }
        public long TotalPrice { get; set; }
    }
}
