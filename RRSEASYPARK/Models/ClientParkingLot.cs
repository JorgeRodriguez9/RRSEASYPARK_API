using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRSEasyPark.Models
{
    //[Table ("ClientParkingLot")]
    public class ClientParkingLot
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        public long Identification { get; set; }
        [MaxLength(60)] 
        public string? Email { get; set; } = string.Empty;
        public long Telephone { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
