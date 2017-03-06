using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Twilio.Message")]
    public class TwilioMessage
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string AccountSid { get; set; }
        [StringLength(20)]
        public string APIVersion { get; set; }
        public string Body { get; set; }
        [StringLength(100)]
        public string From { get; set; }
        [StringLength(50)]
        public string DateCreated {get;set;}
        [StringLength(50)]
        public string DateUpdated { get;set;}
        [StringLength(50)]
        public string DateSent { get;set;}
        [StringLength(50)]
        public string Direction { get;set;}
        [StringLength(50)]
        public string ErrorCode { get;set;}
        public string ErrorMessage { get;set;}
        [StringLength(100)]
        public string MessageServiceSid { get;set;}
        [StringLength(10)]
        public string NumMedia { get;set;}
        [StringLength(10)]
        public string NumSegments { get;set;}
        [StringLength(20)]
        public string Price { get;set;}
        [StringLength(10)]
        public string PriceUnit { get; set; }
        [StringLength(100)]
        public string Sid { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(100)]
        public string To { get; set; }
        [StringLength(1000)]
        public string Uri { get; set; }
    }
}
