using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CertificateManagement.Domain.Entities
{
    public class CertificateImage
    {
        public string FileName { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string ContentType { get; set; } = null!;
    }
}

