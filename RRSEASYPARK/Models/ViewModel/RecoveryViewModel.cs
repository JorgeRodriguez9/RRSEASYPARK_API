using System.ComponentModel.DataAnnotations;

namespace RRSEASYPARK.Models.ViewModel
{
    public class RecoveryViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
