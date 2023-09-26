using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;

namespace RRSEASYPARK.DAL
{
    public class RRSEASYPARKContext : DbContext
    {
        public RRSEASYPARKContext(DbContextOptions<RRSEASYPARKContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<ClientParkingLot> ClientParkingLot { get; set; }
        public DbSet<ParkingLot> parkingLots { get; set; }
        public DbSet<PropietaryPark> propietaryParks { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<Rol> rols { get; set; }
        public DbSet<TypeVehicle> typeVehicles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Value> values { get; set; }
    }
}
