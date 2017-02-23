using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("dbo.APILog")]
    public partial class APILog
    {
        public int Id { get; set; }
        
        public Guid? GroupKey { get; set; }

        [StringLength(100)]
        public string RequestIpAddress { get; set; }

        [StringLength(100)]
        public string RequestContentType { get; set; }
        
        public string RequestContentBody { get; set; }

        public string RequestUri { get; set; }

        public string RequestMethod { get; set; }

        public string RequestHeaders { get; set; }

        public DateTime? RequestTimeStamp { get; set; }

        [StringLength(100)]
        public string ResponseContentType { get; set; }

        public string ResponseContentBody { get; set; }

        public int? ResponseStatusCode { get; set; }

        public string ResponseHeaders { get; set; }

        public DateTime? ResponseTimeStamp { get; set; }
         
    }
}
