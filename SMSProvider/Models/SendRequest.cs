
namespace SMSProvider.Models
{
    public class SendRequest
    {
        public string To { get; set; }

        public string From { get; set; }

        public string Message { get; set; }

        public string ReferenceCode { get; set; }
    }
}
