using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Span_Aid
{
    public class Span_Aid_TL
    {
        public long TLTicketId { get; set; }
        public string TLTicketNumber { get; set; }
        public string TLEmailAddress { get; set; }
        public string TLUserName { get; set; }
        public string TLModuleName { get; set; }
        public string TLTicketDescription { get; set; }
        public string TLTicketStatus { get; set; }
        public DateTime TLTicketCreateDate { get; set; }
        public DateTime TLTicketUpdateDate { get; set; }
    }
}