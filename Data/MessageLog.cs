namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message.Log")]
    public partial class MessageLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string To { get; set; }

        [Required]
        [StringLength(1000)]
        public string From { get; set; }

        public int ThirdPartyServiceID { get; set; }

        [StringLength(200)]
        public string ThirdPartyReferenceCode { get; set; }

        [StringLength(200)]
        public string ReferenceCode { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
