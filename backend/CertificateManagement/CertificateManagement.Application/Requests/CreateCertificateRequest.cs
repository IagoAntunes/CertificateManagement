namespace CertificateManagement.Application.Requests
{
    public class CreateCertificateRequest
    {
        public string StudentFullName { get; set; }
        public string[] Activities { get; set; }
    }
}
