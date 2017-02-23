using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class SendRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public string ReferenceCode { get; set; }
    }
}
