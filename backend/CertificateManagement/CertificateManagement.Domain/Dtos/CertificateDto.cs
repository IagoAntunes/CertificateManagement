namespace CertificateManagement.Domain.Dtos
{
    public class CertificateDto
    {
        public string? Id { get; set; }
        public string StudentFullName { get; set; } = null!;
        public List<string> Activities { get; set; } = new List<string>();
        public ImageDto? Image { get; set; }
        public string SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
