
namespace SMSProvider.Models
{
    public class SendRequest
    {
        public string To { get; set; }

        public string From { get; set; }

        public string Message { get; set; }

        public string ReferenceCode { get; set; }

        public string AccountSid { get; set; }

        public string AuthToken { get; set; }

        public string StatusCallbackURL { get; set; }
    }
}
