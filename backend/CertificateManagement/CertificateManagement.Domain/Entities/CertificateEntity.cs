using MongoDB.Bson.Serialization.Attributes;

namespace CertificateManagement.Domain.Entities
{
    public class CertificateEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("studentFullName")] 
        public string StudentFullName { get; set; } = null!;

        [BsonElement("activities")]
        public List<string> Activities { get; set; } = new List<string>();

        [BsonElement("image")]
        public CertificateImage? Image { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
