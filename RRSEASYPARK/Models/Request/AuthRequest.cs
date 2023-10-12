using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace RRSEASYPARK.Models.Request
{
    public class AuthRequest
    {
        public string? nameUser { get; set; } = string.Empty;

        public string? Password { get; set; } = string.Empty;

    }
}
