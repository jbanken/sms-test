using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
   

    [Table("sms.Message")]
    public partial class MessageLog
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string To { get; set; }

        [Required]
        [StringLength(1000)]
        public string From { get; set; }

        public Guid ThirdPartyServiceID { get; set; }

        [StringLength(200)]
        public string ThirdPartyReferenceCode { get; set; }

        [StringLength(200)]
        public string ReferenceCode { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
