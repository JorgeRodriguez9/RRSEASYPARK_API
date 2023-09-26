using System.ComponentModel.DataAnnotations;

namespace RRSEasyPark.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
    }
}
