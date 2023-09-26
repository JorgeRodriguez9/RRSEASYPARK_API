using System.ComponentModel.DataAnnotations;

namespace RRSEasyPark.Models
{
    public class PropietaryPark
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        public long Identification { get; set; }
        [MaxLength(60)]
        public string? Email { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
