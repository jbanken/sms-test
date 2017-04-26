using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class SendRequest
    {
        [Required]
        public string To { get; set; }
        public string From { get; set; }
        [Required]
        public string Message { get; set; }
        public string ReferenceCode { get; set; }
        public Guid? MessageId { get; set; }
    }
}
