﻿namespace RRSEASYPARK.Models.Dto
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public TimeOnly starttime { get; set; }
        public TimeOnly Endtime { get; set; }
        public long TotalPrice { get; set; }
        public string? Disabled { get; set; }
        public Guid ClientId { get; set; }
        public string? ClientName { get; set; } = string.Empty;
        public string? Telephone { get; set; } = string.Empty;
        public Guid TypeVehicleId { get; set; }
        public Guid ParkingLotId { get; set; }
    }
}
