namespace RRSEASYPARK.Models.Dto
{
    public class PropietaryParkPostDto
    {
        public string? Name { get; set; } = string.Empty;
        public long Identification { get; set; }
        public string? Email { get; set; } = string.Empty;
        public long Telephone { get; set; }
        public string? NameUser { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Rol { get; set; }

    }
}
