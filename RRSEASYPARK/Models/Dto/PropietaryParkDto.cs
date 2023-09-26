namespace RRSEASYPARK.Models.Dto
{
    public class PropietaryParkDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public long Identification { get; set; }
        public string? Email { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
