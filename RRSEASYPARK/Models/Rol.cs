using System.ComponentModel.DataAnnotations;

namespace RRSEasyPark.Models
{
    public class Rol
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; } = string.Empty;
    }
}
