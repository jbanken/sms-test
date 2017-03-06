using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSProvider.Models
{
    public class SendResponse
    {
        public int Id { get; set; }
        public string AccountSid { get; set; }
        public string APIVersion { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string DateSent { get; set; }
        public string Direction { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string MessageServiceSid { get; set; }
        public string NumMedia { get; set; }
        public string NumSegments { get; set; }
        public string Price { get; set; }
        public string PriceUnit { get; set; }
        public string Sid { get; set; }
        public string Status { get; set; }
        public string To { get; set; }
        public string Uri { get; set; }
    }
}
