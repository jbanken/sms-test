
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Twilio.IncomingMessageCallback")]
    public class TwilioIncomingMessageCallback
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string AccountSid    {get;set;}
        [StringLength(20)]
        public string APIVersion { get;set;}
        public string Body { get;set;}
        [StringLength(100)]
        public string From {get;set;}
        [StringLength(200)]
        public string FromCity { get;set;}
        [StringLength(50)]
        public string FromCountry { get;set;}
        [StringLength(50)]
        public string FromState { get;set;}
        [StringLength(50)]
        public string FromZip { get;set;}
        [StringLength(100)]
        public string MessageSid { get;set;}
        [StringLength(10)]
        public string NumMedia { get;set;}
        [StringLength(10)]
        public string NumSegments { get;set;}
        [StringLength(100)]
        public string SmsMessageSid { get;set;}
        [StringLength(100)]
        public string SmsSid {get;set;}
        [StringLength(50)]
        public string SmsStatus { get;set;}
        [StringLength(100)]
        public string To {get;set;}
        [StringLength(200)]
        public string ToCity { get;set;}
        [StringLength(50)]
        public string ToCountry { get;set;}
        [StringLength(50)]
        public string ToState { get;set;}
        [StringLength(50)]
        public string ToZip { get;set;}
        public DateTime CreateDate { get; set; }
    }
}
