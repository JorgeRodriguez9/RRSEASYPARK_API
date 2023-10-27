using System.ComponentModel.DataAnnotations;

namespace RRSEASYPARK.Models.ViewModel
{
    public class TokenViewModel
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
