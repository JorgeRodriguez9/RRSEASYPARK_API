using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RRSEasyPark.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(256)]
        public string? Password { get; set; } = string.Empty;
        public string? Token_Recovery {  get; set; } = string.Empty;
        public Guid RolId { get; set; }
        public virtual Rol? Rol { get; set; }
    }
}
