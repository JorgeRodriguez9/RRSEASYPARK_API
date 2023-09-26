using System.ComponentModel.DataAnnotations;

namespace RRSEasyPark.Models
{
    public class TypeVehicle
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
    }
}
