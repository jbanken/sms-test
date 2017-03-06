using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("Twilio.StatusCallback")]
    public class TwilioStatusCallback
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string SmsSid  { get; set; }
        [StringLength(100)]
        public string SmsStatus  { get; set; }
        [StringLength(100)]
        public string MessageStatus { get; set; }
        [StringLength(100)]
        public string To { get; set; }
        [StringLength(100)]
        public string MessageSid { get; set; }
        [StringLength(100)]
        public string AccountSid { get; set; }
        [StringLength(100)]
        public string From { get; set; }
        [StringLength(20)]
        public string APIVersion { get; set; }
    }
}
