using System.ComponentModel.DataAnnotations;

namespace RRSEASYPARK.Models.ViewModel
{
    public class RecoveryPasswordViewModel
    {
        public string token { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Compare("Password")]
        [Required]
        public string Password2 { get; set; } = string.Empty;
    }
}
